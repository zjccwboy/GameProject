using H6Game.Message;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public static class ObjectFactory
    {
        private static readonly HashSet<Type> messageHandlerTypes = new HashSet<Type>();
        private static readonly HashSet<Type> messageTypes = new HashSet<Type>();
        private static readonly HashSet<Type> componentTypes = new HashSet<Type>();
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
            var messageHandlerBaseType = typeof(IMessageHandler);
            var messageBaseTypes = typeof(IMessage);
            var componentBaseType = typeof(BaseComponent);

            foreach (var assembly in assemblys)
            {
                var types = assembly.GetTypes();
                foreach (var t in types)
                {
                    if (messageHandlerBaseType.IsAssignableFrom(t) && t != messageHandlerBaseType)
                    {
                        messageHandlerTypes.Add(t);
                    }
                    else if(messageBaseTypes.IsAssignableFrom(t) && t != messageBaseTypes)
                    {
                        messageTypes.Add(t);
                    }
                    else if(t.BaseType == componentBaseType)
                    {
                        componentTypes.Add(t);
                    }
                }
            }

            dictionary[messageHandlerBaseType] = messageHandlerTypes;
            dictionary[messageBaseTypes] = messageTypes;
            dictionary[componentBaseType] = componentTypes;
        }

    }
}
