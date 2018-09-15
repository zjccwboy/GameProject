using H6Game.Entities.Enums;

namespace H6Game.Base
{
    public sealed class ActorPoolManager
    {
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
