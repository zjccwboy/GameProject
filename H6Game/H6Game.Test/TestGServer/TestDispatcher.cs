using H6Game.Base;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGServer
{
    [H6Game.Message.HandlerCMD(1024)]
    public class TestDispatcher : AHandler<TestMessage>
    {
        protected override void Handler(TestMessage message, int messageId)
        {
            //LogRecord.Log(LogLevel.Notice, $"{this.GetType()}/Handler", message.ToJson());
            CallBack(message.ToBytes());
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
