﻿using H6Game.Base.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace H6Game.Base.Component
{
    public static class ObjectTypeStorage
    {
        private static Dictionary<Type, HashSet<Type>> Objcets { get; } = new Dictionary<Type, HashSet<Type>>();
        private static Dictionary<Type, EventType> EventTypes { get; } = new Dictionary<Type, EventType>();
        public static List<Assembly> Assemblies { get; private set; } = new List<Assembly>();

        public static void Load()
        {
            LoadType();
            LoadEvent();
        }

        public static HashSet<Type> GetTypes<T>()
        {
            return Objcets[typeof(T)];
        }

        public static EventType GetEvent(Type type)
        {
            if(!EventTypes.TryGetValue(type, out EventType value))
            {
                return EventType.None;
            }
            return value;
        }

        private static void LoadEvent()
        {
            var type = typeof(BaseComponent);
            if(Objcets.TryGetValue(type, out HashSet<Type> types))
            {
                foreach(var componentType in types)
                {
                    var attributes = componentType.GetCustomAttributes<ComponentEventAttribute>();
                    if (!attributes.Any())
                        continue;

                    EventTypes[componentType] = attributes.First().EventType;
                }
            }
        }

        /// <summary>
        /// 加载类型数据在内存中
        /// </summary>
        private static void LoadType()
        {
            var assemblyNames = Assembly.GetEntryAssembly().GetReferencedAssemblies().Where(a => a.Name.StartsWith("H6Game")).ToList();
            var assemblys = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("H6Game")).ToList();
            foreach (var name in assemblyNames)
            {
                var exist = false;
                foreach(var assembly in assemblys)
                {
                    if (assembly.GetName().Name == name.Name)
                    {
                        exist = true;
                        break;
                    }
                }

                if(!exist)
                {
                    var assembly = Assembly.Load(name);
                    assemblys.Add(assembly);
                }
            }
            Assemblies = assemblys;

            var componentBaseType = typeof(BaseComponent);
            var componentTypes = new HashSet<Type>();
            Objcets[componentBaseType] = componentTypes;

            var messageBaseType = typeof(IMessage);
            var messageTypes = new HashSet<Type>();
            Objcets[messageBaseType] = messageTypes;

            var subscriberType = typeof(ISubscriber);
            var subscriberTypes = new HashSet<Type>();
            Objcets[subscriberType] = subscriberTypes;

            var entityBaseType = typeof(BaseEntity);
            var entityTypes = new HashSet<Type>();
            Objcets[entityBaseType] = entityTypes;

            var rpositoryBaseType = typeof(IRpository);
            var rpositoryTypes = new HashSet<Type>();
            Objcets[rpositoryBaseType] = rpositoryTypes;

            var actorBaseType = typeof(BaseActor);
            var actorTypes = new HashSet<Type>();
            Objcets[actorBaseType] = actorTypes;

            foreach (var assembly in assemblys)
            {
                try
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
                        if (subscriberType.IsAssignableFrom(t))
                        {
                            subscriberTypes.Add(t);
                        }
                        if (rpositoryBaseType.IsAssignableFrom(t))
                        {
                            rpositoryTypes.Add(t);
                        }
                        if (CompareBaseType(t, componentBaseType))
                        {
                            componentTypes.Add(t);
                        }
                        if (CompareBaseType(t, actorBaseType))
                        {
                            actorTypes.Add(t);
                        }
                        if (CompareBaseType(t, entityBaseType))
                        {
                            entityTypes.Add(t);
                        }
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        private static bool CompareBaseType(Type compare, Type baseType)
        {
            if (compare.BaseType == null)
                return false;

            if (compare.BaseType == typeof(object))
                return false;

            if (compare.BaseType == baseType)
                return true;

            return CompareBaseType(compare.BaseType, baseType);
        }

    }
}
