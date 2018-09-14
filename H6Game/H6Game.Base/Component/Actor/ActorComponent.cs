using H6Game.Entities.Enums;
using H6Game.Message;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace H6Game.Base
{
    [Event(EventType.Awake)]
    public class ActorComponent : BaseComponent
    {
        private ConcurrentDictionary<long, ActorEntity> EntitiesDictionary { get; } = new ConcurrentDictionary<long, ActorEntity>();
        private ConcurrentDictionary<string, ActorEntity> RemoteEntitiesDictionary { get; } = new ConcurrentDictionary<string, ActorEntity>();
        private ConcurrentDictionary<string, ActorEntity> LocalEntitiesDictionary { get; } = new ConcurrentDictionary<string, ActorEntity>();
        private ConcurrentDictionary<int, HashSet<ActorEntity>> NetChannelIdEntitys { get; } = new ConcurrentDictionary<int, HashSet<ActorEntity>>();
        public ActorType ActorType { get; set; }

        public override void Awake()
        {
            var innerComponent = Game.Scene.GetComponent<InnerComponent>();

            innerComponent.OnDisConnected += (channel) =>
            {
                if (NetChannelIdEntitys.TryRemove(channel.Id, out HashSet<ActorEntity> entities))
                {
                    foreach (var entity in entities)
                    {
                        RemoteEntitiesDictionary.TryRemove(entity.Id, out ActorEntity value);
                        EntitiesDictionary.TryRemove(entity.GetEntityId(), out value);
                    }
                }
            };

            innerComponent.OnConnected += (channel) => { channel.Network.Send((int)MessageCMD.SyncActorInfoCmd); };
        }

        public IEnumerable<ActorEntity> LocalEntitys { get { return LocalEntitiesDictionary.Values; } }

        public void AddLocalEntity(ActorEntity entity)
        {
            if (LocalEntitiesDictionary.TryAdd(entity.Id, entity))
                NotifyAllServerWithAdd(entity);
        }

        public void AddNetEntity(ActorEntity entity)
        {
            var entityId = entity.GetEntityId();
            EntitiesDictionary.AddOrUpdate(entityId, entity, (k, v) => { return entity; });
            RemoteEntitiesDictionary.AddOrUpdate(entity.Id, entity, (k, v) => { return entity; });

            if(!NetChannelIdEntitys.TryGetValue(entity.Network.Channel.Id, out HashSet<ActorEntity> hashVal))
            {
                hashVal = new HashSet<ActorEntity>();
                NetChannelIdEntitys[entity.Network.Channel.Id] = hashVal;
            }
        }

        public void RemoveFromNet(string objectId)
        {
            if (RemoteEntitiesDictionary.TryRemove(objectId, out ActorEntity actorInfo))
            {
                var entityId = actorInfo.GetEntityId();
                EntitiesDictionary.TryRemove(entityId, out ActorEntity actorInfoEntity);

                var message = new ActorSyncMessage
                {
                    ObjectId = actorInfo.Id,
                    ActorType = actorInfo.ActorType,
                };
                actorInfo.Network.SendActor(message, (int)MessageCMD.RemoveActorCmd, actorInfo.ActorId);
            }
        }

        public void RemoveFromLocal(string objectId)
        {
            if (LocalEntitiesDictionary.TryRemove(objectId, out ActorEntity actorInfo))
                NotifyAllServerWithRemove(actorInfo);
        }

        public bool TryGetNetEntity(long entityId, out ActorEntity entity)
        {
            return EntitiesDictionary.TryGetValue(entityId, out entity);
        }

        public bool TryGetNetEntity(string objectId, out ActorEntity entity)
        {
            return RemoteEntitiesDictionary.TryGetValue(objectId, out entity);
        }

        public bool TryGetLocalEntity(string objectId, out ActorEntity entity)
        {
            return LocalEntitiesDictionary.TryGetValue(objectId, out entity);
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
