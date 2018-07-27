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
    public class TestMessageDispatcher : AMessageDispatcher<string>
    {
        protected override void Dispatcher(string response)
        {
            this.Session.Notice(this.Channel, new Packet
            {
                RpcId = this.RpcId,
                MessageId = (int)MessageCMD.TestCMD,
                Data = Encoding.UTF8.GetBytes(response),
            });
        }
    }
}
