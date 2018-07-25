using H6Game.Base;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTcpServer
{
    [MessageCMD(MessageCMD.TestCMD)]
    public class TestMessageDispatcher : AMessageDispatcher<IResponse>
    {
        public override void Dispatcher(IResponse response)
        {

        }

        public override void Dispatcher(byte[] bytes)
        {
            this.Session.SendMessage(new Packet
            {
                RpcId = this.RpcId,
                MessageId = (uint)MessageCMD.TestCMD,
                Data = bytes,
            });
        }
    }
}
