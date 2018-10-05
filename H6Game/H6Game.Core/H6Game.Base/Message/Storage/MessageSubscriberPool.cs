using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace H6Game.Base
{
    /// <summary>
    /// 订阅者与消息Code池
    /// </summary>
    public static class MessageSubscriberPool
    {
        private static Dictionary<int, HashSet<ISubscriber>> Subscribers { get; } = new Dictionary<int, HashSet<ISubscriber>>();
        private static Dictionary<int, HashSet<IActorSubscriber>> ActorSubscribers { get; } = new Dictionary<int, HashSet<IActorSubscriber>>();
        private static Dictionary<int, HashSet<Type>> CmdTypes { get; } = new Dictionary<int, HashSet<Type>>();
        private static Dictionary<Type, HashSet<int>> TypeCmds { get; } = new Dictionary<Type, HashSet<int>>();

        public static void Load()
        {
            LoadSubscriber();
        }

        private static void LoadSubscriber()
        {
            var handlerTypes = ObjectPool.GetTypes<ISubscriber>();
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
                    try
                    {
                        if (subscriber is IActorSubscriber)
                        {
                            if (!ActorSubscribers.TryGetValue(cmd, out HashSet<IActorSubscriber> subscribers))
                            {
                                subscribers = new HashSet<IActorSubscriber>();
                                ActorSubscribers[cmd] = subscribers;
                            }
                            subscribers.Add(subscriber as IActorSubscriber);
                        }
                        else
                        {
                            if (!Subscribers.TryGetValue(cmd, out HashSet<ISubscriber> subscribers))
                            {
                                subscribers = new HashSet<ISubscriber>();
                                Subscribers[cmd] = subscribers;
                            }
                            subscribers.Add(subscriber);
                        }

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
                    catch(Exception e)
                    {
                        Log.Error(e, LoggerBllType.System);
                    }
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

            if (ActorSubscribers.TryGetValue(netCommand, out HashSet<IActorSubscriber> actorSubscribers))
            {
                foreach (var subscriber in actorSubscribers)
                {
                    if (subscriber.MessageType == type)
                        result.Add(subscriber);
                }
            }

            return result;
        }

        public static IEnumerable<ISubscriber> GetSubscribers(int netCommand)
        {
            if (!Subscribers.TryGetValue(netCommand, out HashSet<ISubscriber> value))
                throw new Exception($"NetCommand:{netCommand} 没有Subscriber订阅该消息.");

            return value;
        }

        public static IEnumerable<IActorSubscriber> GetActorSubscribers(int netCommand)
        {
            if (!ActorSubscribers.TryGetValue(netCommand, out HashSet<IActorSubscriber> value))
                throw new Exception($"NetCommand:{netCommand} 没有ActorSubscriber订阅该消息.");

            return value;
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
