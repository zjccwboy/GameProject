using System;
using System.Collections.Concurrent;

namespace H6Game.Base
{
    public static class ComponentPool
    {
        private static readonly ConcurrentDictionary<int, BaseComponent> componentDictionary = new ConcurrentDictionary<int, BaseComponent>();
        private static readonly ConcurrentDictionary<Type, ConcurrentQueue<BaseComponent>> componentTypeDictionary = new ConcurrentDictionary<Type, ConcurrentQueue<BaseComponent>>();

        static ComponentPool()
        {
            var types = ComponentFactory.CmponentTypes;
            foreach(var type in types)
            {
                if(!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    componentTypeDictionary.TryAdd(type, queue);
                }
            }
        }

        public static T Fetch<T>() where T: BaseComponent
        {
            var type = typeof(T);
            if (!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
            {
                queue = new ConcurrentQueue<BaseComponent>();
                componentTypeDictionary.TryAdd(type, queue);
            }

            if(!queue.TryDequeue(out BaseComponent result))
            {
                result = ComponentFactory.CreateComponent(type);
            }
            result.Id = ComponentIdCreator.CreateId();
            while(componentDictionary.ContainsKey(result.Id))
                result.Id = ComponentIdCreator.CreateId();

            componentDictionary.AddOrUpdate(result.Id, result, (k,v)=> { return result; });
            return (T)result;
        }

        public static T Fetch<T, K1>(K1 k1) where T : BaseComponent
        {
            var type = typeof(T);
            if (!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
            {
                queue = new ConcurrentQueue<BaseComponent>();
                componentTypeDictionary.TryAdd(type, queue);
            }

            if (!queue.TryDequeue(out BaseComponent result))
            {
                result = ComponentFactory.CreateComponent(type, k1);
            }
            result.Id = ComponentIdCreator.CreateId();
            while (componentDictionary.ContainsKey(result.Id))
                result.Id = ComponentIdCreator.CreateId();

            componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            return (T)result;
        }

        public static T Fetch<T, K1, K2>(K1 k1, K2 k2) where T : BaseComponent
        {
            var type = typeof(T);
            if (!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
            {
                queue = new ConcurrentQueue<BaseComponent>();
                componentTypeDictionary.TryAdd(type, queue);
            }

            if (!queue.TryDequeue(out BaseComponent result))
            {
                result = ComponentFactory.CreateComponent(type, k1, k2);
            }
            result.Id = ComponentIdCreator.CreateId();
            while (componentDictionary.ContainsKey(result.Id))
                result.Id = ComponentIdCreator.CreateId();

            componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            return (T)result;
        }

        public static T Fetch<T, K1, K2, K3>(K1 k1, K2 k2, K3 k3) where T : BaseComponent
        {
            var type = typeof(T);
            if (!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
            {
                queue = new ConcurrentQueue<BaseComponent>();
                componentTypeDictionary.TryAdd(type, queue);
            }

            if (!queue.TryDequeue(out BaseComponent result))
            {
                result = ComponentFactory.CreateComponent(type, k1, k2, k3);
            }
            result.Id = ComponentIdCreator.CreateId();
            while (componentDictionary.ContainsKey(result.Id))
                result.Id = ComponentIdCreator.CreateId();

            componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            return (T)result;
        }

        public static T Fetch<T, K1, K2, K3, K4>(K1 k1, K2 k2, K3 k3, K4 k4) where T : BaseComponent
        {
            var type = typeof(T);
            if (!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
            {
                queue = new ConcurrentQueue<BaseComponent>();
                componentTypeDictionary.TryAdd(type, queue);
            }

            if (!queue.TryDequeue(out BaseComponent result))
            {
                result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4);
            }
            result.Id = ComponentIdCreator.CreateId();
            while (componentDictionary.ContainsKey(result.Id))
                result.Id = ComponentIdCreator.CreateId();

            componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            return (T)result;
        }

        public static T Fetch<T, K1, K2, K3, K4, K5>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5) where T : BaseComponent
        {
            var type = typeof(T);
            if (!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
            {
                queue = new ConcurrentQueue<BaseComponent>();
                componentTypeDictionary.TryAdd(type, queue);
            }

            if (!queue.TryDequeue(out BaseComponent result))
            {
                result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4, k5);
            }
            result.Id = ComponentIdCreator.CreateId();
            while (componentDictionary.ContainsKey(result.Id))
                result.Id = ComponentIdCreator.CreateId();

            componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            return (T)result;
        }

        public static T Fetch<T, K1, K2, K3, K4, K5, K6>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5, K6 k6) where T : BaseComponent
        {
            var type = typeof(T);
            if (!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
            {
                queue = new ConcurrentQueue<BaseComponent>();
                componentTypeDictionary.TryAdd(type, queue);
            }

            if (!queue.TryDequeue(out BaseComponent result))
            {
                result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4, k5, k6);
            }
            result.Id = ComponentIdCreator.CreateId();
            while (componentDictionary.ContainsKey(result.Id))
                result.Id = ComponentIdCreator.CreateId();

            componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            return (T)result;
        }

        public static void PutBack<T>(this T component) where T : BaseComponent
        {
            var baseComponent = component as BaseComponent;
            componentDictionary.TryRemove(component.Id, out BaseComponent value);
            value.Id = 0;
            value.IsStart = false;
        }
    }
}
