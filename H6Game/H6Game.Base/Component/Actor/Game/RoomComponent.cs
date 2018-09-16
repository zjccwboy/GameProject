using H6Game.Hotfix.Entities;
using H6Game.Hotfix.Enums;

namespace H6Game.Base
{
    public class RoomComponent : BaseActorComponent<TRoom>
    {
        public override ActorType ActorType => ActorType.Room;
    }

}
