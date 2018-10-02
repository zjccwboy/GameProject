using ProtoBuf;

namespace H6Game.Base
{
    [MessageType(SysMessageType.ActorMessage)]
    [ProtoContract]
    public class ActorSyncMessage : IMessage
    {
        [ProtoMember(1)]
        public string ObjectId { get; set; }

        [ProtoMember(2)]
        public ActorType ActorType { get; set; }
    }
}
