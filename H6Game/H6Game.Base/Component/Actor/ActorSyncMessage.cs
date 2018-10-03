using ProtoBuf;

namespace H6Game.Base
{
    [MessageType(SysMessageType.ActorMessage)]
    [ProtoContract]
    public class ActorSyncMessage : IMessage
    {
        [ProtoMember(1)]
        public int ActorId { get; set; }

        [ProtoMember(2)]
        public string ObjectId { get; set; }

        [ProtoMember(3)]
        public ActorType ActorType { get; set; }
    }
}
