using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class BaseManager
    {
        protected internal ConcurrentDictionary<Type, HashSet<BaseComponent>> TypeComponent { get; } = new ConcurrentDictionary<Type, HashSet<BaseComponent>>();
        protected internal ConcurrentDictionary<int, BaseComponent> IdComponent { get; } = new ConcurrentDictionary<int, BaseComponent>();
        protected internal ConcurrentDictionary<Type, BaseComponent> SingleDictionary { get; } = new ConcurrentDictionary<Type, BaseComponent>();

        /// <summary>
        /// 新建并添加一个组件到该组件中.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        public virtual bool AddComponent<T>(T component) where T : BaseComponent
        {
            var type = component.GetType();
            var isSingle = ComponentPool.IsSingleType(type);
            var isNew = false;
            if (isSingle)
            {
                isNew = SingleDictionary.TryAdd(type, component);
            }
            else
            {
                IdComponent.AddOrUpdate(component.Id, component, (k, v) => { return component; });
                if (!TypeComponent.TryGetValue(type, out HashSet<BaseComponent> components))
                {
                    components = new HashSet<BaseComponent>();
                    TypeComponent[type] = components;
                }
                isNew = components.Add(component);
            }

            if (isNew)
            {
                Game.Event.Add(component);
            }

            return isNew;
        }

        /// <summary>
        /// 新建或者从组件池中获取一个单例组件添加到该组件中.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public virtual T AddComponent<T>() where T : BaseComponent
        {
            var component = ComponentPool.Fetch<T>();
            this.AddComponent(component);
            return component;
        }

        /// <summary>
        /// 新建或者从组件池中获取一个单例组件添加到该组件中.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public virtual T AddComponent<T,K1>(K1 k1) where T : BaseComponent
        {
            var component = ComponentPool.Fetch<T, K1>(k1);
            this.AddComponent(component);
            return component;
        }

        /// <summary>
        /// 新建或者从组件池中获取一个单例组件添加到该组件中.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public virtual T AddComponent<T, K1, K2>(K1 k1, K2 k2) where T : BaseComponent
        {
            var component = ComponentPool.Fetch<T, K1, K2>(k1, k2);
            this.AddComponent(component);
            return component;
        }

        /// <summary>
        /// 获取一个SingleCase组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T GetComponent<T>() where T : BaseComponent
        {
            var type = typeof(T);
            var isSingle = ComponentPool.IsSingleType(type);
            if (!isSingle)
                throw new Exception($"类型:{type}非SingleCase组件不允许获取.");

            if (!SingleDictionary.TryGetValue(type, out BaseComponent component))
                throw new Exception($"类型:{type}没有加到该组件中.");

            return (T)component;
        }

        /// <summary>
        /// 获取一个组件集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual HashSet<BaseComponent> GetComponents<T>() where T : BaseComponent
        {
            var type = typeof(T);
            var isSingle = ComponentPool.IsSingleType(type);
            if (isSingle)
                throw new Exception($"类型:{type}是SingleCase组件不允许获取.");

            if (!TypeComponent.TryGetValue(type, out HashSet<BaseComponent> components))
            {
                throw new Exception($"类型:{type}没有加到该组件中.");
            }

            return components;
        }

        /// <summary>
        /// 获取一个组件，不允许获取SingleCase
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>        
        public virtual T GetComponent<T>(int id) where T : BaseComponent
        {
            var type = typeof(T);
            var isSingle = ComponentPool.IsSingleType(type);
            if (isSingle)
                throw new Exception($"类型:{type}是SingleCase组件不允许获取.");

            if (IdComponent.TryGetValue(id, out BaseComponent component))
            {
                return (T)component;
            }
            throw new Exception($"类型:{type} ID:{id}组件没有加到该组件中.");
        }

        public virtual bool Remove(BaseComponent component)
        {
            var type = component.GetType();
            var isSingle = ComponentPool.IsSingleType(type);
            if (isSingle)
            {
                return SingleDictionary.TryRemove(type, out BaseComponent value);
            }
            else
            {
                if (IdComponent.TryGetValue(component.Id, out BaseComponent value))
                {
                    if (TypeComponent.TryGetValue(type, out HashSet<BaseComponent> hashVal))
                    {
                        if (hashVal.Remove(component))
                            Game.Scene.Remove(component);
                    }
                }
            }
            return false;
        }
    }
}
