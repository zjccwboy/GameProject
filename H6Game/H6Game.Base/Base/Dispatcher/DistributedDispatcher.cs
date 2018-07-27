﻿using H6Game.Message;
using H6Game.Message.InNetMessage;
using System.Collections.Generic;

namespace H6Game.Base.Base.Message
{
    [MessageCMD((int)MessageCMD.AddOneServer, (int)MessageCMD.DeleteOneServer,(int)MessageCMD.UpdateInNetonnections)]
    public class DistributedDispatcher : AMessageDispatcher<DistributedMessage>
    {
        protected override void Dispatcher(DistributedMessage response, int messageId)
        {
            switch ((MessageCMD)MessageId)
            {
                case MessageCMD.AddOneServer:
                    break;
                case MessageCMD.DeleteOneServer:
                    break;
                case MessageCMD.UpdateInNetonnections:
                    break;
            }
        }
    }
}