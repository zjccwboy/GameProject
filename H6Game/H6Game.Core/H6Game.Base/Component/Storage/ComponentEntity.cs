using System;
using System.Collections.Generic;

namespace H6Game.Base
{
    public abstract class ComponentEntity
    {
        protected internal Dictionary<Type, HashSet<BaseComponent>> TypeComponents { get; } = new Dictionary<Type, HashSet<BaseComponent>>();
        protected internal Dictionary<int, BaseComponent> IdComponents { get; } = new Dictionary<int, BaseComponent>();
        protected internal Dictionary<Type, BaseComponent> SingleComponents { get; } = new Dictionary<Type, BaseComponent>();

        /// <summary>
        /// 添加一个组件。
        /// 单例组件(SingleCase):单例组件直接返回存在的组件，单例组件只能被实例化一次放到池中，并且生命周期与进程生命周期相同。
        /// 多例组件(ManyCase)：多例组件每次都会从内存重用池中或者重新实例化一个组件对象返回，Dispose接口被调用生命周期结束，实际
        /// 多例组件放回池中一直不会被GC回收，实际生命周期是与进程生命周期一样的，只是多例组件可以被实例化多个，并且可以多次被
        /// 放到池中与从池中取出来复用。
        /// </summary>
        /// <typeparam name="T">组件类型。</typeparam>
        /// <param name="component">组件实体。</param>
        /// <returns>新增成功返回true,失败返回false，如果返回false表示池中已经存在该组件。</returns>
        public virtual bool AddComponent(BaseComponent component)
        {
            var type = component.GetType();
            var isSingle = ComponentPool.IsSingleType(type);
            var isNew = false;
            if (isSingle)
            {
                isNew = !SingleComponents.ContainsKey(type);
                if (isNew)
                {
                    SingleComponents[type] = component;
                }
            }
            else
            {
                isNew = !IdComponents.ContainsKey(component.Id);
                if (isNew)
                {
                    IdComponents[component.Id] = component;
                    if (!TypeComponents.TryGetValue(type, out HashSet<BaseComponent> components))
                    {
                        components = new HashSet<BaseComponent>();
                        TypeComponents[type] = components;
                    }
                    components.Add(component);
                }
            }

            if (isNew)
            {
                if (typeof(IController).IsAssignableFrom(type))
                {
                    MetodContextPool.Add((IController)component);
                }
                Game.Event.Add(component);
            }

            return isNew;
        }

        /// <summary>
        /// 添加并返回一个组件。
        /// 单例组件(SingleCase):单例组件直接返回存在的组件，单例组件只能被实例化一次放到池中，并且生命周期与进程生命周期相同。
        /// 多例组件(ManyCase)：多例组件每次都会从内存重用池中或者重新实例化一个组件对象返回，Dispose接口被调用生命周期结束，实际
        /// 多例组件放回池中一直不会被GC回收，实际生命周期是与进程生命周期一样的，只是多例组件可以被实例化多个，并且可以多次被
        /// 放到池中与从池中取出来复用。
        /// </summary>
        /// <typeparam name="T">组件类型。</typeparam>
        /// <returns>返回一个组件实体。</returns>
        public virtual T AddComponent<T>() where T : BaseComponent
        {
            var component = ComponentPool.Fetch<T>();
            this.AddComponent(component);
            return component;
        }

        /// <summary>
        /// 添加并返回一个组件。
        /// 单例组件(SingleCase):单例组件直接返回存在的组件，单例组件只能被实例化一次放到池中，并且生命周期与进程生命周期相同。
        /// 多例组件(ManyCase)：多例组件每次都会从内存重用池中或者重新实例化一个组件对象返回，Dispose接口被调用生命周期结束，实际
        /// 多例组件放回池中一直不会被GC回收，实际生命周期是与进程生命周期一样的，只是多例组件可以被实例化多个，并且可以多次被
        /// 放到池中与从池中取出来复用。
        /// </summary>
        /// <typeparam name="T">组件类型。</typeparam>
        /// <typeparam name="K1">组件构造函数第一个参数类型。</typeparam>
        /// <param name="k1">组件构造函数第一个参数。</param>
        /// <returns>返回一个组件实体。</returns>
        public virtual T AddComponent<T,K1>(K1 k1) where T : BaseComponent
        {
            var component = ComponentPool.Fetch<T, K1>(k1);
            this.AddComponent(component);
            return component;
        }

