using H6Game.Base;
using H6Game.Base.Component;
using H6Game.Base.Logger;
using H6Game.Base.Message;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H6Game.Actor
{

    [ComponentEvent(EventType.Awake)]
    [SingleCase]
    public class ActorComponentStorage : BaseComponent
    {
        private Dictionary<ActorType, Type> ActorTypes { get; } = new Dictionary<ActorType, Type>();
        private Dictionary<int, Dictionary<int, BaseActorComponent>> NetIdActors { get; } = new Dictionary<int, Dictionary<int, BaseActorComponent>>();
        private Dictionary<int, BaseActorComponent> RemoteActors { get; } = new Dictionary<int, BaseActorComponent>();
        private Dictionary<int, BaseActorComponent> LocalActors { get; } = new Dictionary<int, BaseActorComponent>();
        private Dictionary<string, BaseActorComponent> ActorComponents { get; } = new Dictionary<string, BaseActorComponent>();
        private NetDistributionsComponent Distributions { get; set; }
        private Action<Network> OnDisconnected { get; set; }
        private Action<Network> OnConnected { get; set; }

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
                network.Send((ushort)NetCommand.SyncActorInfoCmd);
            };
        }

        internal void SendLocalActors(Network network)
        {
            var count = 0;
            foreach (var actor in LocalActors.Values)
            {
                actor.SendMySelf(network, NetCommand.AddActorCmd);
                count++;

                //一次最多发送100条，避免服务端分配过大的缓冲区
                if (count >= 100)
                {
                    network.Channel.StartSend();
                    count = 0;
                }
            }
        }

        internal Type GetActorType(ActorType actorType)
        {
            return this.ActorTypes[actorType];
        }

        internal void AddActor(BaseActorComponent component)
        {
            this.ActorComponents[component.ActorEntity.Id] = component;

            if (component.IsLocalActor)
            {
                AddLocal(component);
                return;
            }
            AddRemote(component);
        }

        private void AddLocal(BaseActorComponent component)
        {
            if (LocalActors.ContainsKey(component.Id))
                return;

            LocalActors.Add(component.Id, component);

            var message = new ActorSyncMessage
            {
                ActorId = component.Id,
                ObjectId = component.ActorEntity.Id,
                ActorType = component.ActorEntity.ActorType,
            };
            this.Distributions.InnerNetworks.Broadcast(message, (ushort)NetCommand.AddActorCmd);
        }

        private void AddRemote(BaseActorComponent component)
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
        }

        public BaseActorComponent GetActor(string objectId)
        {
            if (!this.ActorComponents.TryGetValue(objectId, out BaseActorComponent component))
                return null;

            return component;
        }

        public BaseActorComponent GetActor(int actorId)
        {
            if(!Game.Scene.GetComponent(actorId, out BaseComponent component))
            {
                return null;
            }

            return component as BaseActorComponent;
        }

        internal void Remove(BaseActorComponent component)
        {
            if (component.ActorEntity == null)
                return;

            if (component.ActorEntity.Id == null)
                return;

            this.ActorComponents.Remove(component.ActorEntity.Id);

            if (component.IsLocalActor)
            {
                RemoveLocal(component);
                return;
            }
            RemoveRemote(component);
        }

        private void RemoveRemote(BaseActorComponent component)
        {
            if (component == null)
                return;

            if (!NetIdActors.TryGetValue(component.ActorEntity.Network.Id, out Dictionary<int, BaseActorComponent> dicVal))
                return;

            if (!dicVal.Remove(component.ActorEntity.ActorId))
                return;

            if (!RemoteActors.Remove(component.Id))
                return;
        }

        private void RemoveLocal(BaseActorComponent component)
        {
            if (component == null)
                return;


            if (!LocalActors.Remove(component.Id))
                return;

            this.Distributions.InnerNetworks.Broadcast(component.Id, (ushort)NetCommand.RemoveActorCmd);
        }
    }
}
