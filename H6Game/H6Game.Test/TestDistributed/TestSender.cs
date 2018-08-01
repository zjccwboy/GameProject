using H6Game.Base;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDistributed
{
    public class TestSender : BaseComponent
    {
        private uint time = TimeUitls.Now();

        public override void Start()
        {
            Broadcast();
        }

        InNetComponent inNetComponent = SinglePool.Get<InNetComponent>();
        private async void Broadcast()
        {
            var sessions = inNetComponent.ConnectSessions;
            foreach (var session in sessions)
            {
                var send = new TestMessage { ActorId = 10001, Message = "MessageMessageMessageMessage" };
                var message = await inNetComponent.CallMessage<TestMessage>(session, session.ConnectChannel, send.ConvertToBytes(), (int)MessageCMD.TestCMD1);
                LogRecord.Log(LogLevel.Debug, "RPC消息:", message.ConvertToJson());
            }
        }
    }
}
