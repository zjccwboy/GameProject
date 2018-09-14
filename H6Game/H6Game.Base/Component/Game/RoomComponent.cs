using H6Game.Entities;
using H6Game.Entities.Enums;

namespace H6Game.Base
{
    public class RoomComponent : BaseComponent
    {
        public void AddLocal(TRoom roomInfo)
        {
            Game.Actor.AddLocalAcotr(new ActorEntity
            {
                ActorId = this.Id,
                Id = roomInfo.Id,
                ActorType = ActorType.Room,
                ActorInfo = roomInfo,
            });
        }

        public void AddRemote(string objectId, Network network)
        {
            Game.Actor.AddRemoteActor(new ActorEntity
            {
                ActorId = this.Id,
                Id = objectId,
                ActorType = ActorType.Room,
                Network = network,
            });
        }

        public void Remove(string objectId)
        {
            Game.Actor.RemoveActor(ActorType.Room, objectId);
        }
    }

}
