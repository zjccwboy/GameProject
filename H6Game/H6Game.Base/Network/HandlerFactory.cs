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
        private static Dictionary<int, IHandler> HandlerDictionary = new Dictionary<int, IHandler>();
        private static Dictionary<int, Type> MeesageCmdDictionary = new Dictionary<int, Type>();

        static HandlerFactory()
        {
            var dispatcherTypes = TypeFactory.GetTypes<IHandler>();
            foreach (var type in dispatcherTypes)
            {
                var attributes = type.GetCustomAttributes<HandlerCMDAttribute>();
                var cmds = attributes.Select(a => a.MessageCmds).SelectMany(c=>c).Distinct().ToList();
                var dispatcher = (IHandler)Activator.CreateInstance(type);

                foreach (var cmd in cmds)
                {
                    HandlerDictionary[cmd] = dispatcher;
                    MeesageCmdDictionary[cmd] = dispatcher.ResponseType;
                }
            }
        }

        public static bool TryGetMessage<T>(int messageCmd, byte[] bytes, out T response)
        {
            if (!MeesageCmdDictionary.TryGetValue(messageCmd, out Type cmdType))
            {
                response = default;
                return false;
            }

            var type = typeof(T);
            if (cmdType != type)
            {
                response = default;
                return false;
            }

            response = bytes.ProtoToObject<T>();
            return true;
        }

        public static IHandler Get(int messageCmd)
        {
            if (!HandlerDictionary.TryGetValue(messageCmd, out IHandler handler))
                throw new Exception($"CMD:{messageCmd}没有在IDispatcher实现类中加入MessageCMDAttribute.");

            return handler;
        }
    }
}
