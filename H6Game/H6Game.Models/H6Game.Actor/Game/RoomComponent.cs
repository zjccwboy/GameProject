
using H6Game.Hotfix.Entities;

namespace H6Game.Actor
{
    public class RoomComponent : BaseActorComponent<TRoom>
    {
        public override ActorType ActorType => ActorType.Room;
    }

}
