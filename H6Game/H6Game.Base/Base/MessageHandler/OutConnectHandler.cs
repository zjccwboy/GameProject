using System;
using System.Collections.Generic;
using System.Text;
using H6Game.Message;

namespace H6Game.Base
{
    [MessageCMD((int)MessageCMD.GetGateEndPoint)]
    public class OutConnectHandler : AHandler<NetEndPointMessage>
    {
        protected override void Handler(NetEndPointMessage response, int messageId)
        {
            throw new NotImplementedException();
        }
    }
}
