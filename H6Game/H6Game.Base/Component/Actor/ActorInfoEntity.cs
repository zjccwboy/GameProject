using H6Game.Entitys;

namespace H6Game.Base
{
    public class ActorInfoEntity : BaseEntity
    {
        public int ActorId { get; set; }
        public Network Network { get; set; }

        public long GetEntityId()
        {
            var entityId = ActorId;
            entityId = entityId | Network.Channel.Id << 32;
            return entityId;
        }
    }
}
