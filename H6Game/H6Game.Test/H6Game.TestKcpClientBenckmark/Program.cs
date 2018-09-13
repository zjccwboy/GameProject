using H6Game.Base;
using H6Game.Message;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace H6Game.TestKcpClientBenckmark
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<OuterComponent>();
            Start();
            while (true)
            {
                Game.Update();
            }
        }

        static int Count;
        static Stopwatch Swatch = new Stopwatch();
        static TestMessage send = new TestMessage
        {
            Actor = 1020201,
            Message = "我是客户端",
        };

        static void Start()
        {
            for (var i = 0; i < 2000; i++)
                Game.Update();

            Swatch.Start();

            for (var i = 0; i < 2000; i++)
                Test();
        }

        static async void Test()
        {
            while(true)
               await Call();
        }

        private static async Task Call()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;
            await Call(network);
        }

        private static async Task Call(Network network)
        {
            await network.CallMessage<TestMessage, TestMessage>(send, 1024);

            if (Thread.CurrentThread.ManagedThreadId != 1)
                Console.WriteLine("Thread.CurrentThread.ManagedThreadId");

            Count++;
            if (Swatch.ElapsedMilliseconds >= 1000)
            {
                Log.Logger.Info($"耗时:{Swatch.ElapsedMilliseconds}/ms RPS:{Count}");
                Swatch.Restart();
                Count = 0;
            }
        }
    }


    [ProtoBuf.ProtoContract]
    [MessageType(MessageType.TestGServerTestMessage)]
    public class TestMessage : IMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public int Actor { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public string Message { get; set; }
    }

}
