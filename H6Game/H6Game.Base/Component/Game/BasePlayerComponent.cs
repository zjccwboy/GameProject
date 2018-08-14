using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class BasePlayerComponent : BaseComponent, ICompoentPlayer
    {
        private readonly ConcurrentDictionary<Type, BaseComponent> Dictionary = new ConcurrentDictionary<Type, BaseComponent>();
        
        public void Add<T>(BaseSceneComponent component) where T : BaseSceneComponent
        {
            var type = typeof(T);
            Dictionary[type] = component;
        }

        public void Add<T>(BaseRommComponent component) where T : BaseRommComponent
        {
            var type = typeof(T);
            Dictionary[type] = component;
        }

        public void Add<T>(BaseGameComponent component) where T : BaseGameComponent
        {
            var type = typeof(T);
            Dictionary[type] = component;
        }

        public void Remove<T>() where T : BaseComponent
        {
            var type = typeof(T);
            Dictionary.TryRemove(type, out BaseComponent value);
        }

        public T Get<T>() where T : BaseComponent
        {
            var type = typeof(T);
            if (Dictionary.TryGetValue(type, out BaseComponent component))
            {
                return (T)component;
            }
            throw new Exception("类型不存在.");
        }
    }
}
