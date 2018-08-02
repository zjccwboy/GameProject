using H6Game.Base;
using H6Game.Message;
using System;
using System.Collections.Generic;
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
            Game.Add<ConfigNetComponent>();
            Game.Add<OutNetComponent>();
            Game.Add<Scene>();

            while (true)
            {
                var send = new TestMessage
                {
                    Actor = 1020201,
                    Message = "我是客户端",
                };
                Game.Get<OutNetComponent>().CallMessage<TestMessage>(send.ToBytes(), 1024, (p) =>
                {
                    LogRecord.Log(LogLevel.Debug, "测试:", p.ToJson());
                });

                Game.Update();
                Thread.Sleep(1);
            }
        }
    }

    [ProtoBuf.ProtoContract]
    public class TestMessage : IMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public int Actor { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public string Message { get; set; }
    }

}
