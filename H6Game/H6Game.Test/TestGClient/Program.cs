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
            Game.Scene.AddComponent<OutNetComponent>();
            Swatch.Start();
            while (true)
            {
                Game.Update();
                Test();
                Thread.Sleep(1);
            }
        }

        static int Count;
        static Stopwatch Swatch = new Stopwatch();

        static async void Test()
        {
            for (var i = 0; i < 100; i++)
               await  TestCallBack();
        }

        static async Task TestCallBack()
        {
            var network = Game.Scene.GetComponent<OutNetComponent>().Network;
            if (network == null)
                return;

            TestMessage send = new TestMessage
            {
                Actor = 1020201,
                Message = "我是客户端",
            };

            var result = await network.CallMessage<TestMessage, TestMessage>(send, 1024);
            if (!result.Result)
                return;

                Count++;

            if (result.Content.Actor != 1020201)
                Console.WriteLine("解包错误!");

            if(Swatch.ElapsedMilliseconds >= 1000)
            {
                LogRecord.Log(LogLevel.Info, "CallBack", $"耗时:{Swatch.ElapsedMilliseconds}/ms RPS:{Count}");
                Swatch.Restart();
                Count = 0;
            }
            //LogRecord.Log(LogLevel.Info, "CallBack", result.Content.ToJson());
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
