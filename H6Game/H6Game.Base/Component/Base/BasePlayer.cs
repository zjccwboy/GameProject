using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class BasePlayer : BaseComponent
    {
        private readonly ConcurrentDictionary<int, BaseScene> SceneDictionary = new ConcurrentDictionary<int, BaseScene>();
        
        public int SceneId { get; set; }
        public int RoomId { get; set; }
        public int PalyerId { get; set; }
    }
}
