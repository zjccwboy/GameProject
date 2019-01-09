using H6Game.Base;
using H6Game.Base.Message;
using H6Game.Hotfix.Entities;

namespace H6Game.Actor
{
    public class PlayerComponent : BaseActorComponent<TAccount>
    {
        public override ActorType ActorType => ActorType.Player;

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
