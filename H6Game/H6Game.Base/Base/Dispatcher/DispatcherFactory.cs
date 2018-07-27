using System;
using System.Collections.Generic;
using H6Game.Message;
using System.Reflection;
using System.Linq;

namespace H6Game.Base
{
    public static class DispatcherFactory
    {
        private static HashSet<Type> dispatcherTypes;
        private static Dictionary<uint, IDispatcher> dispatcherDictionary = new Dictionary<uint, IDispatcher>();
        private static Dictionary<uint, Type> meesageCmdDictionary = new Dictionary<uint, Type>();

        static DispatcherFactory()
        {
            dispatcherTypes = ObjectFactory.GetTypes<IDispatcher>();
            foreach (var type in dispatcherTypes)
            {
                var attributes = type.GetCustomAttributes<MessageCMDAttribute>();
                var cmds = attributes.Select(a => a.MessageCmd).ToList();
                var dispatcher = (IDispatcher)Activator.CreateInstance(type);

                foreach (var cmd in cmds)
                {
                    meesageCmdDictionary[cmd] = dispatcher.ResponseType;
                    if (!dispatcherDictionary.ContainsKey(cmd))
                    {
                        dispatcherDictionary[cmd] = dispatcher;
                    }
                }
            }
        }

        public static bool TryGetResponse<T>(uint messageCmd, byte[] bytes, out T response)
        {
            if (!meesageCmdDictionary.TryGetValue(messageCmd, out Type type))
            {
                response = default(T);
                return false;
            }
            response = (T)bytes.ConvertToObject(type);
            return true;
        }

        public static IDispatcher Get(uint messageCmd)
        {
            if (!dispatcherDictionary.TryGetValue(messageCmd, out IDispatcher dispatcher))
            {
                throw new Exception($"CMD:{messageCmd}没有在IDispatcher实现类中加入MessageCMDAttribute.");
            }
            return dispatcher;
        }
    }
}
