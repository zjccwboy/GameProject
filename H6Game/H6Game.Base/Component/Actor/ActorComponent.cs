using H6Game.Entitys;
using H6Game.Entitys.Enums;
using H6Game.Message;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace H6Game.Base
{
    [Event(EventType.Awake)]
    public class ActorComponent : BaseComponent
    {
        private ConcurrentDictionary<long, ActorInfoEntity> EntitiesDictionary { get; } = new ConcurrentDictionary<long, ActorInfoEntity>();
        private ConcurrentDictionary<string, ActorInfoEntity> RemoteEntitiesDictionary { get; } = new ConcurrentDictionary<string, ActorInfoEntity>();
        private ConcurrentDictionary<string, ActorInfoEntity> LocalEntitiesDictionary { get; } = new ConcurrentDictionary<string, ActorInfoEntity>();
        private ConcurrentDictionary<int, HashSet<ActorInfoEntity>> NetChannelIdEntitys { get; } = new ConcurrentDictionary<int, HashSet<ActorInfoEntity>>();
        private InNetComponent InNetComponent { get; set; }

        public ActorType ActorType { get; set; }

        public override void Awake()
        {
            InNetComponent = Game.Scene.GetComponent<InNetComponent>();
            InNetComponent.OnDisConnected += this.OnNetDisconnected;
            InNetComponent.OnConnected += OnNetConnected;
        }

        public IEnumerable<ActorInfoEntity> LocalEntitys { get { return LocalEntitiesDictionary.Values; } }

        public void AddLocalEntity(ActorInfoEntity entity)
        {
            if (LocalEntitiesDictionary.TryAdd(entity.Id, entity))
                NotifyAllServerWithAdd(entity);
        }

        public void AddNetEntity(ActorInfoEntity entity)
        {
            var entityId = entity.GetEntityId();
            EntitiesDictionary.AddOrUpdate(entityId, entity, (k, v) => { return entity; });
            RemoteEntitiesDictionary.AddOrUpdate(entity.Id, entity, (k, v) => { return entity; });

            if(!NetChannelIdEntitys.TryGetValue(entity.Network.Channel.Id, out HashSet<ActorInfoEntity> hashVal))
            {
                hashVal = new HashSet<ActorInfoEntity>();
                NetChannelIdEntitys[entity.Network.Channel.Id] = hashVal;
            }
        }

        public void RemoveFromNet(string objectId)
        {
            if (RemoteEntitiesDictionary.TryRemove(objectId, out ActorInfoEntity actorInfo))
            {
                var entityId = actorInfo.GetEntityId();
                EntitiesDictionary.TryRemove(entityId, out ActorInfoEntity actorInfoEntity);

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
            if (LocalEntitiesDictionary.TryRemove(objectId, out ActorInfoEntity actorInfo))
                NotifyAllServerWithRemove(actorInfo);
        }

        public bool TryGetNetEntity(long entityId, out ActorInfoEntity entity)
        {
            return EntitiesDictionary.TryGetValue(entityId, out entity);
        }

        public bool TryGetNetEntity(string objectId, out ActorInfoEntity entity)
        {
            return RemoteEntitiesDictionary.TryGetValue(objectId, out entity);
        }

        public bool TryGetLocalEntity(string objectId, out ActorInfoEntity entity)
        {
            return LocalEntitiesDictionary.TryGetValue(objectId, out entity);
        }

        private void OnNetConnected(ANetChannel channel)
        {
            channel.Dispatcher.Network.Send((int)MessageCMD.SyncActorInfoCmd);
        }

        private void OnNetDisconnected(ANetChannel channel)
        {
            if(NetChannelIdEntitys.TryRemove(channel.Id, out HashSet<ActorInfoEntity> entities))
            {
                foreach(var entity in entities)
                {
                    RemoteEntitiesDictionary.TryRemove(entity.Id, out ActorInfoEntity value);
                    EntitiesDictionary.TryRemove(entity.GetEntityId(), out value);
                }
            }
        }

        private void NotifyAllServerWithAdd(ActorInfoEntity entity)
        {
            var message = new ActorSyncMessage
            {
                ObjectId = entity.Id,
                ActorType = entity.ActorType,
            };
            InNetComponent.InConnNets.BroadcastActor(message, (int)MessageCMD.AddActorCmd, entity.ActorId);
        }

        private void NotifyAllServerWithRemove(ActorInfoEntity entity)
        {
            var message = new ActorSyncMessage
            {
                ObjectId = entity.Id,
                ActorType = entity.ActorType,
            };
            InNetComponent.InConnNets.BroadcastActor(message, (int)MessageCMD.RemoveActorCmd, entity.ActorId);
        }
    }
}
