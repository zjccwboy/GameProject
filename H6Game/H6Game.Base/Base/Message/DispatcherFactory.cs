using System;
using System.Collections.Generic;
using H6Game.Message;
using System.Reflection;

namespace H6Game.Base
{
    public static class DispatcherFactory
    {
        private static HashSet<Type> dispatcherTypes;
        private static Dictionary<uint, IDispatcher> dispatcherDictionary = new Dictionary<uint, IDispatcher>();

        static DispatcherFactory()
        {
            dispatcherTypes = ObjectFactory.GetTypes<IDispatcher>();
            foreach(var type in dispatcherTypes)
            {
               var attributes  =  type.GetCustomAttributes<MessageCMDAttribute>();
                var dispatcher = Activator.CreateInstance(type);
                foreach (var attribute in attributes)
                {
                    if (!dispatcherDictionary.ContainsKey(attribute.MessageCmd))
                    {
                        dispatcherDictionary[attribute.MessageCmd] = (IDispatcher)dispatcher;
                    }
                }
            }
        }

        public static IDispatcher Get(uint messageCmd)
        {
            if(!dispatcherDictionary.TryGetValue(messageCmd, out IDispatcher dispatcher))
            {
                throw new Exception($"CMD:{messageCmd}没有在IDispatcher实现类中加入MessageCMDAttribute.");
            }
            return dispatcher;
        }
    }
}
