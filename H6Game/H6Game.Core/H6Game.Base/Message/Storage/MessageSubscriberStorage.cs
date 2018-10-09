using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace H6Game.Base
{
    /// <summary>
    /// 订阅者与消息Code池
    /// </summary>
    public static class MessageSubscriberStorage
    {
        private static Dictionary<int, HashSet<ISubscriber>> Subscribers { get; } = new Dictionary<int, HashSet<ISubscriber>>();
        private static Dictionary<int, HashSet<Type>> CmdTypes { get; } = new Dictionary<int, HashSet<Type>>();
        private static Dictionary<Type, HashSet<int>> TypeCmds { get; } = new Dictionary<Type, HashSet<int>>();

        public static void Load()
        {
            LoadSubscriber();
        }

        private static void LoadSubscriber()
        {
            var handlerTypes = ObjectTypeStorage.GetTypes<ISubscriber>();
            foreach (var type in handlerTypes)
            {
                var attributes = type.GetCustomAttributes<NetCommandAttribute>();
                if (type.IsAbstract)
                    continue;

                if (attributes == null || !attributes.Any())
                    throw new Exception($"类型:{type}必须有SubscriberCMDAttribute特性器指定订阅消息类型.");

                var cmds = attributes.Select(a => a.MessageCmds).SelectMany(c => c).Distinct().ToList();
                var subscriber = (ISubscriber)Activator.CreateInstance(type);

                foreach (var cmd in cmds)
                {
                    //校验订阅的NetCommand是否相同，相同抛出一个异常。
                    Validate(subscriber, cmd);

                    if (!Subscribers.TryGetValue(cmd, out HashSet<ISubscriber> subscribers))
                    {
                        subscribers = new HashSet<ISubscriber>();
                        Subscribers[cmd] = subscribers;
                    }
                    subscribers.Add(subscriber);

                    if (subscriber.MessageType == null)
                        continue;

                    if (!CmdTypes.TryGetValue(cmd, out HashSet<Type> types))
                    {
                        types = new HashSet<Type>();
                        CmdTypes[cmd] = types;
                    }
                    types.Add(subscriber.MessageType);

                    if (!TypeCmds.TryGetValue(subscriber.MessageType, out HashSet<int> msgCmds))
                    {
                        msgCmds = new HashSet<int>();
                        TypeCmds[subscriber.MessageType] = msgCmds;
                    }
                    msgCmds.Add(cmd);
                }
            }
        }

        /// <summary>
        /// 检查Subscriber派生类是否订阅了NetCommand与指定消息类型
        /// </summary>
        /// <param name="netCommand"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool ExistSubscriberCmd(int netCommand, Type type)
        {
            return ExistCmd(type, netCommand) && ExistType(type, netCommand);
        }

        /// <summary>
        /// 获取指定订阅NetCommand与指定消息类型集合
        /// </summary>
        /// <param name="netCommand"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<ISubscriber> GetSubscribers(int netCommand, Type type)
        {
            var result = new List<ISubscriber>();
            if(Subscribers.TryGetValue(netCommand, out HashSet<ISubscriber> subscribers))
            {
                foreach(var subscriber in subscribers)
                {
                    if (subscriber.MessageType == type)
                        result.Add(subscriber);
                }
            }

            return result;
        }

        public static bool TryGetSubscribers(int netCommand, out HashSet<ISubscriber> subscribers)
        {
            return Subscribers.TryGetValue(netCommand, out subscribers);
        }

        private static void Validate(ISubscriber subscriber, int netCommand)
        {
            if(Subscribers.TryGetValue(netCommand, out HashSet<ISubscriber> oldSubscribers))
            {
                foreach(var odlSubscriber in oldSubscribers)
                {
                    if(subscriber.MessageType == odlSubscriber.MessageType)
                    {
                        throw new SubscribeException($"类:{subscriber.GetType()}与类:{odlSubscriber.GetType()}订阅消息类型相同，消息类型相同时不能订阅一样的NetCommand:{netCommand}");
                    }
                }
            }
        }

        private static bool ExistType(Type type, int netCommand)
        {
            if (!CmdTypes.TryGetValue(netCommand, out HashSet<Type> types))
            {
                return false;
            }
            return types.Contains(type);
        }

        private static bool ExistCmd(Type type, int netCommand)
        {
            if(!TypeCmds.TryGetValue(type, out HashSet<int> cmds))
            {
                return false;
            }
            return cmds.Contains(netCommand);
        }
    }
}