        /// <summary>
        /// 添加并返回一个组件。
        /// 单例组件(SingleCase):单例组件直接返回存在的组件，单例组件只能被实例化一次放到池中，并且生命周期与进程生命周期相同。
        /// 多例组件(ManyCase)：多例组件每次都会从内存重用池中或者重新实例化一个组件对象返回，Dispose接口被调用生命周期结束，实际
        /// 多例组件放回池中一直不会被GC回收，实际生命周期是与进程生命周期一样的，只是多例组件可以被实例化多个，并且可以多次被
        /// 放到池中与从池中取出来复用。
        /// </summary>
        /// <typeparam name="T">组件类型。</typeparam>
        /// <param name="k1">组件构造函数第一个参数类型。</param>
        /// <typeparam name="K2">组件构造函数第二个参数类型。</typeparam>
        /// <param name="k1">组件构造函数第一个参数。</param>
        /// <param name="k2">组件构造函数第二个参数。</param>
        /// <returns>返回一个组件实体。</returns>
        public virtual T AddComponent<T, K1, K2>(K1 k1, K2 k2) where T : BaseComponent
        {
            var component = ComponentPool.Fetch<T, K1, K2>(k1, k2);
            this.AddComponent(component);
            return component;
        }

        /// <summary>
        /// 添加并返回一个组件。
        /// 单例组件(SingleCase):单例组件直接返回存在的组件，单例组件只能被实例化一次放到池中，并且生命周期与进程生命周期相同。
        /// 多例组件(ManyCase)：多例组件每次都会从内存重用池中或者重新实例化一个组件对象返回，Dispose接口被调用生命周期结束，实际
        /// 多例组件放回池中一直不会被GC回收，实际生命周期是与进程生命周期一样的，只是多例组件可以被实例化多个，并且可以多次被
        /// 放到池中与从池中取出来复用。
        /// </summary>
        /// <typeparam name="T">组件类型。</typeparam>
        /// <param name="k1">组件构造函数第一个参数类型。</param>
        /// <typeparam name="K2">组件构造函数第二个参数类型。</typeparam>
        /// <param name="k1">组件构造函数第一个参数。</param>
        /// <param name="k2">组件构造函数第二个参数。</param>
        /// <param name="k3">组件构造函数第三个参数。</param>
        /// <returns>返回一个组件实体。</returns>
        public virtual T AddComponent<T, K1, K2,K3>(K1 k1, K2 k2, K3 k3) where T : BaseComponent
        {
            var component = ComponentPool.Fetch<T, K1, K2, K3>(k1, k2, k3);
            this.AddComponent(component);
            return component;
        }

        /// <summary>
        /// 添加并返回一个组件。
        /// 单例组件(SingleCase):单例组件直接返回存在的组件，单例组件只能被实例化一次放到池中，并且生命周期与进程生命周期相同。
        /// 多例组件(ManyCase)：多例组件每次都会从内存重用池中或者重新实例化一个组件对象返回，Dispose接口被调用生命周期结束，实际
        /// 多例组件放回池中一直不会被GC回收，实际生命周期是与进程生命周期一样的，只是多例组件可以被实例化多个，并且可以多次被
        /// 放到池中与从池中取出来复用。
        /// </summary>
        /// <typeparam name="T">组件类型。</typeparam>
        /// <param name="k1">组件构造函数第一个参数类型。</param>
        /// <typeparam name="K2">组件构造函数第二个参数类型。</typeparam>
        /// <param name="k1">组件构造函数第一个参数。</param>
        /// <param name="k2">组件构造函数第二个参数。</param>
        /// <param name="k3">组件构造函数第三个参数。</param>
        /// <param name="k4">组件构造函数第四个参数。</param>
        /// <returns>返回一个组件实体。</returns>
        public virtual T AddComponent<T, K1, K2, K3, K4>(K1 k1, K2 k2, K3 k3, K4 k4) where T : BaseComponent
        {
            var component = ComponentPool.Fetch<T, K1, K2, K3, K4>(k1, k2, k3, k4);
            this.AddComponent(component);
            return component;
        }

