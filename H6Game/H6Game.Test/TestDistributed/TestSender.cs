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
            for(var i=0;i<50;i++)
                Broadcast();
        }

        private void Broadcast()
        {
            if (inNetComponent.IsCenterServer)
                return;

            var networks = inNetComponent.InConnNets;

            if (!networks.Any())
                return;

            var send = new TestMessage
            {
                ActorId = 10001,
                Message = "Message",
                LongData = 29999,
                ULongData = 30000011,
                ByteData = 200,
                SByteData = 80,
                UIntData = 191919191,
                ListIntData = new List<int> { 1, 2, 3, 4, 5, 6, },
            };

            foreach (var network in networks)
            {
                network.CallRpc(send, (m) =>
                {
                    if (m == null)
                        return;

                    Count++;
                    size += 35;
                    if (stopWatch.ElapsedMilliseconds > 1000)
                    {
                        LogRecord.Log(LogLevel.Debug, "RPC响应次数:", $"数量:{Count}/条 大小:{size / 1024 / 1024}/MB");
                        LogRecord.Log(LogLevel.Debug, "接收到数据:", $"JSON:{send.ToJson()}");
                        stopWatch.Restart();
                        Count = 0;
                        size = 0;
                    }
                },(int)MessageCMD.TestCMD1);
            }
        }
    }
}
