using H6Game.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Entitys
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
