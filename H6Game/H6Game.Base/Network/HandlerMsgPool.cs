using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using H6Game.Hotfix.Messages;
using H6Game.Hotfix.Messages.Attributes;
using H6Game.Hotfix.Messages.Enums;

namespace H6Game.Base
{
    public static class HandlerMsgPool
    {
        private static Dictionary<int, HashSet<IHandler>> HandlerDictionary { get; } = new Dictionary<int, HashSet<IHandler>>();
        private static Dictionary<int, HashSet<IActorHandler>> ActorHandlerDictionary { get; } = new Dictionary<int, HashSet<IActorHandler>>();
        private static Dictionary<int, HashSet<Type>> CmdTypeDictionary { get; } = new Dictionary<int, HashSet<Type>>();
        private static Dictionary<Type, HashSet<int>> TypeCmdDictionary { get; } = new Dictionary<Type, HashSet<int>>();
        private static Dictionary<Type, int> MsgCodeDictionary { get; } = new Dictionary<Type, int>();

        static HandlerMsgPool()
        {
            LoadHandler();
            LoadMessageType();
        }

        public static void Load()
        {

        }

        private static void LoadMessageType()
        {
            var msgTypes = TypePool.GetTypes<IMessage>();
            foreach(var type in msgTypes)
            {
                var attribute = type.GetCustomAttribute<MessageTypeAttribute>();

                if (attribute == null)
                    throw new Exception($"类型:{type}必须有MessageTypeAttribute特性器指定消息类型.");

                if (attribute.TypeCode == (int)MessageType.Ignore)
                    continue;

                MsgCodeDictionary[type] = attribute.TypeCode;
            }

            var valTypes = GetValueTypeCode();
            foreach(var val in valTypes)
            {
                MsgCodeDictionary[val.Key] = (int)val.Value;
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

        private static void LoadHandler()
        {
            var handlerTypes = TypePool.GetTypes<IHandler>();
            foreach (var type in handlerTypes)
            {
                var attributes = type.GetCustomAttributes<HandlerCMDAttribute>();
                if (type.IsAbstract)
                    continue;

                if (attributes == null || !attributes.Any())
                    throw new Exception($"类型:{type}必须有HandlerCMDAttribute特性器指定订阅消息类型.");

                var cmds = attributes.Select(a => a.MessageCmds).SelectMany(c => c).Distinct().ToList();
                var handler = (IHandler)Activator.CreateInstance(type);

                foreach (var cmd in cmds)
                {
                    try
                    {
                        if (handler is IActorHandler)
                        {
                            if (!ActorHandlerDictionary.TryGetValue(cmd, out HashSet<IActorHandler> handlers))
                            {
                                handlers = new HashSet<IActorHandler>();
                                ActorHandlerDictionary[cmd] = handlers;
                            }
                            handlers.Add(handler as IActorHandler);
                        }
                        else
                        {
                            if (!HandlerDictionary.TryGetValue(cmd, out HashSet<IHandler> handlers))
                            {
                                handlers = new HashSet<IHandler>();
                                HandlerDictionary[cmd] = handlers;
                            }
                            handlers.Add(handler);
                        }

                        if (handler.MessageType == null)
                            continue;

                        if (!CmdTypeDictionary.TryGetValue(cmd, out HashSet<Type> types))
                        {
                            types = new HashSet<Type>();
                            CmdTypeDictionary[cmd] = types;
                        }
                        types.Add(handler.MessageType);

                        if (!TypeCmdDictionary.TryGetValue(handler.MessageType, out HashSet<int> msgCmds))
                        {
                            msgCmds = new HashSet<int>();
                            TypeCmdDictionary[handler.MessageType] = msgCmds;
                        }
                        msgCmds.Add(cmd);
                    }
                    catch(Exception e)
                    {
                        Log.Logger.Error(e);
                    }
                }
            }
        }

        public static int GetTypeCode(Type type)
        {
            if(!MsgCodeDictionary.TryGetValue(type, out int result))
            {
                throw new Exception($"MessageType:{type} 不存在.");
            }

            return result;
        }

        public static bool IsValidMessage(this Packet packet, Type type)
        {
            return ExistCmd(type, packet.MessageCmd) && ExistType(packet.MessageCmd, type);
        }

        private static bool ExistType(int messageCmd, Type type)
        {
            if (!CmdTypeDictionary.TryGetValue(messageCmd, out HashSet<Type> types))
            {
                return false;
            }
            return types.Contains(type);
        }

        private static bool ExistCmd(Type type, int messageCmd)
        {
            if(!TypeCmdDictionary.TryGetValue(type, out HashSet<int> cmds))
            {
                return false;
            }
            return cmds.Contains(messageCmd);
        }

        public static IEnumerable<IHandler> GetHandler(int messageCmd)
        {
            if (!HandlerDictionary.TryGetValue(messageCmd, out HashSet<IHandler> value))
            {
                throw new Exception($"MessageCMD:{messageCmd} 没有IHandler订阅该消息.");
            }
            return value;
        }

        public static IEnumerable<IActorHandler> GetActorHandler(int messageCmd)
        {
            if(!ActorHandlerDictionary.TryGetValue(messageCmd, out HashSet<IActorHandler> value))
            {
                throw new Exception($"MessageCMD:{messageCmd} 没有ActorHandler订阅该消息.");
            }
            return value;
        }

        public static T GetMessage<T>(this Packet packet)
        {
            return packet.Read<T>();
        }
    }
}
