using H6Game.Hotfix.Enums;
using H6Game.Hotfix.Messages.Attributes;
using H6Game.Hotfix.Messages.Enums;
using H6Game.Hotfix.Messages.Inner;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H6Game.Base
{
    /// <summary>
    /// 订阅与处理Actor新增消息
    /// </summary>
    [SubscriberCMD(InnerMessageCMD.AddActorCmd)]
    public class ActorAddSubscriber : AActorSubscriber<ActorSyncMessage>
    {
        protected override void Subscribe(Network network, ActorSyncMessage message, int messageCmd, int actorId)
        {
            var logs = $"CMD:{messageCmd} ActorId:{actorId} MSG:{message.ToJson()}";
            Log.Info(logs, LoggerBllType.System);
            switch (message.ActorType)
            {
                case ActorType.Player:
                    {
                        var actor = Game.Scene.AddComponent<PlayerComponent>();
                        actor.SetRemote(message.ObjectId, network);
                        Game.Scene.GetComponent<ActorPoolComponent>().AddRemote(actor);
                    }
                    break;
                case ActorType.Room:
                    {
                        var actor = Game.Scene.AddComponent<RoomComponent>();
                        actor.SetRemote(message.ObjectId, network);
                        Game.Scene.GetComponent<ActorPoolComponent>().AddRemote(actor);
                    }
                    break;
                case ActorType.Game:
                    {
                        var actor = Game.Scene.AddComponent<GameComponent>();
                        actor.SetRemote(message.ObjectId, network);
                        Game.Scene.GetComponent<ActorPoolComponent>().AddRemote(actor);
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// 订阅与处理Actor删除消息
    /// </summary>
    [SubscriberCMD(InnerMessageCMD.RemoveActorCmd)]
    public class ActorRemoveSubscriber : AActorSubscriber
    {
        protected override void Subscribe(Network network, int messageCmd, int actorId)
        {
            var pool = Game.Scene.GetComponent<ActorPoolComponent>();
            var actor = pool.GetActor(network.Channel.Id, actorId);
            Game.Scene.GetComponent<ActorPoolComponent>().RemoveRemote(actor);

            Log.Info(network.RecvPacket.MessageCmd.ToString(), LoggerBllType.System);
        }
    }

    /// <summary>
    /// 订阅与回发全量的本地LocalActor信息
    /// </summary>
    [SubscriberCMD(InnerMessageCMD.SyncActorInfoCmd)]
    public class SyncFullActorSubscriber : AMsgSubscriber
    {
        protected override void Subscribe(Network network)
        {
            //回发AMessageCMD.AddActorCmd消息告诉远程订阅服务新增RemoteActor
            Game.Scene.GetComponent<ActorPoolComponent>().ResponseLocalActors(network, (int)InnerMessageCMD.AddActorCmd);
        }
    }


    [Event(EventType.Awake)]
    [SingletCase]
    public class ActorPoolComponent : BaseComponent
    {
        private Dictionary<int, Dictionary<int, BaseActorEntityComponent>> NetIdActors { get; } = new Dictionary<int, Dictionary<int, BaseActorEntityComponent>>();
        private Dictionary<ActorType, Dictionary<int, BaseActorEntityComponent>> TypeComponentIdActors { get; } = new Dictionary<ActorType, Dictionary<int, BaseActorEntityComponent>>();
        private Dictionary<ActorType, Dictionary<string, BaseActorEntityComponent>> TypeObjectIdActors { get; } = new Dictionary<ActorType, Dictionary<string, BaseActorEntityComponent>>();
        private Dictionary<int, BaseActorEntityComponent> LocalActors { get; } = new Dictionary<int, BaseActorEntityComponent>();
        private Dictionary<int, BaseActorEntityComponent> RemoteActors { get; } = new Dictionary<int, BaseActorEntityComponent>();
        public Action<ANetChannel> OnDisconnected { get; set; }
        public Action<ANetChannel> OnConnected { get; set; }
        public override void Awake()
        {
            var innerComponent = Game.Scene.AddComponent<DistributionsComponent>();            
            innerComponent.OnInnerClientDisconnected += c =>
            {
                if (!NetIdActors.TryGetValue(c.Network.Channel.Id, out Dictionary<int, BaseActorEntityComponent> dicVal))
                    return;

                this.OnDisconnected?.Invoke(c);

                var components = dicVal.Values.ToList();
                foreach (var component in components)
                    this.RemoveRemote(component);
            };

            innerComponent.OnInnerClientConnected += c => 
            {
                this.OnConnected?.Invoke(c);
                c.Network.Send((int)InnerMessageCMD.SyncActorInfoCmd);
            };
        }

        public void AddLocal(BaseActorEntityComponent component)
        {
            if (LocalActors.ContainsKey(component.Id))
                return;

            LocalActors[component.Id] = component;
            if (!TypeComponentIdActors.TryGetValue(component.ActorType, out Dictionary<int,BaseActorEntityComponent > dicVal))
            {
                dicVal = new Dictionary<int, BaseActorEntityComponent>();
                TypeComponentIdActors[component.ActorType] = dicVal;
            }
            dicVal[component.Id] = component;

            if(!TypeObjectIdActors.TryGetValue(component.ActorType, out Dictionary<string, BaseActorEntityComponent> dicStrVal))
            {
                dicStrVal = new Dictionary<string, BaseActorEntityComponent>();
                TypeObjectIdActors[component.ActorType] = dicStrVal;
            }
            dicStrVal[component.ActorEntity.Id] = component;

            NotifyAllServerWithAdd(component.ActorEntity, component);
        }

        public BaseActorEntityComponent GetActor(string objectId, ActorType actorType)
        {
            if (!TypeObjectIdActors.TryGetValue(actorType, out Dictionary<string, BaseActorEntityComponent> dicStrVal))
                return null;

            if (!dicStrVal.TryGetValue(objectId, out BaseActorEntityComponent component))
                return null;

            return component;
        }

        public BaseActorEntityComponent GetActor(int netChannelId, int actorId)
        {
            if (!NetIdActors.TryGetValue(netChannelId, out Dictionary<int, BaseActorEntityComponent> dicVal))
                return null;

            if (!dicVal.TryGetValue(actorId, out BaseActorEntityComponent component))
                return null;

            return component;
        }

        public BaseActorEntityComponent GetActor(int componentId, ActorType actorType)
        {
            if (!TypeComponentIdActors.TryGetValue(actorType, out Dictionary<int, BaseActorEntityComponent> dicVal))
                return null;

            if (!dicVal.TryGetValue(componentId, out BaseActorEntityComponent component))
                return null;

            return component;
        }

        public void RemoveRemote(BaseActorEntityComponent component)
        {
            if (!TypeObjectIdActors.TryGetValue(component.ActorType, out Dictionary<string, BaseActorEntityComponent> dicStrVal))
                return;

            if (!dicStrVal.Remove(component.ActorEntity.Id))
                return;

            if (!NetIdActors.TryGetValue(component.ActorEntity.Network.Channel.Id, out Dictionary<int, BaseActorEntityComponent> dicVal))
                return;

            if (!dicVal.Remove(component.ActorEntity.ActorId))
                return;

            if (!RemoteActors.Remove(component.Id))
                return;

            if (!TypeComponentIdActors.TryGetValue(component.ActorType, out Dictionary<int, BaseActorEntityComponent> typeVal))
                return;

            if (!typeVal.Remove(component.Id))
                return;

            component.Dispose();
        }

        public void RemoveLocal(BaseActorEntityComponent component)
        {
            if (!TypeObjectIdActors.TryGetValue(component.ActorType, out Dictionary<string, BaseActorEntityComponent> dicStrVal))
                return;

            if (!dicStrVal.Remove(component.ActorEntity.Id))
                return;

            if (!LocalActors.Remove(component.Id))
                return;

            if (!TypeComponentIdActors.TryGetValue(component.ActorType, out Dictionary<int, BaseActorEntityComponent> typeVal))
                return;

            if (!typeVal.Remove(component.Id))
                return;

            NotifyAllServerWithRemove(component.ActorEntity, component);

            component.Dispose();
        }

        public void ResponseLocalActors(Network network, int messageCmd)
        {
            var count = 0;
            foreach (var actor in LocalActors.Values)
            {
                actor.SendMySelf(network, messageCmd);
                count++;

                //一次最多发送100条，避免服务端分配过大的缓冲区
                if (count >= 100)
                {
                    network.Channel.StartSend();
                    count = 0;
                }
            }
        }

        public void AddRemote(BaseActorEntityComponent component)
        {
            if (RemoteActors.ContainsKey(component.Id))
                return;

            RemoteActors[component.Id] = component;

            var entity = component.ActorEntity;
            if (!NetIdActors.TryGetValue(entity.Network.Channel.Id, out Dictionary<int, BaseActorEntityComponent> dicVal))
            {
                dicVal = new Dictionary<int, BaseActorEntityComponent>();
                NetIdActors[entity.Network.Channel.Id] = dicVal;
            }
            dicVal[component.ActorEntity.ActorId] = component;

            if (!TypeComponentIdActors.TryGetValue(component.ActorType, out Dictionary<int, BaseActorEntityComponent> typeVal))
            {
                typeVal = new Dictionary<int, BaseActorEntityComponent>();
                TypeComponentIdActors[component.ActorType] = typeVal;
            }
            typeVal[component.Id] = component;

            if (!TypeObjectIdActors.TryGetValue(component.ActorType, out Dictionary<string, BaseActorEntityComponent> dicStrVal))
            {
                dicStrVal = new Dictionary<string, BaseActorEntityComponent>();
                TypeObjectIdActors[component.ActorType] = dicStrVal;
            }
            dicStrVal[component.ActorEntity.Id] = component;
        }

        private void NotifyAllServerWithAdd(ActorEntity entity, BaseActorEntityComponent component)
        {
            var message = new ActorSyncMessage
            {
                ObjectId = entity.Id,
                ActorType = entity.ActorType,
            };
            Game.Scene.GetComponent<DistributionsComponent>().InConnNets.BroadcastActor(message, (int)InnerMessageCMD.AddActorCmd, component.Id);
        }

        private void NotifyAllServerWithRemove(ActorEntity entity, BaseActorEntityComponent component)
        {
            Game.Scene.GetComponent<DistributionsComponent>().InConnNets.BroadcastActor((int)InnerMessageCMD.RemoveActorCmd, component.Id);
        }
    }
}
