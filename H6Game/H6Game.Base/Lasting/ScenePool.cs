using System;
using System.Collections.Concurrent;

namespace H6Game.Base.Base
{
    public static class ScenePool
    {
        public static readonly ConcurrentDictionary<int, BaseSceneComponent> ComponentDictionary = new ConcurrentDictionary<int, BaseSceneComponent>();
        public static readonly ConcurrentDictionary<Type, ConcurrentQueue<BaseSceneComponent>> TypeQueue = new ConcurrentDictionary<Type, ConcurrentQueue<BaseSceneComponent>>();


        public static bool TryGet<T>(int id, out T component) where T : BaseSceneComponent
        {
            if (!ComponentDictionary.TryGetValue(id, out BaseSceneComponent value))
            {
                component = default;
                return false;
            }
            component = (T)value;
            return true;
        }

        public static void Remove<T>(int id) where T : BaseSceneComponent
        {
            if (ComponentDictionary.TryRemove(id, out BaseSceneComponent value))
            {
                value.Close();
            }
        }

        public static T Add<T, K1>(K1 k1) where T : BaseSceneComponent
        {
            var value = ComponentPool.Fetch<T, K1>(k1);
            ComponentDictionary[value.Id] = value;
            return value;
        }

        public static T Add<T, K1, K2>(K1 k1, K2 k2) where T : BaseSceneComponent
        {
            var value = ComponentPool.Fetch<T, K1, K2>(k1, k2);
            ComponentDictionary[value.Id] = value;
            return value;
        }

        public static T Add<T, K1, K2, K3>(K1 k1, K2 k2, K3 k3) where T : BaseSceneComponent
        {
            var value = ComponentPool.Fetch<T, K1, K2, K3>(k1, k2, k3);
            ComponentDictionary[value.Id] = value;
            return value;
        }

        public static T Add<T, K1, K2, K3, K4>(K1 k1, K2 k2, K3 k3, K4 k4) where T : BaseSceneComponent
        {
            var value = ComponentPool.Fetch<T, K1, K2, K3, K4>(k1, k2, k3, k4);
            ComponentDictionary[value.Id] = value;
            return value;
        }

        public static T Add<T, K1, K2, K3, K4, K5>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5) where T : BaseSceneComponent
        {
            var value = ComponentPool.Fetch<T, K1, K2, K3, K4, K5>(k1, k2, k3, k4, k5);
            ComponentDictionary[value.Id] = value;
            return value;
        }

        public static T Add<T, K1, K2, K3, K4, K5, K6>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5, K6 k6) where T : BaseSceneComponent
        {
            var value = ComponentPool.Fetch<T, K1, K2, K3, K4, K5, K6>(k1, k2, k3, k4, k5, k6);
            ComponentDictionary[value.Id] = value;
            return value;
        }
    }
}
