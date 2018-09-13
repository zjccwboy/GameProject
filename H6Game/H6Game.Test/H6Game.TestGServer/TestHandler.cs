using H6Game.Base;
using H6Game.Message;
using System;

namespace TestGServer
{
    [HandlerCMD(1024)]
    public class TestHandler : AHandler<TestMessage>
    {
        protected override void Handler(Network network, TestMessage message)
        {
            network.RpcCallBack(message);
        }
    }

    [HandlerCMD(1024)]
    public class TestHandlerInt : AHandler<int>
    {
        protected override void Handler(Network network, int message)
        {
            if(message != 1024)
            {
                Console.WriteLine("TestHandlerInt:解包出错");
            }

            network.RpcCallBack(message);
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
