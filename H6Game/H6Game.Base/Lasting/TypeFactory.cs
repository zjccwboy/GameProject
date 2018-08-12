﻿using H6Game.Message;
using System;
using System.Collections.Generic;

namespace H6Game.Base
{
    public static class TypeFactory
    {
        private static Dictionary<Type, HashSet<Type>> ObjcetDictionary { get; } = new Dictionary<Type, HashSet<Type>>();
        private static Dictionary<Type, MessageType> MsgTypeDictionary { get; } = new Dictionary<Type, MessageType>();

        static TypeFactory()
        {
            Load();
        }

        public static HashSet<Type> GetTypes<T>()
        {
            return ObjcetDictionary[typeof(T)];
        }

        /// <summary>
        /// 加载类型数据在内存中
        /// </summary>
        private static void Load()
        {
            var assemblys = AppDomain.CurrentDomain.GetAssemblies();

            var componentBaseType = typeof(BaseComponent);
            var componentBaseTypes = new HashSet<Type>();

            var messageTypes = new HashSet<Type>();
            var messageBaseType = typeof(IMessage);

            var dispatcherType = typeof(IHandler);
            var dispatcherTypes = new HashSet<Type>();

            foreach (var assembly in assemblys)
            {
                var types = assembly.GetTypes();
                foreach (var t in types)
                {
                    if (!t.IsClass)
                        continue;

                    if (t.IsAbstract)
                        continue;

                    if (messageBaseType.IsAssignableFrom(t))
                    {
                        messageTypes.Add(t);
                    }
                    else if(t.BaseType == componentBaseType)
                    {
                        componentBaseTypes.Add(t);
                    }
                    else if (dispatcherType.IsAssignableFrom(t))
                    {
                        dispatcherTypes.Add(t);
                    }
                }
            }

            ObjcetDictionary[messageBaseType] = messageTypes;
            ObjcetDictionary[componentBaseType] = componentBaseTypes;
            ObjcetDictionary[dispatcherType] = dispatcherTypes;
        }

    }
}
