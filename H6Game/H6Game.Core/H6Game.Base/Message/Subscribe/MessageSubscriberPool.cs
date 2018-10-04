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
        private static Dictionary<Type, int> MsgCodes { get; } = new Dictionary<Type, int>();
        private static Dictionary<int, Type> CodeMsgTypes { get; } = new Dictionary<int, Type>();

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

                if (attribute.TypeCode == (int)BasicMessageType.Ignore)
                    continue;

                MsgCodes[type] = attribute.TypeCode;
                CodeMsgTypes[attribute.TypeCode] = type;
            }

            var valTypes = GetValueTypeCode();
            foreach(var val in valTypes)
            {
                MsgCodes[val.Key] = (int)val.Value;
                CodeMsgTypes[(int)val.Value] = val.Key;
            }
        }

        private static IDictionary<Type, BasicMessageType> GetValueTypeCode()
        {
            var result = new Dictionary<Type, BasicMessageType>();
            result.Add(typeof(string), BasicMessageType.String);
            result.Add(typeof(int), BasicMessageType.Int);
            result.Add(typeof(uint), BasicMessageType.Uint);
            result.Add(typeof(long), BasicMessageType.Long);
            result.Add(typeof(ulong), BasicMessageType.ULong);
            result.Add(typeof(short), BasicMessageType.Short);
            result.Add(typeof(ushort), BasicMessageType.UShort);
            result.Add(typeof(byte), BasicMessageType.Byte);
            result.Add(typeof(sbyte), BasicMessageType.Sbyte);
            result.Add(typeof(float), BasicMessageType.Float);
            result.Add(typeof(double), BasicMessageType.Double);
            result.Add(typeof(decimal), BasicMessageType.Decimal);
            result.Add(typeof(char), BasicMessageType.Char);
            result.Add(typeof(bool), BasicMessageType.BooLean);
            result.Add(typeof(Guid), BasicMessageType.Guid);
            result.Add(typeof(DateTime), BasicMessageType.DateTime);
            return result;
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
                throw new Exception($"MessageType:{type} 不存在.");

            return result;
        }

        public static Type GetMsgType(int msgCode)
        {
            if(!CodeMsgTypes.TryGetValue(msgCode, out Type result))
                throw new Exception($"MessageType:{msgCode} 不存在.");

            return result;
        }

        public static bool IsValidMessage(this Packet packet, Type type)
        {
            return ExistCmd(type, packet.MessageCmd) && ExistType(packet.MessageCmd, type);
        }

        public static IEnumerable<ISubscriber> GetSubscriber(int messageCmd)
        {
            if (!Subscribers.TryGetValue(messageCmd, out HashSet<ISubscriber> value))
                throw new Exception($"MessageCMD:{messageCmd} 没有Subscriber订阅该消息.");

            return value;
        }

        public static IEnumerable<IActorSubscriber> GetActorSubscriber(int messageCmd)
        {
            if (!ActorSubscribers.TryGetValue(messageCmd, out HashSet<IActorSubscriber> value))
                throw new Exception($"MessageCMD:{messageCmd} 没有ActorSubscriber订阅该消息.");

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
