using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public abstract class BaseScene : BaseComponent
    {
        public SceneType SceneType { get; set; }
        public int ScentId { get; set; }

        private ConcurrentDictionary<int, PlayerComponent> PalyerDictionary = new ConcurrentDictionary<int, PlayerComponent>();
        private ConcurrentDictionary<Type, LinkedList<PlayerComponent>> PlayerTypeDictionary = new ConcurrentDictionary<Type, LinkedList<PlayerComponent>>();

        private ConcurrentDictionary<int, BaseScene> SceneDictionary = new ConcurrentDictionary<int, BaseScene>();
        private ConcurrentDictionary<Type, LinkedList<BaseScene>> SceneTypeDictionary = new ConcurrentDictionary<Type, LinkedList<BaseScene>>();


    }
}
