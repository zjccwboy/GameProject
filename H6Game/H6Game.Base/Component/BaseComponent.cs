using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace H6Game.Base
{
    public abstract class BaseComponent
    {
        private ConcurrentDictionary<Type, HashSet<int>> TypeComponent { get; } = new ConcurrentDictionary<Type, HashSet<int>>();
        private ConcurrentDictionary<int, BaseComponent> IdComponent { get; } = new ConcurrentDictionary<int, BaseComponent>();

        public void AddNew<T>() where T:BaseComponent
        {
            var component = ManyPool.Add<T>();
            IdComponent.AddOrUpdate(component.Id, component, (k, v) => { return component; });
            var type = typeof(T);
            if(!TypeComponent.TryGetValue(type, out HashSet<int> ids))
            {
                ids = new HashSet<int>();
                TypeComponent[type] = ids;
            }
            ids.Add(component.Id);
        }

        public void Add<T>(T component) where T : BaseComponent
        {
            IdComponent.AddOrUpdate(component.Id, component, (k, v) => { return component; });
            var type = typeof(T);
            if (!TypeComponent.TryGetValue(type, out HashSet<int> ids))
            {
                ids = new HashSet<int>();
                TypeComponent[type] = ids;
            }
            ids.Add(component.Id);
        }

        public bool TryGet<T>(out HashSet<int> value) where T:BaseComponent
        {
            return TypeComponent.TryGetValue(typeof(T), out value);
        }

        public bool TryGet<T>(int id, out T value) where T:BaseComponent
        {
            if(IdComponent.TryGetValue(id, out BaseComponent component))
            {
                value = (T)component;
                return true;
            }
            value = null;
            return false;
        }

        public void Remove(int componentId)
        {
            if(IdComponent.TryGetValue(componentId, out BaseComponent value))
            {
                var type = value.GetType();
                if(TypeComponent.TryGetValue(type, out HashSet<int> hashVal))
                {
                    hashVal.Remove(componentId);
                }
            }
        }

        public virtual void Start() { }
        public virtual void Update() { }
        public int Id { get; set; }
        public virtual void Stop()
        {
            this.PutBack();
        }
    }
}
