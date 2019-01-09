using H6Game.Base;
using H6Game.Base.Message;
using H6Game.Hotfix.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace H6Game.ServerApp
{
    [NetCommand(TestCMD.TestCmd)]
    public class TestSubscriber : NetSubscriber<TestMessage>
    {
        protected override void Subscribe(Network network, TestMessage message, ushort messageCmd)
        {
            network.Response(message);
        }
    }

    public enum TestCMD
    {
        TestCmd = 47778,
    }

    [ProtoBuf.ProtoContract]
    [NetMessageType(MessageType.TestDistributedTestMessage)]
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
