
using H6Game.Base;

namespace H6Game.Actor
{
    public class ActorEntity : BaseEntity
    {
        public int ActorId { get; set; }

        public Network Network { get; set; }

        public ActorType ActorType { get; set; }

        public BaseEntity ActorInfo { get; set; }
    }
}
