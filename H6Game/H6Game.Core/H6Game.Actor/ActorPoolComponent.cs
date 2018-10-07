using H6Game.Base;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H6Game.Actor
{
    /// <summary>
    /// 订阅与处理Actor新增消息
    /// </summary>
    [NetCommand(NetCommand.AddActorCmd)]
    public class AddActorSubscriber : NetSubscriber<ActorSyncMessage>
    {
        protected override void Subscribe(Network network, ActorSyncMessage message, int messageCmd)
        {
            var logs = $"CMD:{messageCmd} ActorId:{message.ActorId} MSG:{message.ToJson()}";
            Log.Info(logs, LoggerBllType.System);

            var type = Game.Scene.GetComponent<ActorPoolComponent>().GetActorType(message.ActorType);
            var actor = ComponentPool.Fetch(type) as BaseActorEntityComponent;
            actor.SetRemote(network, message.ObjectId, message.ActorId);
            Game.Scene.AddComponent(actor);
        }
    }

    /// <summary>
    /// 订阅与处理Actor删除消息
    /// </summary>
    [NetCommand(NetCommand.RemoveActorCmd)]
    public class ActorRemoveSubscriber : NetSubscriber<ActorSyncMessage>
    {
        protected override void Subscribe(Network network, ActorSyncMessage message, int messageCmd)
        {
            Log.Info(messageCmd, LoggerBllType.System);

            var pool = Game.Scene.GetComponent<ActorPoolComponent>();
            var actor = pool.GetActor(network.Channel.Id, message.ActorId);
            pool.RemoveRemote(actor);
        }
    }

    /// <summary>
    /// 订阅与回发全量的本地LocalActor信息
    /// </summary>
    [NetCommand(NetCommand.SyncActorInfoCmd)]
    public class SyncFullActorSubscriber : NetSubscriber
    {
        protected override void Subscribe(Network network, int messageCmd)
        {
            Log.Info(messageCmd, LoggerBllType.System);

            //回发AMessageCMD.AddActorCmd消息告诉远程订阅服务新增RemoteActor
            Game.Scene.GetComponent<ActorPoolComponent>().ResponseLocalActors(network, (int)NetCommand.AddActorCmd);
        }
    }

    [ComponentEvent(EventType.Awake)]
    [SingleCase]
    public class ActorPoolComponent : BaseComponent
    {
        private Dictionary<int, Dictionary<int, BaseActorEntityComponent>> NetIdActors { get; } = new Dictionary<int, Dictionary<int, BaseActorEntityComponent>>();
        private Dictionary<ActorType, Dictionary<int, BaseActorEntityComponent>> TypeComponentIdActors { get; } = new Dictionary<ActorType, Dictionary<int, BaseActorEntityComponent>>();
        private Dictionary<ActorType, Dictionary<string, BaseActorEntityComponent>> TypeObjectIdActors { get; } = new Dictionary<ActorType, Dictionary<string, BaseActorEntityComponent>>();
        private Dictionary<ActorType, Type> ActorTypes { get; } = new Dictionary<ActorType, Type>();
        private Dictionary<int, BaseActorEntityComponent> LocalActors { get; } = new Dictionary<int, BaseActorEntityComponent>();
        private Dictionary<int, BaseActorEntityComponent> RemoteActors { get; } = new Dictionary<int, BaseActorEntityComponent>();
        public Action<ANetChannel> OnDisconnected { get; set; }
        public Action<ANetChannel> OnConnected { get; set; }
        public override void Awake()
        {
            var types = ObjectPool.GetTypes<BaseActorEntity>();
            foreach(var type in types)
            {
                using (var component = ComponentPool.Fetch(type) as BaseActorEntityComponent)
                {
                    ActorTypes[component.ActorType] = type;
                }
            }

            var innerComponent = Game.Scene.AddComponent<NetDistributionsComponent>();            
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
                c.Network.Send((int)NetCommand.SyncActorInfoCmd);
            };
        }

        public Type GetActorType(ActorType type)
        {
            return ActorTypes[type];
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

        public BaseActorEntityComponent GetActor(string objectId, ActorType actorType)
        {
            if (!TypeObjectIdActors.TryGetValue(actorType, out Dictionary<string, BaseActorEntityComponent> dicStrVal))
                return null;

            if (!dicStrVal.TryGetValue(objectId, out BaseActorEntityComponent component))
                return null;

            return component;
        }

        public BaseActorEntityComponent GetActor(int netChannelId, int remoteActorId)
        {
            if (!NetIdActors.TryGetValue(netChannelId, out Dictionary<int, BaseActorEntityComponent> dicVal))
                return null;

            if (!dicVal.TryGetValue(remoteActorId, out BaseActorEntityComponent component))
                return null;

            return component;
        }

        public BaseActorEntityComponent GetActor(int componentId)
        {
            Game.Scene.GetComponent(componentId, out BaseComponent component);
            return component as BaseActorEntityComponent;
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

        private void NotifyAllServerWithAdd(ActorEntity entity, BaseActorEntityComponent component)
        {
            var message = new ActorSyncMessage
            {
                ActorId = component.Id,
                ObjectId = entity.Id,
                ActorType = entity.ActorType,
            };
            Game.Scene.GetComponent<NetDistributionsComponent>().InConnNets.Broadcast(message, (int)NetCommand.AddActorCmd);
        }

        private void NotifyAllServerWithRemove(ActorEntity entity, BaseActorEntityComponent component)
        {
            Game.Scene.GetComponent<NetDistributionsComponent>().InConnNets.Broadcast((int)NetCommand.RemoveActorCmd, component.Id);
        }
    }
}
