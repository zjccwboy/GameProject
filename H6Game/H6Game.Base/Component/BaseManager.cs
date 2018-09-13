using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace H6Game.Base
{
    public class BaseManager
    {
        protected internal ConcurrentDictionary<Type, HashSet<BaseComponent>> TypeComponent { get; } = new ConcurrentDictionary<Type, HashSet<BaseComponent>>();
        protected internal ConcurrentDictionary<int, BaseComponent> IdComponent { get; } = new ConcurrentDictionary<int, BaseComponent>();
        protected internal ConcurrentDictionary<Type, BaseComponent> SingleDictionary { get; } = new ConcurrentDictionary<Type, BaseComponent>();

        /// <summary>
        /// 添加一个组件。
        /// 单例组件(SingleCase):单例组件直接返回存在的组件，单例组件只能被实例化一次放到池中，并且生命周期与进程生命周期相同。
        /// 多例组件(ManyCase)：多例组件每次都会从内存重用池中或者重新实例化一个组件对象返回，Dispose接口被调用生命周期结束。
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
        /// 添加并返回一个组件。
        /// 单例组件(SingleCase):单例组件直接返回存在的组件，单例组件只能被实例化一次放到池中，并且生命周期与进程生命周期相同。
        /// 多例组件(ManyCase)：多例组件每次都会从内存重用池中或者重新实例化一个组件对象返回，Dispose接口被调用生命周期结束。
        /// </summary>
        /// <typeparam name="T">组件类型。</typeparam>
        /// <returns>返回被实例化的组件。</returns>
        public virtual T AddComponent<T>() where T : BaseComponent
        {
            var component = ComponentPool.Fetch<T>();
            this.AddComponent(component);
            return component;
        }

        /// <summary>
        /// 添加并返回一个组件。
        /// 单例组件(SingleCase):单例组件直接返回存在的组件，单例组件只能被实例化一次放到池中，并且生命周期与进程生命周期相同。
        /// 多例组件(ManyCase)：多例组件每次都会从内存重用池中或者重新实例化一个组件对象返回，Dispose接口被调用生命周期结束。
        /// </summary>
        /// <typeparam name="T">组件类型。</typeparam>
        /// <typeparam name="K1">组件构造函数第一个参数类型。</typeparam>
        /// <param name="k1">组件构造函数第一个参数。</param>
        /// <returns>返回被实例化的组件。</returns>
        public virtual T AddComponent<T,K1>(K1 k1) where T : BaseComponent
        {
            var component = ComponentPool.Fetch<T, K1>(k1);
            this.AddComponent(component);
            return component;
        }

        /// <summary>
        /// 添加并返回一个组件。
        /// 单例组件(SingleCase):单例组件直接返回存在的组件，单例组件只能被实例化一次放到池中，并且生命周期与进程生命周期相同。
        /// 多例组件(ManyCase)：多例组件每次都会从内存重用池中或者重新实例化一个组件对象返回，Dispose接口被调用生命周期结束。
        /// </summary>
        /// <typeparam name="T">组件类型。</typeparam>
        /// <param name="k1">组件构造函数第一个参数类型。</param>
        /// <typeparam name="K2">组件构造函数第二个参数类型。</typeparam>
        /// <param name="k1">组件构造函数第一个参数。</param>
        /// <param name="k2">组件构造函数第二个参数。</param>
        /// <returns></returns>
        public virtual T AddComponent<T, K1, K2>(K1 k1, K2 k2) where T : BaseComponent
        {
            var component = ComponentPool.Fetch<T, K1, K2>(k1, k2);
            this.AddComponent(component);
            return component;
        }

        /// <summary>
        /// 获取一个SingleCase组件，不允许获取非SingleCase
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ComponentException">非SingleCase类型组件异常。</exception>
        /// <returns></returns>
        public virtual T GetComponent<T>() where T : BaseComponent
        {
            var type = typeof(T);
            var isSingle = ComponentPool.IsSingleType(type);
            if (!isSingle)
                throw new ComponentException($"类型:{type}非SingleCase组件不允许获取.");

            if (!SingleDictionary.TryGetValue(type, out BaseComponent component))
                throw new ComponentException($"类型:{type}没有加到该组件中.");

            return (T)component;
        }

        /// <summary>
        /// 获取一个组件集合，不允许获取SingleCase
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <exception cref="ComponentException">不允许获取SingleCase类型组件异常。</exception>
        /// <returns></returns>
        public virtual HashSet<BaseComponent> GetComponents<T>() where T : BaseComponent
        {
            var type = typeof(T);
            var isSingle = ComponentPool.IsSingleType(type);
            if (isSingle)
                throw new ComponentException($"类型:{type}是SingleCase组件不允许获取.");

            if (!TypeComponent.TryGetValue(type, out HashSet<BaseComponent> components))
            {
                throw new ComponentException($"类型:{type}没有加到该组件中.");
            }

            return components;
        }

        /// <summary>
        /// 获取一个组件，不允许获取SingleCase
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <exception cref="ComponentException">不允许获取SingleCase类型组件异常。</exception>
        /// <returns></returns>        
        public virtual T GetComponent<T>(int id) where T : BaseComponent
        {
            var type = typeof(T);
            var isSingle = ComponentPool.IsSingleType(type);
            if (isSingle)
                throw new ComponentException($"类型:{type}是SingleCase组件不允许获取.");

            if (IdComponent.TryGetValue(id, out BaseComponent component))
            {
                return (T)component;
            }
            throw new ComponentException($"类型:{type} ID:{id}组件没有加到该组件中.");
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
