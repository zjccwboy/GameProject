﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace H6Game.Base.Component
{
    public static class ObjectStorage
    {
        private static ConcurrentDictionary<int, BaseComponent> IdComponents{ get; } = new ConcurrentDictionary<int, BaseComponent>();
        private static ConcurrentDictionary<Type, ConcurrentQueue<BaseComponent>> TypeComponents { get; } = new ConcurrentDictionary<Type, ConcurrentQueue<BaseComponent>>();
        private static ConcurrentDictionary<Type, BaseComponent> SingleCaseComponents { get; } = new ConcurrentDictionary<Type, BaseComponent>();
        private static HashSet<Type> SingleTypes { get; } = new HashSet<Type>();

        public static void Load()
        {
            var types = ComponentFactory.CmponentTypes;
            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<SingleCaseAttribute>();
                if (attribute != null)
                {
                    SingleTypes.Add(type);
                    continue;
                }

                if (!TypeComponents.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    TypeComponents[type] = queue;
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
            while (IdComponents.ContainsKey(component.Id))
                component.Id = ComponentIdCreator.CreateId();
        }

        public static T Fetch<T>() where T: BaseComponent
        {
            var type = typeof(T);
            BaseComponent result;
            if (IsSingleType(type))
            {
                if(!SingleCaseComponents.TryGetValue(type, out result))
                {
                    result = ComponentFactory.CreateComponent(type);
                    SetId(result);
                    SingleCaseComponents[type] = result;
                }
            }
            else
            {
                if (!TypeComponents.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    TypeComponents[type] = queue;
                }

                if (!queue.TryDequeue(out result))
                {
                    result = ComponentFactory.CreateComponent(type);
                }

                SetId(result);
                IdComponents[result.Id] = result;
            }
            return (T)result;
        }

        public static T Fetch<T, K1>(K1 k1) where T : BaseComponent
        {
            var type = typeof(T);
            BaseComponent result;
            if (IsSingleType(type))
            {
                if (!SingleCaseComponents.TryGetValue(type, out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1);
                    SetId(result);
                    SingleCaseComponents[type] = result;
                }
            }
            else
            {
                if (!TypeComponents.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    TypeComponents[type] = queue;
                }

                if (!queue.TryDequeue(out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1);
                }

                SetId(result);
                IdComponents[result.Id] = result;
            }
            return (T)result;
        }

        public static BaseComponent Fetch(Type type)
        {
            BaseComponent result;
            if (IsSingleType(type))
            {
                if (!SingleCaseComponents.TryGetValue(type, out result))
                {
                    result = ComponentFactory.CreateComponent(type);
                    SetId(result);
                    SingleCaseComponents[type] = result;
                }
            }
            else
            {
                if (!TypeComponents.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    TypeComponents[type] = queue;
                }

                if (!queue.TryDequeue(out result))
                {
                    result = ComponentFactory.CreateComponent(type);
                }

                SetId(result);
                IdComponents[result.Id] = result;
            }
            return result;
        }

        public static T Fetch<T, K1, K2>(K1 k1, K2 k2) where T : BaseComponent
        {
            var type = typeof(T);
            BaseComponent result;
            if (IsSingleType(type))
            {
                if (!SingleCaseComponents.TryGetValue(type, out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2);
                    SetId(result);
                    SingleCaseComponents[type] = result;
                }
            }
            else
            {
                if (!TypeComponents.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    TypeComponents[type] = queue;
                }

                if (!queue.TryDequeue(out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2);
                }

                SetId(result);
                IdComponents[result.Id] = result;
            }
            return (T)result;
        }

        public static T Fetch<T, K1, K2, K3>(K1 k1, K2 k2, K3 k3) where T : BaseComponent
        {
            var type = typeof(T);
            BaseComponent result;
            if (IsSingleType(type))
            {
                if (!SingleCaseComponents.TryGetValue(type, out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2, k3);
                    SetId(result);
                    SingleCaseComponents[type] = result;
                }
            }
            else
            {
                if (!TypeComponents.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    TypeComponents[type] = queue;
                }

                if (!queue.TryDequeue(out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2, k3);
                }

                SetId(result);
                IdComponents[result.Id] = result;
            }
            return (T)result;
        }

        public static T Fetch<T, K1, K2, K3, K4>(K1 k1, K2 k2, K3 k3, K4 k4) where T : BaseComponent
        {
            var type = typeof(T);
            BaseComponent result;
            if (IsSingleType(type))
            {
                if (!SingleCaseComponents.TryGetValue(type, out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4);
                    SetId(result);
                    SingleCaseComponents[type] = result;
                }
            }
            else
            {
                if (!TypeComponents.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    TypeComponents[type] = queue;
                }

                if (!queue.TryDequeue(out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4);
                }

                SetId(result);
                IdComponents[result.Id] = result;
            }
            return (T)result;
        }

        public static T Fetch<T, K1, K2, K3, K4, K5>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5) where T : BaseComponent
        {
            var type = typeof(T);
            BaseComponent result;
            if (IsSingleType(type))
            {
                if (!SingleCaseComponents.TryGetValue(type, out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4, k5);
                    SetId(result);
                    SingleCaseComponents[type] = result;
                }
            }
            else
            {
                if (!TypeComponents.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    TypeComponents[type] = queue;
                }

                if (!queue.TryDequeue(out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4, k5);
                }

                SetId(result);
                IdComponents[result.Id] = result;
            }
            return (T)result;
        }

        public static T Fetch<T, K1, K2, K3, K4, K5, K6>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5, K6 k6) where T : BaseComponent
        {
            var type = typeof(T);
            BaseComponent result;
            if (IsSingleType(type))
            {
                if (!SingleCaseComponents.TryGetValue(type, out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4, k5, k6);
                    SetId(result);
                    SingleCaseComponents[type] = result;
                }
            }
            else
            {
                if (!TypeComponents.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
                {
                    queue = new ConcurrentQueue<BaseComponent>();
                    TypeComponents[type] = queue;
                }

                if (!queue.TryDequeue(out result))
                {
                    result = ComponentFactory.CreateComponent(type, k1, k2, k3, k4, k5, k6);
                }

                SetId(result);
                IdComponents[result.Id] = result;
            }
            return (T)result;
        }

        public static void PutBack(this BaseComponent component)
        {
            if (!IdComponents.TryRemove(component.Id, out BaseComponent val))
                return;

            var type = component.GetType();
            if (!TypeComponents.TryGetValue(type, out ConcurrentQueue<BaseComponent> queue))
            {
                queue = new ConcurrentQueue<BaseComponent>();
                TypeComponents[type] = queue;
            }

            component.Id = 0;
            queue.Enqueue(component);
        }
    }
}
