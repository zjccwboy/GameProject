using H6Game.Base;
using H6Game.Base.Message;
using H6Game.Hotfix.Entities;

namespace H6Game.Actor
{
    public class GameComponent : BaseActorComponent<TGame>
    {
        public override ActorType ActorType => ActorType.Game;

        public override void ReceiveMessage(IActorMessage message, MSGCommand command)
        {
            throw new System.NotImplementedException();
        }

        public override IActorMessage ReceiveRpcMessage(IActorMessage message, MSGCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}
