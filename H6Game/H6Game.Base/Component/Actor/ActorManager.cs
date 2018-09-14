using H6Game.Entities.Enums;

namespace H6Game.Base
{
    public sealed class ActorManager
    {
        public void AddRemoteActor(ActorEntity entity)
        {
            var actorComponent = GetActorComponent(entity.ActorType);
            actorComponent.AddNetEntity(entity);
        }

        public void AddLocalAcotr(ActorEntity entity)
        {
            var actorComponent = GetActorComponent(entity.ActorType);
            actorComponent.AddLocalEntity(entity);
        }

        public void RemoveActor(ActorType type, string objectId)
        {
            var actorComponent = GetActorComponent(type);
            var entity = GetRemoteActor(type, objectId);
            if (entity != null)
            {
                actorComponent.RemoveFromNet(objectId);
                return;
            }

            entity = GetLocalActor(type, objectId);
            if (entity != null)
            {
                actorComponent.RemoveFromLocal(objectId);
                return;
            }
        }

        public ActorEntity GetRemoteActor(ActorType type, string objectId)
        {
            var actorComponent = GetActorComponent(type);
            if (!actorComponent.TryGetNetEntity(objectId, out ActorEntity value))
                throw new ActorException("无效的ActorType或者objectId.");
            return value;
        }

        public ActorEntity GetLocalActor(ActorType type, string objectId)
        {
            var actorComponent = GetActorComponent(type);
            if (!actorComponent.TryGetLocalEntity(objectId, out ActorEntity value))
                throw new ActorException("无效的ActorType或者objectId.");
            return value;
        }

        private ActorComponent GetActorComponent(ActorType type)
        {
            var components = Game.Scene.GetComponents<ActorComponent>();
            foreach(var component in components)
            {
                var actor = component as ActorComponent;
                if (actor.ActorType == type)
                    return actor;
            }
            return null;
        }
    }
}
