using H6Game.Base;
using H6Game.Base.Component;
using H6Game.Base.Config;
using H6Game.Base.Logger;
using H6Game.Base.Message;
using H6Game.Hotfix.Enums;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace H6Game.TestKcpClientBenckmarkCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<NetConnectorComponent>().OnConnect = (c, t) => { Start(); };
            Game.Start();

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
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
            Swatch.Start();

            for (var i = 0; i < 2000; i++)
                Call();
        }

        private static async void Call()
        {
            while (true)
            {
                var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

                var result = await network.CallMessageAsync<TestMessage, TestMessage>(send, 1024);
                if (result.Actor != send.Actor && result.Message != send.Message)
                {
                    Log.Error($"解包出错。", LoggerBllType.System);
                }

                Count++;
                if (Swatch.ElapsedMilliseconds >= 1000)
                {
                    Log.Info($"耗时:{Swatch.ElapsedMilliseconds}/ms RPS:{Count}", LoggerBllType.System);
                    Swatch.Restart();
                    Count = 0;
                }
            }
        }
    }


    [ProtoBuf.ProtoContract]
    [NetMessageType(MessageType.TestGServerTestMessage)]
    public class TestMessage : IMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public int Actor { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public string Message { get; set; }
    }
}
