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
            this.Distributions.OnDisconnected += network =>
            {
                if (!NetIdActors.TryGetValue(network.Id, out Dictionary<int, BaseActorComponent> dicVal))
                    return;

                this.OnDisconnected?.Invoke(network);

                var acotrs = dicVal.Values.ToList();
                foreach (var acotr in acotrs)
                    acotr.Dispose();
            };

            this.Distributions.OnConnected += network => 
            {
                this.OnConnected?.Invoke(network);
                network.Send((ushort)MSGCommand.SyncActorInfoCmd);
            };
        }

        internal void SendLocalActors(Network network)
        {
            var count = 0;
            foreach (var actor in LocalActors.Values)
            {
                actor.SendMySelf(network, MSGCommand.AddActorCmd);
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
            if (component.IsLocalActor)
            {
                this.AddLocal(component);
                return;
            }
            this.AddRemote(component);
        }

        private void AddLocal(BaseActorComponent component)
        {
            if (this.LocalActors.ContainsKey(component.Id))
                return;

            this.LocalActors.Add(component.Id, component);

            this.ActorComponents[component.ActorEntity.Id] = component;

            var message = new ActorSyncMessage
            {
                ActorId = component.Id,
                ActorType = component.ActorEntity.ActorType,
            };
            this.Distributions.InnerNetworks.Send(message, (ushort)MSGCommand.AddActorCmd);
        }

        private void AddRemote(BaseActorComponent component)
        {
            if (this.RemoteActors.ContainsKey(component.Id))
                return;

            this.RemoteActors[component.Id] = component;

            var entity = component.ActorEntity;
            if (!this.NetIdActors.TryGetValue(entity.Network.Id, out Dictionary<int, BaseActorComponent> dicVal))
            {
                dicVal = new Dictionary<int, BaseActorComponent>();
                this.NetIdActors[entity.Network.Id] = dicVal;
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
            if (component == null)
                return;

            if (component.ActorEntity == null)
                return;

            if (this.LocalActors.ContainsKey(component.Id))
            {
                RemoveLocal(component);
                return;
            }

            if (this.RemoteActors.ContainsKey(component.Id))
            {
                this.RemoveRemote(component);
            }
        }

        private void RemoveRemote(BaseActorComponent component)
        {
            if (!this.NetIdActors.TryGetValue(component.ActorEntity.Network.Id, out Dictionary<int, BaseActorComponent> dicVal))
                return;

            if (!dicVal.Remove(component.ActorEntity.ActorId))
                return;

            if (!RemoteActors.Remove(component.Id))
                return;
        }

        private void RemoveLocal(BaseActorComponent component)
        {
            if (!this.LocalActors.Remove(component.Id))
                return;

            if (!this.ActorComponents.Remove(component.ActorEntity.Id))
                return;

            this.Distributions.InnerNetworks.Send(component.Id, (ushort)MSGCommand.RemoveActorCmd);
        }
    }
}
