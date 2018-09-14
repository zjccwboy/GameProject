using H6Game.Entities;
using H6Game.Entities.Enums;

namespace H6Game.Base
{
    public class GameHandlerComponent : BaseComponent
    {
        public void AddLocal(TGame gameInfo)
        {
            var handler = Game.Scene.AddComponent<GameComponent>();
            handler.Add(gameInfo);
        }

        public void AddRemote(string objectId, Network network)
        {
            Game.Actor.AddRemoteActor(new ActorEntity
            {
                ActorId = this.Id,
                Id = objectId,
                ActorType = ActorType.Game,
                Network = network,
            });
        }

        public void Remove(string objectId)
        {
            Game.Actor.RemoveActor(ActorType.Game, objectId);
        }
    }

    public class GameComponent : BaseComponent<TGame>
    {
        public void Add(TGame gameInfo)
        {
            this.EntityInfo = gameInfo;
            Game.Actor.AddLocalAcotr(new ActorEntity
            {
                ActorId = this.Id,
                Id = gameInfo.Id,
                ActorType = ActorType.Game,
                ActorInfo = gameInfo,
            });
        }
    }
}
