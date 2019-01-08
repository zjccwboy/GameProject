using H6Game.Base.Message;
using H6Game.Hotfix.Entities;

namespace H6Game.Actor
{
    public class RoomComponent : BaseActorComponent<TRoom>
    {
        public override ActorType ActorType => ActorType.Room;

        public override void ReceiveMessage(IActorMessage message)
        {
            throw new System.NotImplementedException();
        }

        public override IActorMessage ReceiveRpcMessage(IActorMessage message)
        {
            throw new System.NotImplementedException();
        }
    }

}
