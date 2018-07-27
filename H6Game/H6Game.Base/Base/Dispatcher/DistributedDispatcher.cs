using H6Game.Message;
using H6Game.Message.InNetMessage;

namespace H6Game.Base.Base.Message
{
    [MessageCMD(MessageCMD.AddOneServer)]
    public class DistributedDispatcher : AMessageDispatcher<DistributedMessageRp>
    {
        protected override void Dispatcher(DistributedMessageRp response)
        {

        }
    }
}
