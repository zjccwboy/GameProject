using ProtoBuf;
using H6Game.Entities.Enums;

namespace H6Game.Message
{
    [MessageType(MessageType.ActorMessage)]
    [ProtoContract]
    public class ActorSyncMessage : IMessage
    {
        [ProtoMember(1)]
        public string ObjectId { get; set; }

        [ProtoMember(2)]
        public ActorType ActorType { get; set; }
    }
}
