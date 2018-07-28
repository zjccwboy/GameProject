using System.Collections.Concurrent;

namespace H6Game.Base.Base
{
    public static class ScenePool
    {
        public static readonly ConcurrentDictionary<int, BaseComponent> componentDictionary = new ConcurrentDictionary<int, BaseComponent>();

        public static bool TryGet<T>(int id, out T component) where T : BaseComponent
        {
            if (!componentDictionary.TryGetValue(id, out BaseComponent value))
            {
                component = default(T);
                return false;
            }
            component = (T)value;
            return true;
        }

        public static void Remove<T>(int id) where T : BaseComponent
        {
            if (componentDictionary.TryRemove(id, out BaseComponent value))
            {
                value.Close();
            }
        }

        public static T Add<T, K1>(K1 k1) where T : BaseComponent
        {
            var value = ComponentPool.Fetch<T, K1>(k1);
            componentDictionary[value.Id] = value;
            return value;
        }

        public static T Add<T, K1, K2>(K1 k1, K2 k2) where T : BaseComponent
        {
            var value = ComponentPool.Fetch<T, K1, K2>(k1, k2);
            componentDictionary[value.Id] = value;
            return value;
        }

        public static T Add<T, K1, K2, K3>(K1 k1, K2 k2, K3 k3) where T : BaseComponent
        {
            var value = ComponentPool.Fetch<T, K1, K2, K3>(k1, k2, k3);
            componentDictionary[value.Id] = value;
            return value;
        }

        public static T Add<T, K1, K2, K3, K4>(K1 k1, K2 k2, K3 k3, K4 k4) where T : BaseComponent
        {
            var value = ComponentPool.Fetch<T, K1, K2, K3, K4>(k1, k2, k3, k4);
            componentDictionary[value.Id] = value;
            return value;
        }

        public static T Add<T, K1, K2, K3, K4, K5>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5) where T : BaseComponent
        {
            var value = ComponentPool.Fetch<T, K1, K2, K3, K4, K5>(k1, k2, k3, k4, k5);
            componentDictionary[value.Id] = value;
            return value;
        }

        public static T Add<T, K1, K2, K3, K4, K5, K6>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5, K6 k6) where T : BaseComponent
        {
            var value = ComponentPool.Fetch<T, K1, K2, K3, K4, K5, K6>(k1, k2, k3, k4, k5, k6);
            componentDictionary[value.Id] = value;
            return value;
        }
    }
}
