using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace H6Game.Base
{
    internal static class ComponentPool
    {
        private static ConcurrentDictionary<int, BaseComponent> componentDictionary { get; } = new ConcurrentDictionary<int, BaseComponent>();
        private static ConcurrentDictionary<Type, ConcurrentQueue<BaseComponent>> componentTypeDictionary { get; } = new ConcurrentDictionary<Type, ConcurrentQueue<BaseComponent>>();
        private static ConcurrentDictionary<Type, BaseComponent> SingleCaseDictionary { get; } = new ConcurrentDictionary<Type, BaseComponent>();
        private static HashSet<Type> SingleTypes { get; } = new HashSet<Type>();
        static ComponentPool()
        {
            var types = ComponentFactory.CmponentTypes;
            foreach(var type in types)
            {
                var attribute = type.GetCustomAttribute<SingletCaseAttribute>();
                if(attribute != null)
                {
                    SingleTypes.Add(type);
                    continue;
                }

                if(!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    componentTypeDictionary.TryAdd(type, queue);
                }
            }
        }

        public static bool IsSingleType(Type type)
        {
            return SingleTypes.Contains(type);
        }

        private static void SetId(BaseComponent component)
        {
            component.Id = ComponentIdCreator.CreateId();
            while (componentDictionary.ContainsKey(component.Id))
                component.Id = ComponentIdCreator.CreateId();
        }

        public static T Fetch<T>() where T: BaseComponent
        {
            var type = typeof(T);
            BaseComponent result;
            if (IsSingleType(type))
            {
                if(!SingleCaseDictionary.TryGetValue(type, out result))
                {
                    result = ComponentFactory.CreateComponent(type);
                    SetId(result);
                    SingleCaseDictionary[type] = result;
                }
            }
            else
            {
                if (!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    componentTypeDictionary.TryAdd(type, queue);
                }

                if (!queue.TryDequeue(out result))
                {
                    result = ComponentFactory.CreateComponent(type);
                }

                SetId(result);
                componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            }
            return (T)result;
        }

        public static T Fetch<T, K1>(K1 k1) where T : BaseComponent
        {
            var type = typeof(T);
            BaseComponent result;
            if (IsSingleType(type))
            {
                if (!SingleCaseDictionary.TryGetValue(type, out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1);
                    SetId(result);
                    SingleCaseDictionary[type] = result;
                }
            }
            else
            {
                if (!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    componentTypeDictionary.TryAdd(type, queue);
                }

                if (!queue.TryDequeue(out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1);
                }

                SetId(result);
                componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            }
            return (T)result;
        }

        public static T Fetch<T, K1, K2>(K1 k1, K2 k2) where T : BaseComponent
        {
            var type = typeof(T);
            BaseComponent result;
            if (IsSingleType(type))
            {
                if (!SingleCaseDictionary.TryGetValue(type, out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2);
                    SetId(result);
                    SingleCaseDictionary[type] = result;
                }
            }
            else
            {
                if (!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    componentTypeDictionary.TryAdd(type, queue);
                }

                if (!queue.TryDequeue(out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2);
                }

                SetId(result);
                componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            }
            return (T)result;
        }

        public static T Fetch<T, K1, K2, K3>(K1 k1, K2 k2, K3 k3) where T : BaseComponent
        {
            var type = typeof(T);
            BaseComponent result;
            if (IsSingleType(type))
            {
                if (!SingleCaseDictionary.TryGetValue(type, out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2, k3);
                    SetId(result);
                    SingleCaseDictionary[type] = result;
                }
            }
            else
            {
                if (!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    componentTypeDictionary.TryAdd(type, queue);
                }

                if (!queue.TryDequeue(out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2, k3);
                }

                SetId(result);
                componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            }
            return (T)result;
        }

        public static T Fetch<T, K1, K2, K3, K4>(K1 k1, K2 k2, K3 k3, K4 k4) where T : BaseComponent
        {
            var type = typeof(T);
            BaseComponent result;
            if (IsSingleType(type))
            {
                if (!SingleCaseDictionary.TryGetValue(type, out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4);
                    SetId(result);
                    SingleCaseDictionary[type] = result;
                }
            }
            else
            {
                if (!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    componentTypeDictionary.TryAdd(type, queue);
                }

                if (!queue.TryDequeue(out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4);
                }

                SetId(result);
                componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            }
            return (T)result;
        }

        public static T Fetch<T, K1, K2, K3, K4, K5>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5) where T : BaseComponent
        {
            var type = typeof(T);
            BaseComponent result;
            if (IsSingleType(type))
            {
                if (!SingleCaseDictionary.TryGetValue(type, out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4, k5);
                    SetId(result);
                    SingleCaseDictionary[type] = result;
                }
            }
            else
            {
                if (!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    componentTypeDictionary.TryAdd(type, queue);
                }

                if (!queue.TryDequeue(out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4, k5);
                }

                SetId(result);
                componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            }
            return (T)result;
        }

        public static T Fetch<T, K1, K2, K3, K4, K5, K6>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5, K6 k6) where T : BaseComponent
        {
            var type = typeof(T);
            BaseComponent result;
            if (IsSingleType(type))
            {
                if (!SingleCaseDictionary.TryGetValue(type, out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4, k5, k6);
                    SetId(result);
                    SingleCaseDictionary[type] = result;
                }
            }
            else
            {
                if (!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    componentTypeDictionary.TryAdd(type, queue);
                }

                if (!queue.TryDequeue(out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4, k5, k6);
                }

                SetId(result);
                componentDictionary.AddOrUpdate(result.Id, result, (k, v) => { return result; });
            }
            return (T)result;
        }

        public static void PutBack<T>(this T component) where T : BaseComponent
        {
            var baseComponent = component as BaseComponent;
            if (!componentDictionary.TryRemove(component.Id, out BaseComponent value))
                return;

            var type = typeof(T);
            if (!componentTypeDictionary.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                return;

            value.Id = 0;
            value.IsStart = false;

            queue.Enqueue(component);
        }
    }
}
