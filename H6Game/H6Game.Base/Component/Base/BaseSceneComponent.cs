using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public abstract class BaseSceneComponent : BaseComponent
    {
        public SceneType SceneType { get; set; }

        private ConcurrentDictionary<int, BasePlayerComponent> PalyerDictionary = new ConcurrentDictionary<int, BasePlayerComponent>();
        private ConcurrentDictionary<Type, LinkedList<BasePlayerComponent>> PlayerTypeDictionary = new ConcurrentDictionary<Type, LinkedList<BasePlayerComponent>>();

        private ConcurrentDictionary<int, BaseSceneComponent> SceneDictionary = new ConcurrentDictionary<int, BaseSceneComponent>();
        private ConcurrentDictionary<Type, LinkedList<BaseSceneComponent>> SceneTypeDictionary = new ConcurrentDictionary<Type, LinkedList<BaseSceneComponent>>();


    }
}