        /// <summary>
        /// 添加并返回一个组件。
        /// 单例组件(SingleCase):单例组件直接返回存在的组件，单例组件只能被实例化一次放到池中，并且生命周期与进程生命周期相同。
        /// 多例组件(ManyCase)：多例组件每次都会从内存重用池中或者重新实例化一个组件对象返回，Dispose接口被调用生命周期结束，实际
        /// 多例组件放回池中一直不会被GC回收，实际生命周期是与进程生命周期一样的，只是多例组件可以被实例化多个，并且可以多次被
        /// 放到池中与从池中取出来复用。
        /// </summary>
        /// <typeparam name="T">组件类型。</typeparam>
        /// <param name="k1">组件构造函数第一个参数类型。</param>
        /// <typeparam name="K2">组件构造函数第二个参数类型。</typeparam>
        /// <param name="k1">组件构造函数第一个参数。</param>
        /// <param name="k2">组件构造函数第二个参数。</param>
        /// <param name="k3">组件构造函数第三个参数。</param>
        /// <param name="k4">组件构造函数第四个参数。</param>.
        /// <param name="k5">组件构造函数第五个参数。</param>
        /// <returns>返回一个组件实体。</returns>
        public virtual T AddComponent<T, K1, K2, K3, K4, K5>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5) where T : BaseComponent
        {
            var component = ComponentPool.Fetch<T, K1, K2, K3, K4, K5>(k1, k2, k3, k4, k5);
            this.AddComponent(component);
            return component;
        }

