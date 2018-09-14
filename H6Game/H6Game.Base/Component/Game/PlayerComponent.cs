﻿using H6Game.Entities;
using H6Game.Entities.Enums;

namespace H6Game.Base
{
    public class PlayerComponent : BaseComponent
    {
        public void AddLocal(TAccount accountInfo)
        {
            Game.Actor.AddLocalAcotr(new ActorEntity
            {
                ActorId = this.Id,
                Id = accountInfo.Id,
                ActorType = ActorType.Player,
                ActorInfo = accountInfo,
            });
        }

        public void AddRemote(string objectId, Network network)
        {
            Game.Actor.AddRemoteActor(new ActorEntity
            {
                ActorId = this.Id,
                Id = objectId,
                ActorType = ActorType.Player,
                Network = network,
            });
        }

        public void Remove(string objectId)
        {
            Game.Actor.RemoveActor(ActorType.Player, objectId);
        }
    }
}
