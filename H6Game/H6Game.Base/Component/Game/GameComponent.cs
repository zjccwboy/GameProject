using H6Game.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class GameComponent : BaseComponent
    {
        public TGame GameEntity { get; set; }

        public void Add(TGame gameInfo)
        {
            this.GameEntity = gameInfo;
            Game.Actor.AddLocalAcotr(new ActorInfoEntity
            {
                ActorId = this.Id,
                Id = this.GameEntity.Id,
                ActorType = ActorType.Game,
            });
        }
    }
}
