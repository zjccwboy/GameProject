using H6Game.Entities.Enums;

namespace H6Game.Base
{
    public sealed class ActorPoolManager
    {
        public TActorComponent GetActor<TActorComponent>(ActorType type, string objectId) where TActorComponent : BaseActorEntityComponent
        {
            return GetActor(type, objectId) as TActorComponent;
        }

        public BaseActorEntityComponent GetActor(ActorType type, string objectId)
        {
            var pool = GetActorPool(type);
            if (!pool.TryGetComponent(objectId, out BaseActorEntityComponent value))
                throw new ActorException("无效的ActorType或者objectId.");
            return value;
        }

        public ActorPoolComponent GetActorPool(ActorType type)
        {
            var components = Game.Scene.GetComponents<ActorPoolComponent>();
            foreach(var component in components)
            {
                var actor = component as ActorPoolComponent;
                if (actor.ActorType == type)
                    return actor;
            }
            return null;
        }
    }
}
