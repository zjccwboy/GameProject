using H6Game.Entitys;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class RommComponent : BaseComponent
    {
        public TRoom RoomEntiry { get; set; }

        public void Add(TRoom roomInfo)
        {
            this.RoomEntiry = roomInfo;
            Game.Actor.AddLocalAcotr(new ActorInfoEntity
            {
                ActorId = this.Id,
                Id = this.RoomEntiry.Id,
                ActorType = ActorType.Room,
            });
        }
    }

}
