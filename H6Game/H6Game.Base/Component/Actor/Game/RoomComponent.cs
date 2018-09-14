using H6Game.Entities;
using H6Game.Entities.Enums;

namespace H6Game.Base
{
    public class RoomComponent : BaseActorComponent<TRoom>
    {
        public override ActorType ActorType => ActorType.Room;
    }

}
