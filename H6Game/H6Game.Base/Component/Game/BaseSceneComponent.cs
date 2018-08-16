using H6Game.Entitys;
using H6Game.Message;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public abstract class BaseSceneComponent : BaseComponent, IComponentIScene
    {
        public SceneType SceneType { get; set; }
        private ConcurrentDictionary<int, BasePlayerComponent> PalyerDictionary = new ConcurrentDictionary<int, BasePlayerComponent>();
        private ConcurrentDictionary<int, BaseSceneComponent> SceneDictionary = new ConcurrentDictionary<int, BaseSceneComponent>();
    }
}
