using H6Game.Entitys;
using ProtoBuf;

namespace H6Game.Base
{
    [ProtoContract]
    public class ActorInfoEntity : BaseEntity
    {
        [ProtoMember(2)]
        public int ActorId { get; set; }

        [ProtoMember(3)]
        public Network Network { get; set; }

        public long GetEntityId()
        {
            var entityId = ActorId;
            entityId = entityId | Network.Channel.Id << 32;
            return entityId;
        }
    }
}
