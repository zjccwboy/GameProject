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
    public static class TestBenckmark
    {
        private static Stopwatch stopWatch = new Stopwatch();
        private static int Count = 0;
        private static long size = 0;

        public static void Start()
        {
            for (var i = 0; i < 100; i++)
                Game.Update();

            stopWatch.Start();

            if (Game.Scene.GetComponent<InnerComponent>().IsCenterServer)
                return;

            for (var i = 0; i < 100; i++)
                Benckmark();
        }

        private static async void Benckmark()
        {
            await Call();
        }

        static TestMessage send = new TestMessage
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


        private static async Task Call()
        {
            var inNetComponent = Game.Scene.GetComponent<InnerComponent>();
            if (inNetComponent.IsCenterServer)
                return;

            var networks = inNetComponent.InConnNets;

            foreach (var network in networks)
            {
                await StartCall(network);
            }
        }

        public static async Task StartCall(Network network)
        {
            for (var i = 0; i < 10000000; i++)
                await Call(network);
        }

        public static async Task Call(Network network)
        {
            await network.CallMessage<TestMessage, TestMessage>(send, (int)MessageCMD.TestCMD1);

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
