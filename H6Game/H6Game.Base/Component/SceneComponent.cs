using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class SceneComponent : BaseComponent
    {
        public void AddComponent<T>() where T : BaseComponent
        {
            var component = SinglePool.Get<T>();
            IdComponent.AddOrUpdate(component.Id, component, (k, v) => { return component; });
            var type = typeof(T);
            if (!TypeComponent.TryGetValue(type, out HashSet<BaseComponent> components))
            {
                components = new HashSet<BaseComponent>();
                TypeComponent[type] = components;
            }
            components.Add(component);
        }
    }
}
