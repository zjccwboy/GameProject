using H6Game.Entities;
using H6Game.Entities.Enums;

namespace H6Game.Base
{
    public class GameComponent : BaseComponent
    {
        public void AddLocal(TGame gameInfo)
        {
            Game.Actor.AddLocalAcotr(new ActorEntity
            {
                ActorId = this.Id,
                Id = gameInfo.Id,
                ActorType = ActorType.Game,
                ActorInfo = gameInfo,
            });
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
}
