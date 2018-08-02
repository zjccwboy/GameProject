using H6Game.Base;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDistributed
{
    public class TestSender : BaseComponent
    {
        InNetComponent inNetComponent = SinglePool.Get<InNetComponent>();
        private Stopwatch stopWatch = new Stopwatch();
        private int Count = 0;
        private long size = 0;
        public TestSender()
        {
            stopWatch.Start();
        }
        public override void Start()
        {
            for(var i=0;i<100;i++)
                Broadcast();
        }

        private void Broadcast()
        {
            if (inNetComponent.IsCenterServer)
                return;

            var sessions = inNetComponent.ConnectSessions;

            if (!sessions.Any())
                return;

            foreach (var session in sessions)
            {
                var send = new TestMessage
                {
                    ActorId = 10001,
                    Message = "MessageMessageMessageMessage"
                };
                inNetComponent.CallMessage<TestMessage>(session, session.ConnectChannel, send.ToBytes(), (int)MessageCMD.TestCMD1,(m)=> 
                {
                    Count++;
                    size += m.ToBytes().Length;
                    if (stopWatch.ElapsedMilliseconds > 1000)
                    {
                        LogRecord.Log(LogLevel.Debug, "RPC响应次数:", $"数量:{Count}/条 大小:{size / 1024 / 1024}/MB");
                        stopWatch.Restart();
                        Count = 0;
                        size = 0;
                    }

                });
            }
        }
    }
}
