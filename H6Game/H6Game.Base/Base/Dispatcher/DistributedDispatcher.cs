using H6Game.Message;
using H6Game.Message.InNetMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base.Base.Message
{
    [MessageCMD(MessageCMD.AddOneServer)]
    public class DistributedDispatcher : AMessageDispatcher<DistributedMessageRp>
    {
        public override void Dispatcher(DistributedMessageRp response)
        {

        }
    }
}
