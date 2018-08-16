using H6Game.Entitys;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class BaseRommComponent : BaseComponent, ICompoentRoom
    {
        public RoomType RType { get; set; }
        public GameType GType { get; set; }
    }
}
