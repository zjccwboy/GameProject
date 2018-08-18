﻿using H6Game.Entitys;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace H6Game.Base
{
    public static class TypePool
    {
        private static Dictionary<Type, HashSet<Type>> ObjcetDictionary { get; } = new Dictionary<Type, HashSet<Type>>();
        private static Dictionary<Type, MessageType> MsgTypeDictionary { get; } = new Dictionary<Type, MessageType>();
        private static Dictionary<Type, EventType> EventTypeDictionary { get; } = new Dictionary<Type, EventType>();

        static TypePool()
        {
            Load();
            LoadEvent();
        }

        public static HashSet<Type> GetTypes<T>()
        {
            return ObjcetDictionary[typeof(T)];
        }

        public static EventType GetEvent(Type type)
        {
            if(!EventTypeDictionary.TryGetValue(type, out EventType value))
            {
                return EventType.None;
            }
            return value;
        }

        private static void LoadEvent()
        {
            var type = typeof(BaseComponent);
            if(ObjcetDictionary.TryGetValue(type, out HashSet<Type> types))
            {
                foreach(var componentType in types)
                {
                    var attributes = componentType.GetCustomAttributes<EventAttribute>();
                    if (!attributes.Any())
                        continue;

                    EventTypeDictionary[componentType] = attributes.First().EventType;
                }
            }
        }

        /// <summary>
        /// 加载类型数据在内存中
        /// </summary>
        private static void Load()
        {
            var assemblys = AppDomain.CurrentDomain.GetAssemblies();

            var componentBaseType = typeof(BaseComponent);
            var componentTypes = new HashSet<Type>();

            var messageBaseType = typeof(IMessage);
            var messageTypes = new HashSet<Type>();

            var handlerType = typeof(IHandler);
            var handlerTypes = new HashSet<Type>();

            var entityBaseType = typeof(BaseEntity);
            var entityTypes = new HashSet<Type>();

            var rpositoryBaseType = typeof(BaseRpository);
            var rpositoryTypes = new HashSet<Type>();

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
                        componentTypes.Add(t);
                    }
                    else if (handlerType.IsAssignableFrom(t))
                    {
                        handlerTypes.Add(t);
                    }
                    else if (t.BaseType == entityBaseType)
                    {
                        entityTypes.Add(t);
                    }
                    else if (t.BaseType == rpositoryBaseType)
                    {
                        rpositoryTypes.Add(t);
                    }
                }
            }

            ObjcetDictionary[messageBaseType] = messageTypes;
            ObjcetDictionary[componentBaseType] = componentTypes;
            ObjcetDictionary[handlerType] = handlerTypes;
            ObjcetDictionary[entityBaseType] = entityTypes;
            ObjcetDictionary[rpositoryBaseType] = rpositoryTypes;
        }

    }
}
