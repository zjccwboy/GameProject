using H6Game.Message;
using System;
using System.Collections.Generic;

namespace H6Game.Base
{
    public static class ObjectFactory
    {
        private static readonly Dictionary<Type, HashSet<Type>> dictionary = new Dictionary<Type, HashSet<Type>>();

        static ObjectFactory()
        {
            Load();
        }

        public static HashSet<Type> GetTypes<T>()
        {
            return dictionary[typeof(T)];
        }

        /// <summary>
        /// 加载类型数据在内存中
        /// </summary>
        private static void Load()
        {
            var assemblys = AppDomain.CurrentDomain.GetAssemblies();
            var handlerType = typeof(IHandler);
            var messageBaseType = typeof(IMessage);
            var componentBaseType = typeof(BaseComponent);

            var handlerTypes = new HashSet<Type>();
            var messageTypes = new HashSet<Type>();
            var componentTypes = new HashSet<Type>();

            foreach (var assembly in assemblys)
            {
                var types = assembly.GetTypes();
                foreach (var t in types)
                {
                    if (!t.IsClass)
                    {
                        continue;
                    }

                    if (t.IsAbstract)
                    {
                        continue;
                    }

                    if (handlerType.IsAssignableFrom(t))
                    {
                        handlerTypes.Add(t);
                    }
                    else if(messageBaseType.IsAssignableFrom(t))
                    {
                        messageTypes.Add(t);
                    }
                    else if(t.BaseType == componentBaseType)
                    {
                        componentTypes.Add(t);
                    }
                }
            }

            dictionary[handlerType] = handlerTypes;
            dictionary[messageBaseType] = messageTypes;
            dictionary[componentBaseType] = componentTypes;
        }

    }
}
