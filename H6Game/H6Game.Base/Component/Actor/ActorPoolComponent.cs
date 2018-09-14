using H6Game.Entities.Enums;
using H6Game.Message;
using MongoDB.Bson;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace H6Game.Base
{
    /// <summary>
    /// 订阅与处理Add or Remove Actor消息
    /// </summary>
    [HandlerCMD(MessageCMD.AddActorCmd, MessageCMD.RemoveActorCmd)]
    public class ActorAddOrRemoveHandler : AActorHandler<ActorSyncMessage>
    {
        protected override void Handler(Network network, ActorSyncMessage message)
        {
            Log.Logger.Info(network.RecvPacket.ToJson());
            Game.Actor.GetActorPool(message.ActorType).AddOrRemoveRemoteActor(message, network);
        }
    }

    /// <summary>
    /// 订阅与回发全量的本地LocalActor信息
    /// </summary>
    [HandlerCMD(MessageCMD.SyncActorInfoCmd)]
    public class SyncFullActorHandler : AHandler
    {
        protected override void Handler(Network network)
        {
            Log.Logger.Info(network.RecvPacket.ToJson());

            //回发AMessageCMD.AddActorCmd消息告诉远程订阅服务新增RemoteActor
            network.RecvPacket.MessageCmd = (int)MessageCMD.AddActorCmd;

            var components = Game.Scene.GetComponents<ActorPoolComponent>();
            foreach (var component in components)
            {
                var actorComponent = component as ActorPoolComponent;
                actorComponent.GetFullActor(network);
            }
        }
    }


    [Event(EventType.Awake)]
    public class ActorPoolComponent : BaseComponent
    {
        private ConcurrentDictionary<int, HashSet<BaseActorEntityComponent>> NetChannelIdEntitys { get; } = new ConcurrentDictionary<int, HashSet<BaseActorEntityComponent>>();
        private ConcurrentDictionary<int, BaseActorEntityComponent> LocalComponentDictionary { get; } = new ConcurrentDictionary<int, BaseActorEntityComponent>();
        private ConcurrentDictionary<int, BaseActorEntityComponent> RemoteComponentDictionary { get; } = new ConcurrentDictionary<int, BaseActorEntityComponent>();
        private ConcurrentDictionary<string, BaseActorEntityComponent> AllComponentDictionary { get; } = new ConcurrentDictionary<string, BaseActorEntityComponent>();
        private IEnumerable<BaseActorEntityComponent> LocalComponents { get { return LocalComponentDictionary.Values; } }
        public ActorType ActorType { get;}
        public ActorPoolComponent(ActorType actorType)
        {
            this.ActorType = actorType;
        }

        public override void Awake()
        {
            var innerComponent = Game.Scene.AddComponent<InnerComponent>();

            innerComponent.OnDisConnected += (channel) =>
            {
                if (NetChannelIdEntitys.TryRemove(channel.Id, out HashSet<BaseActorEntityComponent> components))
                {
                    foreach (var component in components)
                    {
                        RemoteComponentDictionary.TryRemove(component.Id, out BaseActorEntityComponent value);
                    }
                }
            };

            innerComponent.OnConnected += (channel) => { channel.Network.Send((int)MessageCMD.SyncActorInfoCmd); };
        }

        public void AddOrRemoveRemoteActor(ActorSyncMessage message, Network network)
        {
            if (network.RecvPacket.MessageCmd == (int)MessageCMD.AddActorCmd)
            {
                AddRemote(message, network);
            }
            else if (network.RecvPacket.MessageCmd == (int)MessageCMD.RemoveActorCmd)
            {
                Remove(message);
            }
        }

        public void AddLocal(BaseActorEntityComponent component)
        {
            if (!LocalComponentDictionary.TryAdd(component.Id, component))
                return;

            AllComponentDictionary[component.ActorEntity.Id] = component;

            NotifyAllServerWithAdd(component.ActorEntity);
        }

        private void Remove(ActorSyncMessage message)
        {
            RemoveRemote(message.ObjectId);
            RemoveLocal(message.ObjectId);
        }

        private void AddRemote(ActorSyncMessage message, Network network)
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

        private void AddRemote(BaseActorEntityComponent component)
        {
            if (!RemoteComponentDictionary.TryAdd(component.Id, component))
                return;

            var entity = component.ActorEntity;
            if(!NetChannelIdEntitys.TryGetValue(entity.Network.Channel.Id, out HashSet<BaseActorEntityComponent> hashVal))
            {
                hashVal = new HashSet<BaseActorEntityComponent>();
                NetChannelIdEntitys[entity.Network.Channel.Id] = hashVal;
            }
            hashVal.Add(component);

            AllComponentDictionary[component.ActorEntity.Id] = component;
        }

        public void RemoveRemote(string objectId)
        {
            if (!AllComponentDictionary.TryRemove(objectId, out BaseActorEntityComponent component))
                return;

            if(RemoteComponentDictionary.TryRemove(component.Id, out component))
                component.Dispose();
        }

        public void RemoveLocal(string objectId)
        {
            if (!AllComponentDictionary.TryRemove(objectId, out BaseActorEntityComponent component))
                return;

            if(LocalComponentDictionary.TryRemove(component.Id, out component))
            {
                NotifyAllServerWithRemove(component.ActorEntity);
                component.Dispose();
            }
        }


        public bool TryGetComponent(string objectId, out BaseActorEntityComponent component)
        {
            return AllComponentDictionary.TryGetValue(objectId, out component);
        }

        public void GetFullActor(Network network)
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

        private void NotifyAllServerWithAdd(ActorEntity entity)
        {
            var message = new ActorSyncMessage
            {
                ObjectId = entity.Id,
                ActorType = entity.ActorType,
            };

            var innerComponent = Game.Scene.GetComponent<InnerComponent>();
            innerComponent.InConnNets.BroadcastActor(message, (int)MessageCMD.AddActorCmd, entity.ActorId);
        }

        private void NotifyAllServerWithRemove(ActorEntity entity)
        {
            var message = new ActorSyncMessage
            {
                ObjectId = entity.Id,
                ActorType = entity.ActorType,
            };

            var innerComponent = Game.Scene.GetComponent<InnerComponent>();
            innerComponent.InConnNets.BroadcastActor(message, (int)MessageCMD.RemoveActorCmd, entity.ActorId);
        }
    }
}
