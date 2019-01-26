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
            Game.Scene.AddComponent<NetConnectorComponent>().OnConnected = (c, t) => { Start(); };
            Game.Start();

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }

        static int CallCount;
        static int SendSn;
        static int SN;
        static Stopwatch Swatch = new Stopwatch();

        static void Start()
        {
            Swatch.Start();

            for (var i = 0; i < 500; i++)
                Call();
        }

        private static async void Call()
        {
            for(var i=0;i<10000000;i++)
            {
                var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

                var send = new TestMessage
                {
                    Actor = SendSn,
                    Message = "我是客户端",
                };
                SendSn++;
                var result = await network.CallMessageAsync<TestMessage, TestMessage>(send, 1024);

                if (result.Actor != send.Actor && result.Message != send.Message)
                {
                    Log.Error($"解包出错。", LoggerBllType.System);
                }
                CallCount++;
                if (Swatch.ElapsedMilliseconds >= 1000)
                {
                    SN++;
                    Log.Info($"{SN}耗时:{Swatch.ElapsedMilliseconds}/ms RPS:{CallCount}", LoggerBllType.System);
                    Swatch.Restart();
                    CallCount = 0;
                    return;
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
