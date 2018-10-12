using H6Game.Base;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H6Game.Actor
{

    [ComponentEvent(EventType.Awake)]
    [SingleCase]
    public class ActorPoolComponent : NetComponentSubscriber
    {
        private Dictionary<int, Dictionary<int, BaseActorComponent>> NetIdActors { get; } = new Dictionary<int, Dictionary<int, BaseActorComponent>>();
        private Dictionary<ActorType, Dictionary<int, BaseActorComponent>> TypeComponentIdActors { get; } = new Dictionary<ActorType, Dictionary<int, BaseActorComponent>>();
        private Dictionary<ActorType, Dictionary<string, BaseActorComponent>> TypeObjectIdActors { get; } = new Dictionary<ActorType, Dictionary<string, BaseActorComponent>>();
        private Dictionary<ActorType, Type> ActorTypes { get; } = new Dictionary<ActorType, Type>();
        private Dictionary<int, BaseActorComponent> LocalActors { get; } = new Dictionary<int, BaseActorComponent>();
        private Dictionary<int, BaseActorComponent> RemoteActors { get; } = new Dictionary<int, BaseActorComponent>();
        private NetDistributionsComponent Distributions { get; set; }
        public Action<Network> OnDisconnected { get; set; }
        public Action<Network> OnConnected { get; set; }
        public override void Awake()
        {
            var types = ObjectTypeStorage.GetTypes<BaseActor>();
            foreach(var type in types)
            {
                using (var component = ObjectStorage.Fetch(type) as BaseActorComponent)
                {
                    ActorTypes[component.ActorType] = type;
                }
            }

            this.Distributions = Game.Scene.AddComponent<NetDistributionsComponent>();
            this.Distributions.OnDisconnect += network =>
            {
                if (!NetIdActors.TryGetValue(network.Id, out Dictionary<int, BaseActorComponent> dicVal))
                    return;

                this.OnDisconnected?.Invoke(network);

                var acotrs = dicVal.Values.ToList();
                foreach (var acotr in acotrs)
                    acotr.Dispose();
            };

            this.Distributions.OnConnect += network => 
            {
                this.OnConnected?.Invoke(network);
                network.Send((int)NetCommand.SyncActorInfoCmd);
            };
        }

        /// <summary>
        /// 远程服务端新增Actor事件消息
        /// </summary>
        /// <param name="message"></param>
        [NetCommand(NetCommand.AddActorCmd)]
        public void SubscribeOnRemoteAddActor(ActorSyncMessage message)
        {
            var logs = $"CMD:{this.NetMessageCmd} ActorId:{message.ActorId} MSG:{message.ToJson()}";
            Log.Info(logs, LoggerBllType.System);

            var type = ActorTypes[message.ActorType];
            var actor = ObjectStorage.Fetch(type) as BaseActorComponent;
            actor.SetRemote(this.CurrentNetwrok, message.ObjectId, message.ActorId);
            Game.Scene.AddComponent(actor);
        }

        /// <summary>
        /// 远程服务端删除Actor事件消息
        /// </summary>
        /// <param name="message"></param>
        [NetCommand(NetCommand.RemoveActorCmd)]
        public void SubscribeOnRemoteActorRemove(ActorSyncMessage message)
        {
            Log.Info(this.NetMessageCmd, LoggerBllType.System);

            var actor = GetActor(this.CurrentNetwrok.Id, message.ActorId);
            actor.Dispose();
        }

        /// <summary>
        /// 远程服务同步本地服务Actor全量数据消息
        /// </summary>
        [NetCommand(NetCommand.SyncActorInfoCmd)]
        public void SubscribeOnRemoteSyncFullActorInfo()
        {
            Log.Info(this.NetMessageCmd, LoggerBllType.System);

            //回发AMessageCMD.AddActorCmd消息告诉远程订阅服务新增RemoteActor
            var count = 0;
            foreach (var actor in LocalActors.Values)
            {
                actor.SendMySelf(this.CurrentNetwrok, (int)NetCommand.AddActorCmd);
                count++;

                //一次最多发送100条，避免服务端分配过大的缓冲区
                if (count >= 100)
                {
                    this.CurrentNetwrok.Channel.StartSend();
                    count = 0;
                }
            }
        }

        public void AddLocal(BaseActorComponent component)
        {
            if (LocalActors.ContainsKey(component.Id))
                return;

            LocalActors[component.Id] = component;
            if (!TypeComponentIdActors.TryGetValue(component.ActorType, out Dictionary<int,BaseActorComponent > dicVal))
            {
                dicVal = new Dictionary<int, BaseActorComponent>();
                TypeComponentIdActors[component.ActorType] = dicVal;
            }
            dicVal[component.Id] = component;

            if(!TypeObjectIdActors.TryGetValue(component.ActorType, out Dictionary<string, BaseActorComponent> dicStrVal))
            {
                dicStrVal = new Dictionary<string, BaseActorComponent>();
                TypeObjectIdActors[component.ActorType] = dicStrVal;
            }
            dicStrVal[component.ActorEntity.Id] = component;

            NotifyAllServerWithAdd(component.ActorEntity, component);
        }

        public void AddRemote(BaseActorComponent component)
        {
            if (RemoteActors.ContainsKey(component.Id))
                return;

            RemoteActors[component.Id] = component;

            var entity = component.ActorEntity;
            if (!NetIdActors.TryGetValue(entity.Network.Id, out Dictionary<int, BaseActorComponent> dicVal))
            {
                dicVal = new Dictionary<int, BaseActorComponent>();
                NetIdActors[entity.Network.Id] = dicVal;
            }
            dicVal[component.ActorEntity.ActorId] = component;

            if (!TypeComponentIdActors.TryGetValue(component.ActorType, out Dictionary<int, BaseActorComponent> typeVal))
            {
                typeVal = new Dictionary<int, BaseActorComponent>();
                TypeComponentIdActors[component.ActorType] = typeVal;
            }
            typeVal[component.Id] = component;

            if (!TypeObjectIdActors.TryGetValue(component.ActorType, out Dictionary<string, BaseActorComponent> dicStrVal))
            {
                dicStrVal = new Dictionary<string, BaseActorComponent>();
                TypeObjectIdActors[component.ActorType] = dicStrVal;
            }
            dicStrVal[component.ActorEntity.Id] = component;
        }

        public BaseActorComponent GetActor(string objectId, ActorType actorType)
        {
            if (!TypeObjectIdActors.TryGetValue(actorType, out Dictionary<string, BaseActorComponent> dicStrVal))
                return null;

            if (!dicStrVal.TryGetValue(objectId, out BaseActorComponent component))
                return null;

            return component;
        }

        public BaseActorComponent GetActor(int netChannelId, int remoteActorId)
        {
            if (!NetIdActors.TryGetValue(netChannelId, out Dictionary<int, BaseActorComponent> dicVal))
                return null;

            if (!dicVal.TryGetValue(remoteActorId, out BaseActorComponent component))
                return null;

            return component;
        }

        public void RemoveRemote(BaseActorComponent component)
        {
            if (component == null)
                return;

            if (!TypeObjectIdActors.TryGetValue(component.ActorType, out Dictionary<string, BaseActorComponent> dicStrVal))
                return;

            if (!dicStrVal.Remove(component.ActorEntity.Id))
                return;

            if (!NetIdActors.TryGetValue(component.ActorEntity.Network.Id, out Dictionary<int, BaseActorComponent> dicVal))
                return;

            if (!dicVal.Remove(component.ActorEntity.ActorId))
                return;

            if (!RemoteActors.Remove(component.Id))
                return;

            if (!TypeComponentIdActors.TryGetValue(component.ActorType, out Dictionary<int, BaseActorComponent> typeVal))
                return;

            if (!typeVal.Remove(component.Id))
                return;
        }

        public void RemoveLocal(BaseActorComponent component)
        {
            if (component == null)
                return;

            if (!TypeObjectIdActors.TryGetValue(component.ActorType, out Dictionary<string, BaseActorComponent> dicStrVal))
                return;

            if (!dicStrVal.Remove(component.ActorEntity.Id))
                return;

            if (!LocalActors.Remove(component.Id))
                return;

            if (!TypeComponentIdActors.TryGetValue(component.ActorType, out Dictionary<int, BaseActorComponent> typeVal))
                return;

            if (!typeVal.Remove(component.Id))
                return;

            NotifyAllServerWithRemove(component.ActorEntity, component);
        }

        private void NotifyAllServerWithAdd(ActorEntity entity, BaseActorComponent component)
        {
            var message = new ActorSyncMessage
            {
                ActorId = component.Id,
                ObjectId = entity.Id,
                ActorType = entity.ActorType,
            };
            foreach(var network in this.Distributions.InnerNetworks)
            {
                if (this.Distributions.IsProxyServer)
                    continue;

                network.Send(message, (int)NetCommand.AddActorCmd);
            }
        }

        private void NotifyAllServerWithRemove(ActorEntity entity, BaseActorComponent component)
        {
            foreach (var network in this.Distributions.InnerNetworks)
            {
                if (this.Distributions.IsProxyServer)
                    continue;

                network.Send((int)NetCommand.RemoveActorCmd, component.Id);
            }
        }
    }
}
