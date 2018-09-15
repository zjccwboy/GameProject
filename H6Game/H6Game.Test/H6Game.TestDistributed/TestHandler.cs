using H6Game.Base;
using H6Game.Message;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDistributed
{
    [HandlerCMD(MessageCMD.TestCMD1)]
    public class TestHandler : AMessageHandler<TestMessage>
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
        [BsonElement]
        [ProtoBuf.ProtoMember(1)]
        public int ActorId { get; set; }

        [BsonElement]
        [ProtoBuf.ProtoMember(2)]
        public string Message { get; set; }

        [BsonElement]
        [ProtoBuf.ProtoMember(3)]
        public long LongData { get; set; }

        [BsonElement]
        [ProtoBuf.ProtoMember(4)]
        public ulong ULongData { get; set; }

        [BsonElement]
        [ProtoBuf.ProtoMember(5)]
        public byte ByteData { get; set; }

        [BsonElement]
        [ProtoBuf.ProtoMember(6)]
        public sbyte SByteData { get; set; }

        [BsonElement]
        [ProtoBuf.ProtoMember(7)]
        public uint UIntData { get; set; }

        [BsonElement]
        [ProtoBuf.ProtoMember(8)]
        public List<int> ListIntData { get; set; }
    }
}
