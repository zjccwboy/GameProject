using System;
using System.Collections.Generic;
using H6Game.Message;
using System.Reflection;
using System.Linq;
using System.Text;

namespace H6Game.Base
{
    public static class HandlerFactory
    {
        private static Dictionary<int, IHandler> dispatcherDictionary = new Dictionary<int, IHandler>();
        private static Dictionary<int, Type> meesageCmdDictionary = new Dictionary<int, Type>();

        static HandlerFactory()
        {
            var dispatcherTypes = ObjectFactory.GetTypes<IHandler>();
            foreach (var type in dispatcherTypes)
            {
                var attributes = type.GetCustomAttributes<HandlerCMDAttribute>();
                var cmds = attributes.Select(a => a.MessageCmds).SelectMany(c=>c).Distinct().ToList();
                var dispatcher = (IHandler)Activator.CreateInstance(type);

                foreach (var cmd in cmds)
                {
                    dispatcherDictionary[cmd] = dispatcher;
                    meesageCmdDictionary[cmd] = dispatcher.ResponseType;
                }
            }
        }

        public static bool TryGetMessage<T>(int messageCmd, byte[] bytes, out T response)
        {
            if (!meesageCmdDictionary.TryGetValue(messageCmd, out Type cmdType))
            {
                response = default(T);
                return false;
            }

            var type = typeof(T);
            if (cmdType != type)
            {
                response = default(T);
                return false;
            }

            response = (T)bytes.ConvertToObject(type);
            return true;
        }

        public static IHandler Get(int messageCmd)
        {
            if (!dispatcherDictionary.TryGetValue(messageCmd, out IHandler handler))
                throw new Exception($"CMD:{messageCmd}没有在IDispatcher实现类中加入MessageCMDAttribute.");

            return handler;
        }
    }
}
