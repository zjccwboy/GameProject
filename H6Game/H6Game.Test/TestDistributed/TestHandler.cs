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
    [MessageType( MessageType.TestDistributedTestMessage)]
    public class TestMessage : IMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public int ActorId { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public string Message { get; set; }

        [ProtoBuf.ProtoMember(3)]
        public long LongData { get; set; }

        [ProtoBuf.ProtoMember(4)]
        public ulong ULongData { get; set; }

        [ProtoBuf.ProtoMember(5)]
        public byte ByteData { get; set; }

        [ProtoBuf.ProtoMember(6)]
        public sbyte SByteData { get; set; }

        [ProtoBuf.ProtoMember(7)]
        public uint UIntData { get; set; }

        [ProtoBuf.ProtoMember(8)]
        public List<int> ListIntData { get; set; }
    }
}
