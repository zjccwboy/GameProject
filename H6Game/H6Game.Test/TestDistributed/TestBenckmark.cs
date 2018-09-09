using H6Game.Base;
using H6Game.Message;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDistributed
{
    [Event(EventType.Awake | EventType.Update)]
    [SingletCase]
    public class TestBenckmark : BaseComponent
    {
        private Stopwatch stopWatch = new Stopwatch();
        private int Count = 0;
        private long size = 0;

        public override void Awake()
        {
            stopWatch.Start();
        }

        public override void Update()
        {
            if (Game.Scene.GetComponent<InnerComponent>().IsCenterServer)
                return;

            Benckmark();
        }

        private async void Benckmark()
        {
            await Call();
        }

        TestMessage send = new TestMessage
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


        private async Task Call()
        {
            var inNetComponent = Game.Scene.GetComponent<InnerComponent>();
            if (inNetComponent.IsCenterServer)
                return;

            var networks = inNetComponent.InConnNets;

            foreach (var network in networks)
            {
                await Call(network);
            }
        }

        public async Task Call(Network network)
        {
            var result = await network.CallMessage<TestMessage, TestMessage>(send, (int)MessageCMD.TestCMD1);

            if (!result.Result)
                return;

            Count++;
            size += 35;
            if (stopWatch.ElapsedMilliseconds > 1000)
            {
                Log.Logger.Debug($"RPC响应次数:{Count}/条 大小:{size / 1024 / 1024}/MB");
                stopWatch.Restart();
                Count = 0;
                size = 0;
            }
        }
    }
}
