using H6Game.Entitys;

namespace H6Game.Base
{
    public sealed class ActorManager
    {
        public void AddRemoteActor(ActorInfoEntity entity)
        {
            var components = Game.Scene.GetComponents<ActorComponent>();
            foreach(var component in components)
            {
                var actorComponent = component as ActorComponent;
                if (actorComponent.ActorType == entity.ActorType)
                    actorComponent.AddNetEntity(entity);
            }
        }

        public void AddLocalAcotr(ActorInfoEntity entity)
        {
            var components = Game.Scene.GetComponents<ActorComponent>();
            foreach (var component in components)
            {
                var actorComponent = component as ActorComponent;
                if (actorComponent.ActorType == entity.ActorType)
                    actorComponent.AddLocalEntity(entity);
            }
        }

        public void RemoveRemoteActor(ActorType type, string objectId, int channelId)
        {
            var components = Game.Scene.GetComponents<ActorComponent>();
            foreach (var component in components)
            {
                var actorComponent = component as ActorComponent;
                if (actorComponent.ActorType == type)
                    actorComponent.RemoveFromNet(objectId, channelId);
            }
        }

        public void RemoveLocalActor(ActorType type, string objectId)
        {
            var components = Game.Scene.GetComponents<ActorComponent>();
            foreach (var component in components)
            {
                var actorComponent = component as ActorComponent;
                if (actorComponent.ActorType == type)
                    actorComponent.RemoveFromLocal(objectId);
            }
        }

        public ActorInfoEntity GetRemoteActor(ActorType type, string objectId)
        {
            var components = Game.Scene.GetComponents<ActorComponent>();
            foreach (var component in components)
            {
                var actorComponent = component as ActorComponent;
                if (actorComponent.ActorType == type)
                {
                    if (!actorComponent.TryGetNetEntity(objectId, out ActorInfoEntity value))
                        throw new System.Exception("无效的ActorType或者objectId.");

                    return value;
                }
            }
            return null;
        }

        public ActorInfoEntity GetLocalActor(ActorType type, string objectId)
        {
            var components = Game.Scene.GetComponents<ActorComponent>();
            foreach (var component in components)
            {
                var actorComponent = component as ActorComponent;
                if (actorComponent.ActorType == type)
                {
                    if (!actorComponent.TryGetLocalEntity(objectId, out ActorInfoEntity value))
                        throw new System.Exception("无效的ActorType或者objectId.");

                    return value;
                }
            }
            return null;
        }

    }
}
