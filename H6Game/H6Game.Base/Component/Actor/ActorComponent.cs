using H6Game.Entitys;
using H6Game.Message;
using System.Collections.Concurrent;

namespace H6Game.Base
{
    [SingletCase]
    [Event(EventType.Awake)]
    public class ActorComponent : BaseComponent
    {
        private ConcurrentDictionary<long, ActorInfoEntity> EntitiesDictionary { get; } = new ConcurrentDictionary<long, ActorInfoEntity>();
        private ConcurrentDictionary<string, ActorInfoEntity> ObjectIdEntitiesDictionary { get; } = new ConcurrentDictionary<string, ActorInfoEntity>();
        private ConcurrentDictionary<string, ActorInfoEntity> LocalEntitiesDictionary { get; } = new ConcurrentDictionary<string, ActorInfoEntity>();
        private InNetComponent InNetComponent { get; set; }

        public override void Awake()
        {
            InNetComponent = Game.Scene.GetComponent<InNetComponent>();
        }


        public void AddLocalEntity(ActorInfoEntity entity)
        {
            if (LocalEntitiesDictionary.TryAdd(entity.Id, entity))
                NotifyAllServerAdd(entity);
        }

        public void AddNetEntity(ActorInfoEntity entity)
        {
            var entityId = entity.GetEntityId();
            EntitiesDictionary.AddOrUpdate(entityId, entity, (k, v) => { return entity; });
            ObjectIdEntitiesDictionary.AddOrUpdate(entity.Id, entity, (k, v) => { return entity; });
        }

        public void RemoveFromNet(string objectId)
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

        private void NotifyAllServerAdd(ActorInfoEntity entity)
        {
            var networks = InNetComponent.InAccNets;
            var message = new ActorMessage
            {
                ObjectId = entity.Id,
            };
            networks.BroadcastActor(message, (int)MessageCMD.AddActorCmd, entity.ActorId);
        }

        private void NotifyAllServerRemove(ActorInfoEntity entity)
        {
            var networks = InNetComponent.InAccNets;
            var message = new ActorMessage
            {
                ObjectId = entity.Id,
            };
            networks.BroadcastActor(message, (int)MessageCMD.RemoveActorCmd, entity.ActorId);
        }
    }
}
