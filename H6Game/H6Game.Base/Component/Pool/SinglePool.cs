using System;
using System.Collections.Concurrent;

namespace H6Game.Base
{
    /// <summary>
    /// 单例的组件池,一个类型只能被实例化一次，并且实例化以后的类型对象在系统中是唯一的，适合生命周期一直存在的对象
    /// </summary>
    public static class SinglePool
    {
        private static ConcurrentDictionary<Type, BaseComponent> TypeDictionary { get; } = new ConcurrentDictionary<Type, BaseComponent>();
        private static EventComponent EventComponent { get; }
        static SinglePool()
        {
            EventComponent = ManyPool.EventComponent;
            var type = EventComponent.GetType();
            TypeDictionary[type] = EventComponent;
        }

        public static T Get<T>() where T:BaseComponent
        {
            var type = typeof(T);
            if(!TypeDictionary.TryGetValue(type, out BaseComponent value))
            {
                value =  ManyPool.Add<T>();
                TypeDictionary[type] = value;
            }            
            return (T)value;
        }

        public static T Get<T,K1>(K1 k1) where T : BaseComponent
        {
            var type = typeof(T);
            if (!TypeDictionary.TryGetValue(type, out BaseComponent value))
            {
                value = ManyPool.Add<T, K1>(k1);
                TypeDictionary[type] = value;
            }
            return (T)value;
        }

        public static T Get<T, K1, K2>(K1 k1, K2 k2) where T : BaseComponent
        {
            var type = typeof(T);
            if (!TypeDictionary.TryGetValue(type, out BaseComponent value))
            {
                value = ManyPool.Add<T, K1,K2>(k1, k2);
                TypeDictionary[type] = value;
            }
            return (T)value;
        }

        public static T Get<T, K1, K2, K3>(K1 k1, K2 k2, K3 k3) where T : BaseComponent
        {
            var type = typeof(T);
            if (!TypeDictionary.TryGetValue(type, out BaseComponent value))
            {
                value = ManyPool.Add<T, K1, K2, K3>(k1, k2, k3);
                TypeDictionary[type] = value;
            }
            return (T)value;
        }

        public static T Get<T, K1, K2, K3, K4>(K1 k1, K2 k2, K3 k3, K4 k4) where T : BaseComponent
        {
            var type = typeof(T);
            if (!TypeDictionary.TryGetValue(type, out BaseComponent value))
            {
                value = ManyPool.Add<T, K1, K2, K3, K4>(k1, k2, k3, k4);
                TypeDictionary[type] = value;
            }
            return (T)value;
        }

        public static T Get<T, K1, K2, K3, K4, K5>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5) where T : BaseComponent
        {
            var type = typeof(T);
            if (!TypeDictionary.TryGetValue(type, out BaseComponent value))
            {
                value = ManyPool.Add<T, K1, K2, K3, K4, K5>(k1, k2, k3, k4, k5);
                TypeDictionary[type] = value;
            }
            return (T)value;
        }

        public static T Get<T, K1, K2, K3, K4, K5, K6>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5, K6 k6) where T : BaseComponent
        {
            var type = typeof(T);
            if (!TypeDictionary.TryGetValue(type, out BaseComponent value))
            {
                value = ManyPool.Add<T, K1, K2, K3, K4, K5, K6>(k1, k2, k3, k4, k5, k6);
                TypeDictionary[type] = value;
            }
            return (T)value;
        }
    }
}
