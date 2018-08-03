using System;
using System.Collections.Concurrent;

namespace H6Game.Base
{
    /// <summary>
    /// 单例的组件池,适合生命周期一直存在的对象
    /// </summary>
    public static class SinglePool
    {
        private static ConcurrentDictionary<Type, BaseComponent> TypeDictionary = new ConcurrentDictionary<Type, BaseComponent>();

        public static T Get<T>() where T:BaseComponent
        {
            var type = typeof(T);
            if(!TypeDictionary.TryGetValue(type, out BaseComponent value))
            {
                value =  ComponentPool.Fetch<T>();
                TypeDictionary[type] = value;
            }
            return (T)value;
        }

        public static T Get<T,K1>(K1 k1) where T : BaseComponent
        {
            var type = typeof(T);
            if (!TypeDictionary.TryGetValue(type, out BaseComponent value))
            {
                value = ComponentPool.Fetch<T, K1>(k1);
                TypeDictionary[type] = value;
            }
            return (T)value;
        }

        public static T Get<T, K1, K2>(K1 k1, K2 k2) where T : BaseComponent
        {
            var type = typeof(T);
            if (!TypeDictionary.TryGetValue(type, out BaseComponent value))
            {
                value = ComponentPool.Fetch<T, K1,K2>(k1, k2);
                TypeDictionary[type] = value;
            }
            return (T)value;
        }

        public static T Get<T, K1, K2, K3>(K1 k1, K2 k2, K3 k3) where T : BaseComponent
        {
            var type = typeof(T);
            if (!TypeDictionary.TryGetValue(type, out BaseComponent value))
            {
                value = ComponentPool.Fetch<T, K1, K2, K3>(k1, k2, k3);
                TypeDictionary[type] = value;
            }
            return (T)value;
        }

        public static T Get<T, K1, K2, K3, K4>(K1 k1, K2 k2, K3 k3, K4 k4) where T : BaseComponent
        {
            var type = typeof(T);
            if (!TypeDictionary.TryGetValue(type, out BaseComponent value))
            {
                value = ComponentPool.Fetch<T, K1, K2, K3, K4>(k1, k2, k3, k4);
                TypeDictionary[type] = value;
            }
            return (T)value;
        }

        public static T Get<T, K1, K2, K3, K4, K5>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5) where T : BaseComponent
        {
            var type = typeof(T);
            if (!TypeDictionary.TryGetValue(type, out BaseComponent value))
            {
                value = ComponentPool.Fetch<T, K1, K2, K3, K4, K5>(k1, k2, k3, k4, k5);
                TypeDictionary[type] = value;
            }
            return (T)value;
        }

        public static T Get<T, K1, K2, K3, K4, K5, K6>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5, K6 k6) where T : BaseComponent
        {
            var type = typeof(T);
            if (!TypeDictionary.TryGetValue(type, out BaseComponent value))
            {
                value = ComponentPool.Fetch<T, K1, K2, K3, K4, K5, K6>(k1, k2, k3, k4, k5, k6);
                TypeDictionary[type] = value;
            }
            return (T)value;
        }

        public static void Remove<T>(this T component) where T : BaseComponent
        {
            var type = typeof(T);
            if(TypeDictionary.TryRemove(type, out BaseComponent value))
            {
                value.Close();
            }
        }
    }
}
