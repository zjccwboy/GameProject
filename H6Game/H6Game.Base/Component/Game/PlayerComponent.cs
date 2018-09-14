using H6Game.Entities;
using H6Game.Entities.Enums;

namespace H6Game.Base
{
    public class PlayerHandlerComponent : BaseComponent
    {
        public void AddLocal(TAccount accountInfo)
        {
            var handler = Game.Scene.AddComponent<PlayerComponent>();
            handler.Add(accountInfo);
        }

        public void AddRemote(string objectId, Network network)
        {
            Game.Actor.AddRemoteActor(new ActorEntity
            {
                ActorId = this.Id,
                Id = objectId,
                ActorType = ActorType.Player,
                Network = network,
            });
        }

        public void Remove(string objectId)
        {
            Game.Actor.RemoveActor(ActorType.Player, objectId);
        }
    }

    public class PlayerComponent : BaseComponent<TAccount>
    {
        public void Add(TAccount accountInfo)
        {
            this.EntityInfo = accountInfo;
            Game.Actor.AddLocalAcotr(new ActorEntity
            {
                ActorId = this.Id,
                Id = accountInfo.Id,
                ActorType = ActorType.Player,
                ActorInfo = accountInfo,
            });
        }
    }
}
