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

        public static bool TryGetComponent<T>(int componentId, out T value) where T : BaseComponent
        {
            if (componentDictionary.TryGetValue(componentId, out BaseComponent result))
            {
                value = (T)result;
                return true;
            }
            value = null;
            return false;
        }

        public static T Fetch<T>() where T: BaseComponent
        {
            var type = typeof(T);
            if (!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
            {
                queue = new ConcurrentQueue<BaseComponent>();
                componentTypeDictionary.TryAdd(type, queue);
            }

            if(queue.TryDequeue(out BaseComponent result))
            {
                result.Id = ComponentIdCreator.CreateId();
            }
            else
            {
                result = ComponentFactory.CreateComponent(type);
            }
            componentDictionary.AddOrUpdate(result.Id, result, (k,v)=> { return result; });
            result.Start();
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

            if (queue.TryDequeue(out BaseComponent result))
            {
                result.Id = ComponentIdCreator.CreateId();
            }
            else
            {
                result = ComponentFactory.CreateComponent(type, k1);
            }
            componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            result.Start();
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

            if (queue.TryDequeue(out BaseComponent result))
            {
                result.Id = ComponentIdCreator.CreateId();
            }
            else
            {
                result = ComponentFactory.CreateComponent(type, k1, k2);
            }
            componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            result.Start();
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

            if (queue.TryDequeue(out BaseComponent result))
            {
                result.Id = ComponentIdCreator.CreateId();
            }
            else
            {
                result = ComponentFactory.CreateComponent(type, k1, k2, k3);
            }
            componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            result.Start();
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

            if (queue.TryDequeue(out BaseComponent result))
            {
                result.Id = ComponentIdCreator.CreateId();
            }
            else
            {
                result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4);
            }
            componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            result.Start();
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

            if (queue.TryDequeue(out BaseComponent result))
            {
                result.Id = ComponentIdCreator.CreateId();
            }
            else
            {
                result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4, k5);
            }
            componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            result.Start();
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

            if (queue.TryDequeue(out BaseComponent result))
            {
                result.Id = ComponentIdCreator.CreateId();
            }
            else
            {
                result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4, k5, k6);
            }
            componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            result.Start();
            return (T)result;
        }

        public static void PutBack<T>(this T component) where T : BaseComponent
        {
            var baseComponent = component as BaseComponent;
            baseComponent.Id = 0;
            componentDictionary.TryRemove(component.Id, out BaseComponent value);
        }

    }
}
