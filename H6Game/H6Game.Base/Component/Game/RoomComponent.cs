using H6Game.Entities;
using H6Game.Entities.Enums;

namespace H6Game.Base
{
    public class RoomHandlerComponent : BaseComponent
    {
        public void AddLocal(TRoom roomInfo)
        {
            var handler = Game.Scene.AddComponent<RoomComponent>();
            handler.AddLocal(roomInfo);
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

    public class RoomComponent : BaseComponent<TRoom>
    {
        public void AddLocal(TRoom roomInfo)
        {
            this.EntityInfo = roomInfo;
            Game.Actor.AddLocalAcotr(new ActorEntity
            {
                ActorId = this.Id,
                Id = roomInfo.Id,
                ActorType = ActorType.Room,
                ActorInfo = roomInfo,
            });
        }
    }

}
