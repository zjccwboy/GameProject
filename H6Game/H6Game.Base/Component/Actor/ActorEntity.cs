using H6Game.Hotfix.Entities;
using H6Game.Hotfix.Enums;

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
