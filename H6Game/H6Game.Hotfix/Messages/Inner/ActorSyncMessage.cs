using ProtoBuf;
using H6Game.Hotfix.Enums;
using H6Game.Hotfix.Messages.Attributes;
using H6Game.Hotfix.Messages.Enums;

namespace H6Game.Hotfix.Messages.Inner
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
