using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace H6Game.Base
{
    public abstract class BaseComponent : IDisposable
    {
        protected ConcurrentDictionary<Type, HashSet<BaseComponent>> TypeComponent { get; } = new ConcurrentDictionary<Type, HashSet<BaseComponent>>();
        protected ConcurrentDictionary<int, BaseComponent> IdComponent { get; } = new ConcurrentDictionary<int, BaseComponent>();

        public IEnumerable<Type> GetTypes()
        {
            return this.TypeComponent.Keys;
        }

        public virtual void Add<T>(T component) where T : BaseComponent
        {
            IdComponent.AddOrUpdate(component.Id, component, (k, v) => { return component; });
            var type = typeof(T);
            if (!TypeComponent.TryGetValue(type, out HashSet<BaseComponent> components))
            {
                components = new HashSet<BaseComponent>();
                TypeComponent[type] = components;
            }
            components.Add(component);
        }

        public bool TryGet<T>(out HashSet<BaseComponent> value) where T:BaseComponent
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

        public virtual void Remove(BaseComponent component)
        {
            if(IdComponent.TryGetValue(component.Id, out BaseComponent value))
            {
                var type = value.GetType();
                if(TypeComponent.TryGetValue(type, out HashSet<BaseComponent> hashVal))
                {
                    hashVal.Remove(component);
                }
            }
        }

        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public int Id { get; set; }
        public bool IsStart { get; set; }
        public bool IsAwake { get; set; }
        public virtual void Dispose()
        {
            this.PutBack();
        }
    }
}
