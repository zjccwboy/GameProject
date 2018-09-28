using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using H6Game.Hotfix.Messages;
using H6Game.Hotfix.Messages.Attributes;
using H6Game.Hotfix.Messages.Enums;

namespace H6Game.Base
{
    /// <summary>
    /// 订阅者与消息Code池
    /// </summary>
    public static class SubscriberMsgPool
    {
        private static Dictionary<int, HashSet<ISubscriber>> Subscribers { get; } = new Dictionary<int, HashSet<ISubscriber>>();
        private static Dictionary<int, HashSet<IActorSubscriber>> ActorSubscribers { get; } = new Dictionary<int, HashSet<IActorSubscriber>>();
        private static Dictionary<int, HashSet<Type>> CmdTypes { get; } = new Dictionary<int, HashSet<Type>>();
        private static Dictionary<Type, HashSet<int>> TypeCmds { get; } = new Dictionary<Type, HashSet<int>>();
        private static Dictionary<Type, int> MsgCodes { get; } = new Dictionary<Type, int>();

        public static void Load()
        {
            LoadSubscriber();
            LoadMessageType();
        }

        private static void LoadMessageType()
        {
            var msgTypes = ObjectPool.GetTypes<IMessage>();
            foreach(var type in msgTypes)
            {
                var attribute = type.GetCustomAttribute<MessageTypeAttribute>();

                if (attribute == null)
                    throw new Exception($"类型:{type}必须有MessageTypeAttribute特性器指定消息类型.");

                if (attribute.TypeCode == (int)MessageType.Ignore)
                    continue;

                MsgCodes[type] = attribute.TypeCode;
            }

            var valTypes = GetValueTypeCode();
            foreach(var val in valTypes)
            {
                MsgCodes[val.Key] = (int)val.Value;
            }
        }

        private static IDictionary<Type, MessageType> GetValueTypeCode()
        {
            var result = new Dictionary<Type, MessageType>();
            result.Add(typeof(string), MessageType.String);
            result.Add(typeof(int), MessageType.Int);
            result.Add(typeof(uint), MessageType.Uint);
            result.Add(typeof(long), MessageType.Long);
            result.Add(typeof(ulong), MessageType.ULong);
            result.Add(typeof(short), MessageType.Short);
            result.Add(typeof(ushort), MessageType.UShort);
            result.Add(typeof(byte), MessageType.Byte);
            result.Add(typeof(sbyte), MessageType.Sbyte);
            result.Add(typeof(float), MessageType.Float);
            result.Add(typeof(double), MessageType.Double);
            result.Add(typeof(decimal), MessageType.Decimal);
            result.Add(typeof(char), MessageType.Char);
            result.Add(typeof(bool), MessageType.BooLean);
            result.Add(typeof(Guid), MessageType.Guid);
            result.Add(typeof(DateTime), MessageType.DateTime);
            return result;
        }

        private static void LoadSubscriber()
        {
            var handlerTypes = ObjectPool.GetTypes<ISubscriber>();
            foreach (var type in handlerTypes)
            {
                var attributes = type.GetCustomAttributes<SubscriberCMDAttribute>();
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
                            if (!ActorSubscribers.TryGetValue(cmd, out HashSet<IActorSubscriber> handlers))
                            {
                                handlers = new HashSet<IActorSubscriber>();
                                ActorSubscribers[cmd] = handlers;
                            }
                            handlers.Add(subscriber as IActorSubscriber);
                        }
                        else
                        {
                            if (!Subscribers.TryGetValue(cmd, out HashSet<ISubscriber> handlers))
                            {
                                handlers = new HashSet<ISubscriber>();
                                Subscribers[cmd] = handlers;
                            }
                            handlers.Add(subscriber);
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

        public static int GetMsgCode(Type type)
        {
            if(!MsgCodes.TryGetValue(type, out int result))
            {
                throw new Exception($"MessageType:{type} 不存在.");
            }

            return result;
        }

        public static bool IsValidMessage(this Packet packet, Type type)
        {
            return ExistCmd(type, packet.MessageCmd) && ExistType(packet.MessageCmd, type);
        }

        public static IEnumerable<ISubscriber> GetSubscriber(int messageCmd)
        {
            if (!Subscribers.TryGetValue(messageCmd, out HashSet<ISubscriber> value))
            {
                throw new Exception($"MessageCMD:{messageCmd} 没有Subscriber订阅该消息.");
            }
            return value;
        }

        public static IEnumerable<IActorSubscriber> GetActorSubscriber(int messageCmd)
        {
            if (!ActorSubscribers.TryGetValue(messageCmd, out HashSet<IActorSubscriber> value))
            {
                throw new Exception($"MessageCMD:{messageCmd} 没有ActorSubscriber订阅该消息.");
            }
            return value;
        }

        public static T GetMessage<T>(this Packet packet)
        {
            return packet.Read<T>();
        }

        private static bool ExistType(int messageCmd, Type type)
        {
            if (!CmdTypes.TryGetValue(messageCmd, out HashSet<Type> types))
            {
                return false;
            }
            return types.Contains(type);
        }

        private static bool ExistCmd(Type type, int messageCmd)
        {
            if(!TypeCmds.TryGetValue(type, out HashSet<int> cmds))
            {
                return false;
            }
            return cmds.Contains(messageCmd);
        }
    }
}
