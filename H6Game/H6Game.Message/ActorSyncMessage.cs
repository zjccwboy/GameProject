using ProtoBuf;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using H6Game.Entities;
using H6Game.Entities.Enums;

namespace H6Game.Message
{
    [MessageType(MessageType.ActorMessage)]
    [ProtoContract]
    public class ActorSyncMessage : IMessage
    {
        [BsonElement]
        [ProtoMember(1)]
        public string ObjectId { get; set; }


        [BsonElement]
        [ProtoMember(2)]
        public ActorType ActorType { get; set; }
    }
}
