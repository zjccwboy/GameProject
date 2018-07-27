using System;
using System.Collections.Generic;
using H6Game.Message;
using System.Reflection;
using System.Linq;

namespace H6Game.Base
{
    public static class DispatcherFactory
    {
        private static Dictionary<int, HashSet<IDispatcher>> dispatcherDictionary = new Dictionary<int, HashSet<IDispatcher>>();
        private static Dictionary<int, HashSet<Type>> meesageCmdDictionary = new Dictionary<int, HashSet<Type>>();

        static DispatcherFactory()
        {
            var dispatcherTypes = ObjectFactory.GetTypes<IDispatcher>();
            foreach (var type in dispatcherTypes)
            {
                var attributes = type.GetCustomAttributes<MessageCMDAttribute>();
                var cmds = attributes.Select(a => a.MessageCmds).SelectMany(c=>c).Distinct().ToList();
                var dispatcher = (IDispatcher)Activator.CreateInstance(type);

                foreach (var cmd in cmds)
                {
                    if (!meesageCmdDictionary.TryGetValue(cmd, out HashSet<Type> messageType))
                    {
                        messageType = new HashSet<Type>();
                        meesageCmdDictionary[cmd] = messageType;
                    }
                    messageType.Add(dispatcher.ResponseType);

                    if(!dispatcherDictionary.TryGetValue(cmd, out HashSet<IDispatcher> dispatchers))
                    {
                        dispatchers = new HashSet<IDispatcher>();
                        dispatcherDictionary[cmd] = dispatchers;
                    }
                    dispatchers.Add(dispatcher);
                }
            }
        }

        public static bool TryGetResponse<T>(int messageCmd, byte[] bytes, out T response)
        {
            var type = typeof(T);
            if (!meesageCmdDictionary.TryGetValue(messageCmd, out HashSet<Type> types))
            {
                response = default(T);
                return false;
            }

            if (!types.Contains(type))
            {
                response = default(T);
                return false;
            }

            response = (T)bytes.ConvertToObject(type);
            return true;
        }

        public static HashSet<IDispatcher> Get(int messageCmd)
        {
            if (!dispatcherDictionary.TryGetValue(messageCmd, out HashSet<IDispatcher> dispatchers))
            {
                throw new Exception($"CMD:{messageCmd}没有在IDispatcher实现类中加入MessageCMDAttribute.");
            }
            return dispatchers;
        }
    }
}
