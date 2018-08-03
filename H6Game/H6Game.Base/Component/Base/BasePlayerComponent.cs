using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class BasePlayerComponent : BaseComponent
    {
        private readonly ConcurrentDictionary<int, BaseSceneComponent> SceneDictionary = new ConcurrentDictionary<int, BaseSceneComponent>();

        public int SceneId { get; set; }
        public int RoomId { get; set; }
        public int PalyerId { get; set; }
    }
}
