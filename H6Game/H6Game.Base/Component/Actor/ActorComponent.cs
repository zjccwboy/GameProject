using H6Game.Message;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace H6Game.Base
{
    [SingletCase]
    [Event(EventType.Awake)]
    public class ActorComponent : BaseComponent
    {
        private ConcurrentDictionary<long, ActorInfoEntity> EntitiesDictionary { get; } = new ConcurrentDictionary<long, ActorInfoEntity>();
        private ConcurrentDictionary<string, ActorInfoEntity> ObjectIdEntitiesDictionary { get; } = new ConcurrentDictionary<string, ActorInfoEntity>();
        private ConcurrentDictionary<string, ActorInfoEntity> LocalEntitiesDictionary { get; } = new ConcurrentDictionary<string, ActorInfoEntity>();
        private ConcurrentDictionary<int, HashSet<ActorInfoEntity>> NetChannelIdEntitys { get; } = new ConcurrentDictionary<int, HashSet<ActorInfoEntity>>();
        private InNetComponent InNetComponent { get; set; }

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
                NotifyAllServerAdd(entity);
        }

        public void AddNetEntity(ActorInfoEntity entity, int channelId)
        {
            var entityId = entity.GetEntityId();
            EntitiesDictionary.AddOrUpdate(entityId, entity, (k, v) => { return entity; });
            ObjectIdEntitiesDictionary.AddOrUpdate(entity.Id, entity, (k, v) => { return entity; });

            if(!NetChannelIdEntitys.TryGetValue(channelId, out HashSet<ActorInfoEntity> hashVal))
            {
                hashVal = new HashSet<ActorInfoEntity>();
                NetChannelIdEntitys[channelId] = hashVal;
            }
        }

        public void RemoveFromNet(string objectId, int channelId)
        {
            if (ObjectIdEntitiesDictionary.TryRemove(objectId, out ActorInfoEntity actorInfo))
            {
                var entityId = actorInfo.GetEntityId();
                EntitiesDictionary.TryRemove(entityId, out ActorInfoEntity actorInfoEntity);
            }
        }

        public void RemoveFromLocal(string objectId)
        {
            if (LocalEntitiesDictionary.TryRemove(objectId, out ActorInfoEntity actorInfo))
                NotifyAllServerRemove(actorInfo);
        }

        public bool TryGetNetEntity(long entityId, out ActorInfoEntity entity)
        {
            return EntitiesDictionary.TryGetValue(entityId, out entity);
        }

        public bool TryGetNetEntity(string objectId, out ActorInfoEntity entity)
        {
            return ObjectIdEntitiesDictionary.TryGetValue(objectId, out entity);
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
                    ObjectIdEntitiesDictionary.TryRemove(entity.Id, out ActorInfoEntity value);
                    EntitiesDictionary.TryRemove(entity.GetEntityId(), out value);
                }
            }
        }

        private void NotifyAllServerAdd(ActorInfoEntity entity)
        {
            var message = new ActorMessage
            {
                ObjectId = entity.Id,
            };
            InNetComponent.InConnNets.BroadcastActor(message, (int)MessageCMD.AddActorCmd, entity.ActorId);
        }

        private void NotifyAllServerRemove(ActorInfoEntity entity)
        {
            var message = new ActorMessage
            {
                ObjectId = entity.Id,
            };
            InNetComponent.InConnNets.BroadcastActor(message, (int)MessageCMD.RemoveActorCmd, entity.ActorId);
        }
    }
}
