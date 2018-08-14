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

        public virtual void AddComponent<T>(T component) where T : BaseComponent
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

        /// <summary>
        /// 该事件只有第一次创建出来的时候执行一次，应该把资源初始化之类的逻辑放到该事件重写方法中处理。
        /// </summary>
        public virtual void Awake() { }

        /// <summary>
        /// 每一次从池中拿出来的时候会被执行一次，在重写方法中可以把一些入口逻辑放到其中。
        /// </summary>
        public virtual void Start() { }

        /// <summary>
        /// 每一帧都会被执行
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// 调用该事件会把当前的组件放回池中
        /// </summary>
        public virtual void Dispose()
        {
            this.PutBack();
        }

        public int Id { get; set; }

        /// <summary>
        /// Start事件状态
        /// </summary>
        public bool IsStart { get; set; }

        /// <summary>
        /// Awake事件状态
        /// </summary>
        public bool IsAwake { get; set; }

    }
}
