using H6Game.Base;
using H6Game.Message;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestGClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Init();
            Game.Scene.AddComponent<OuterComponent>();
            Swatch.Start();
            while (true)
            {
                Game.Update();
                Test();
                Benchmark();
                Thread.Sleep(1);
            }
        }

        static int Count;
        static int CallCount;
        static int BackCount;
        static Stopwatch Swatch = new Stopwatch();

        static TestMessage send = new TestMessage
        {
            Actor = 1020201,
            Message = "我是客户端",
        };

        static async void Test()
        {
            for (var i = 0; i < 50; i++)
               await  TestCallBack();
        }

        static async Task TestCallBack()
        {
            CallCount++;

            var network = Game.Scene.GetComponent<OuterComponent>().Network;
            if (network == null)
                return;
            
            var result = await network.CallMessage<TestMessage, TestMessage>(send, 1024);
            if (!result.Result)
                return;

            Count++;
            BackCount++;

            if (result.Content.Actor != 1020201)
                Console.WriteLine("解包错误!");

            //LogRecord.Log(LogLevel.Info, "CallBack", result.Content.ToJson());
        }

        static void Benchmark()
        {
            if (Swatch.ElapsedMilliseconds >= 1000)
            {
                LogRecord.Log(LogLevel.Info, $"耗时:{Swatch.ElapsedMilliseconds}/ms RPS:{Count} Call:{CallCount} Back:{BackCount}");
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
