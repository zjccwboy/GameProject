using ProtoBuf;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace H6Game.Message
{
    [MessageType(MessageType.ActorMessage)]
    public class ActorMessage : IMessage
    {
        [BsonElement]
        [ProtoMember(1)]
        public string ObjectId { get; set; }
    }
}
