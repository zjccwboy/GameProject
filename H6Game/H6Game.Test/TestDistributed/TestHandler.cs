using H6Game.Base;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDistributed
{
    [HandlerCMD(MessageCMD.TestCMD1)]
    public class TestHandler : AHandler<TestMessage>
    {
        protected override void Handler(TestMessage message, int messageId)
        {
            CallBack(message.ConvertToBytes());
        }
    }

    public class TestMessage : IMessage
    {
        public int ActorId { get; set; }
        public string Message { get; set; }
    }
}
