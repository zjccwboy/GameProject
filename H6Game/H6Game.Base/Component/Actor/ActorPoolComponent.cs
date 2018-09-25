using H6Game.Hotfix.Enums;
using H6Game.Hotfix.Messages.Attributes;
using H6Game.Hotfix.Messages.Enums;
using H6Game.Hotfix.Messages.Inner;
using MongoDB.Bson;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace H6Game.Base
{
    /// <summary>
    /// 订阅与处理Actor新增消息
    /// </summary>
    [HandlerCMD(InnerMessageCMD.AddActorCmd)]
    public class ActorAddSubscriber : AActorSubscriber<ActorSyncMessage>
    {
        protected override void Subscribe(Network network, ActorSyncMessage message)
        {
            Log.Info(message.ToJson(), LoggerBllType.System);
            Game.Actor.GetActorPool(message.ActorType).AddRemote(message, network);
        }
    }

    /// <summary>
    /// 订阅与处理Actor删除消息
    /// </summary>
    [HandlerCMD(InnerMessageCMD.RemoveActorCmd)]
    public class ActorRemoveSubscriber : AActorSubscriber
    {
        protected override void Subscribe(Network network)
        {
            Log.Info(network.RecvPacket.MessageCmd.ToString(), LoggerBllType.System);
            if (Game.Scene.GetComponent(network.RecvPacket.ActorId, out BaseComponent component))
                component.Dispose();
        }
    }

    /// <summary>
    /// 订阅与回发全量的本地LocalActor信息
    /// </summary>
    [HandlerCMD(InnerMessageCMD.SyncActorInfoCmd)]
    public class SyncFullActorSubscriber : AMsgSubscriber
    {
        protected override void Subscribe(Network network)
        {
            //回发AMessageCMD.AddActorCmd消息告诉远程订阅服务新增RemoteActor
            network.RecvPacket.MessageCmd = (int)InnerMessageCMD.AddActorCmd;

            var components = Game.Scene.GetComponents<ActorPoolComponent>();
            foreach (var component in components)
                (component as ActorPoolComponent).ResponseFullActor(network);
        }
    }


    [Event(EventType.Awake)]
    public class ActorPoolComponent : BaseComponent
    {
        private ConcurrentDictionary<int, Dictionary<int, BaseActorEntityComponent>> NetChannelIdEntitys { get; } = new ConcurrentDictionary<int, Dictionary<int, BaseActorEntityComponent>>();
        private ConcurrentDictionary<int, BaseActorEntityComponent> LocalComponentDictionary { get; } = new ConcurrentDictionary<int, BaseActorEntityComponent>();
        private ConcurrentDictionary<int, BaseActorEntityComponent> RemoteComponentDictionary { get; } = new ConcurrentDictionary<int, BaseActorEntityComponent>();
        private ConcurrentDictionary<string, BaseActorEntityComponent> AllComponentDictionary { get; } = new ConcurrentDictionary<string, BaseActorEntityComponent>();
        private IEnumerable<BaseActorEntityComponent> LocalComponents { get { return LocalComponentDictionary.Values; } }

        public static Action<ANetChannel> OnServerDisconnected { get; set; }
        public static Action<ANetChannel> OnInnerClientConnected { get; set; }

        public ActorType ActorType { get; }
        public ActorPoolComponent(ActorType actorType)
        {
            this.ActorType = actorType;
        }

        public override void Awake()
        {
            var innerComponent = Game.Scene.AddComponent<DistributionsComponent>();

            //断开消息只注册一次
            if (OnServerDisconnected == null)
            {
                OnServerDisconnected = c =>
                {
                    if (!NetChannelIdEntitys.TryGetValue(c.Network.Channel.Id, out Dictionary<int, BaseActorEntityComponent> dicVal))
                        return;

                    foreach (var kv in dicVal)
                        kv.Value.Dispose();
                };
                innerComponent.OnInnerServerDisconnected += OnServerDisconnected;
            }

            //连接消息只注册一次
            if(OnInnerClientConnected == null)
            {
                OnInnerClientConnected = c => { c.Network.Send((int)InnerMessageCMD.SyncActorInfoCmd); };
                innerComponent.OnInnerClientConnected += OnInnerClientConnected;
            }
        }

        public void AddLocal(BaseActorEntityComponent component)
        {
            if (!LocalComponentDictionary.TryAdd(component.Id, component))
                return;

            AllComponentDictionary[component.ActorEntity.Id] = component;

            NotifyAllServerWithAdd(component.ActorEntity);
        }

        public void AddRemote(ActorSyncMessage message, Network network)
        {
            switch (message.ActorType)
            {
                case ActorType.Player:
                    {
                        var component = Game.Scene.AddComponent<PlayerComponent>();
                        component.AddRemote(message.ObjectId, network);
                        AddRemote(component);
                    }
                    break;
                case ActorType.Room:
                    {
                        var component = Game.Scene.AddComponent<RoomComponent>();
                        component.AddRemote(message.ObjectId, network);
                        AddRemote(component);
                    }
                    break;
                case ActorType.Game:
                    {
                        var component = Game.Scene.AddComponent<GameComponent>();
                        component.AddRemote(message.ObjectId, network);
                        AddRemote(component);
                    }
                    break;
            }
        }

        public void RemoveRemote(ActorEntity entity)
        {
            if (!NetChannelIdEntitys.TryGetValue(entity.Network.Channel.Id, out Dictionary<int, BaseActorEntityComponent> dicVal))
                return;

            if (!dicVal.TryGetValue(entity.ActorId, out BaseActorEntityComponent component))
                return;

            if (RemoteComponentDictionary.TryRemove(component.Id, out component))
                AllComponentDictionary.TryRemove(component.ActorEntity.Id, out component);
        }

        public void RemoveLocal(ActorEntity entity)
        {
            if (!AllComponentDictionary.TryRemove(entity.Id, out BaseActorEntityComponent component))
                return;

            if (LocalComponentDictionary.TryRemove(component.Id, out component))
                NotifyAllServerWithRemove(component.ActorEntity);
        }

        public void ResponseFullActor(Network network)
        {
            var count = 0;
            foreach (var localComponent in LocalComponents)
            {
                localComponent.SendLocalActorInfoMessage(network);

                count++;

                //一次最多发送100条，避免服务端分配过大的缓冲区
                if (count >= 100)
                {
                    network.Channel.StartSend();
                    count = 0;
                }
            }
        }

        private void AddRemote(BaseActorEntityComponent component)
        {
            if (!RemoteComponentDictionary.TryAdd(component.Id, component))
                return;

            var entity = component.ActorEntity;
            if (!NetChannelIdEntitys.TryGetValue(entity.Network.Channel.Id, out Dictionary<int, BaseActorEntityComponent> dicVal))
            {
                dicVal = new Dictionary<int, BaseActorEntityComponent>();
                NetChannelIdEntitys[entity.Network.Channel.Id] = dicVal;
            }
            dicVal[entity.ActorId] = component;

            AllComponentDictionary[component.ActorEntity.Id] = component;
        }

        private void NotifyAllServerWithAdd(ActorEntity entity)
        {
            var message = new ActorSyncMessage
            {
                ObjectId = entity.Id,
                ActorType = entity.ActorType,
            };
            Game.Scene.GetComponent<DistributionsComponent>().InConnNets.BroadcastActor(message, (int)InnerMessageCMD.AddActorCmd, entity.ActorId);
        }

        private void NotifyAllServerWithRemove(ActorEntity entity)
        {
            var message = new ActorSyncMessage
            {
                ObjectId = entity.Id,
                ActorType = entity.ActorType,
            };
            Game.Scene.GetComponent<DistributionsComponent>().InConnNets.BroadcastActor(message, (int)InnerMessageCMD.RemoveActorCmd, entity.ActorId);
        }
    }
}
