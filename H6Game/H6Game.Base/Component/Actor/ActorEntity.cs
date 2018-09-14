using H6Game.Entities;
using H6Game.Entities.Enums;
using ProtoBuf;

namespace H6Game.Base
{
    public class ActorEntity : BaseEntity
    {
        public int ActorId { get; set; }

        public Network Network { get; set; }

        public ActorType ActorType { get; set; }

        public BaseEntity ActorInfo { get; set; }
    }
}
