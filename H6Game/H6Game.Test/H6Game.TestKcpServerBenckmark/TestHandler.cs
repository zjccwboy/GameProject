using H6Game.Base;
using H6Game.Message;
using H6Game.Rpository;
using System;
using System.Threading.Tasks;

namespace H6Game.TestKcpServerBenckmark
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
