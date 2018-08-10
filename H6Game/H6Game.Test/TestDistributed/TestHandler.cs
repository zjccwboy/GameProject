using H6Game.Base;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDistributed
{
    [HandlerCMD(MessageCMD.TestCMD1)]
    public class TestHandler : AHandler<TestMessage>
    {
        protected override void Handler(Network network, TestMessage message)
        {
            network.RpcCallBack(message);
        }
    }

    [ProtoBuf.ProtoContract]
    public class TestMessage : IMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public int ActorId { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public string Message { get; set; }
    }
}