        /// <summary>
        /// 添加并返回一个组件。
        /// 单例组件(SingleCase):单例组件直接返回存在的组件，单例组件只能被实例化一次放到池中，并且生命周期与进程生命周期相同。
        /// 多例组件(ManyCase)：多例组件每次都会从内存重用池中或者重新实例化一个组件对象返回，Dispose接口被调用生命周期结束，实际
        /// 多例组件放回池中一直不会被GC回收，实际生命周期是与进程生命周期一样的，只是多例组件可以被实例化多个，并且可以多次被
        /// 放到池中与从池中取出来复用。
        /// </summary>
        /// <typeparam name="T">组件类型。</typeparam>
        /// <param name="k1">组件构造函数第一个参数类型。</param>
        /// <typeparam name="K2">组件构造函数第二个参数类型。</typeparam>
        /// <param name="k1">组件构造函数第一个参数。</param>
        /// <param name="k2">组件构造函数第二个参数。</param>
        /// <param name="k3">组件构造函数第三个参数。</param>
        /// <param name="k4">组件构造函数第四个参数。</param>.
        /// <param name="k5">组件构造函数第五个参数。</param>
        /// <param name="k6">组件构造函数第六个参数。</param>
        /// <returns>返回一个组件实体。</returns>
        public virtual T AddComponent<T, K1, K2, K3, K4, K5, K6>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5, K6 k6) where T : BaseComponent
        {
            var component = ComponentPool.Fetch<T, K1, K2, K3, K4, K5, K6>(k1, k2, k3, k4, k5, k6);
            this.AddComponent(component);
            return component;
        }

        /// <summary>
        /// 获取一个SingleCase组件，不允许获取非SingleCase
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ComponentException">非SingleCase类型组件异常。</exception>
        /// <returns>返回一个单例(SigleCase)组件实体</returns>
        public virtual T GetComponent<T>() where T : BaseComponent
        {
            var type = typeof(T);
            var isSingle = ComponentPool.IsSingleType(type);
            if (!isSingle)
                throw new ComponentException($"类型:{type}非SingleCase组件不允许获取.");

            if (!SingleComponents.TryGetValue(type, out BaseComponent component))
                throw new ComponentException($"类型:{type}没有加到该组件中.");

            return (T)component;
        }

        /// <summary>
        /// 获取一个组件集合，不允许获取SingleCase
        /// </summary>
        /// <typeparam name="T">组件类型。</typeparam>
        /// <exception cref="ComponentException">不允许获取SingleCase类型组件异常。</exception>
        /// <returns>返回一个类型的多例(ManyCase)组件集合。</returns>
        public virtual HashSet<BaseComponent> GetComponents<T>() where T : BaseComponent
        {
            var type = typeof(T);
            var isSingle = ComponentPool.IsSingleType(type);
            if (isSingle)
                throw new ComponentException($"类型:{type}是SingleCase组件不允许获取.");

            if (!TypeComponents.TryGetValue(type, out HashSet<BaseComponent> components))
            {
                throw new ComponentException($"类型:{type}没有加到该组件中.");
            }

            return components;
        }

        /// <summary>
        /// 获取一个组件，不允许获取SingleCase
        /// </summary>
        /// <typeparam name="T">组件类型。</typeparam>
        /// <param name="id">组件Id。</param>
        /// <exception cref="ComponentException">不允许获取SingleCase类型组件异常。</exception>
        /// <returns>返回一个类型的多例(ManyCase)组件。</returns>        
        public virtual T GetComponent<T>(int id) where T : BaseComponent
        {
            var type = typeof(T);
            var isSingle = ComponentPool.IsSingleType(type);
            if (isSingle)
                throw new ComponentException($"类型:{type}是SingleCase组件不允许获取.");

            if (IdComponents.TryGetValue(id, out BaseComponent component))
            {
                return (T)component;
            }
            throw new ComponentException($"类型:{type} ID:{id}组件没有加到该组件中.");
        }

        /// <summary>
        /// 获取一个组件
        /// </summary>
        /// <param name="id">组件Id。</param>
        /// <returns>返回一个组件。</returns>        
        public virtual bool GetComponent(int id, out BaseComponent component)
        {
            return IdComponents.TryGetValue(id, out component);
        }

        /// <summary>
        /// 获取一个组件
        /// </summary>
        /// <param name="id">组件Id。</param>
        /// <returns>返回一个组件。</returns>        
        public virtual bool GetComponent<T>(int id, out T component) where T : BaseComponent
        {
            var result = IdComponents.TryGetValue(id, out BaseComponent value);
            component = (T)value;
            return result;
        }

        /// <summary>
        /// 删除一个组件
        /// </summary>
        /// <param name="component">组件实体</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public virtual bool Remove(BaseComponent component)
        {
            if (!IdComponents.TryGetValue(component.Id, out BaseComponent value))
                return false;

            if (TypeComponents.TryGetValue(component.GetType(), out HashSet<BaseComponent> hashVal))
                hashVal.Remove(component);

            return false;
        }

        /// <summary>
        /// 删除一个组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component">组件实体</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public virtual bool Remove<T>(T component) where T : BaseComponent
        {
            if (!IdComponents.TryGetValue(component.Id, out BaseComponent value))
                return false;

            var type = typeof(T);
            if (TypeComponents.TryGetValue(type, out HashSet<BaseComponent> hashVal))
                hashVal.Remove(component);

            return false;
        }

        /// <summary>
        /// 清空缓冲区
        /// </summary>
        public void Clear()
        {
            TypeComponents.Clear();
            IdComponents.Clear();
            SingleComponents.Clear();
        }
    }
}
