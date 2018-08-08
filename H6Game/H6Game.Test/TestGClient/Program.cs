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
            Game.Add<OutNetComponent>();
            while (true)
            {
                Game.Update();
                TestCallBack();
                Thread.Sleep(1);
            }
        }

        static async void TestCallBack()
        {
            var send = new TestMessage
            {
                Actor = 1020201,
                Message = "我是客户端",
            };
            var result = await Game.Get<OutNetComponent>().CallMessage(send, 1024);
            LogRecord.Log(LogLevel.Info, "CallBack", result.ToJson());
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
