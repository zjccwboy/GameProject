using H6Game.Base;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTcpServer
{
    [HandlerCMD(MessageCMD.TestCMD)]
    public class TestMessageDispatcher : AHandler<string>
    {
        protected override void Handler(string response, int messageId)
        {
            this.CallBack(Encoding.UTF8.GetBytes(response));
        }
    }
}
