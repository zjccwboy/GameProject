using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace H6Game.Base
{
    public static class MessageCommandPool
    {
        private static Dictionary<Type, int> MsgCodes { get; } = new Dictionary<Type, int>();
        private static Dictionary<int, Type> CodeMsgTypes { get; } = new Dictionary<int, Type>();

        public static int GetMsgCode(Type type)
        {
            if (!MsgCodes.TryGetValue(type, out int result))
                throw new Exception($"MessageType:{type} 不存在.");

            return result;
        }

        public static Type GetMsgType(int msgCode)
        {
            if (!CodeMsgTypes.TryGetValue(msgCode, out Type result))
                throw new Exception($"MessageType:{msgCode} 不存在.");

            return result;
        }

        public static void Load()
        {
            var msgTypes = ObjectPool.GetTypes<IMessage>();
            foreach (var type in msgTypes)
            {
                var attribute = type.GetCustomAttribute<NetMessageTypeAttribute>();

                if (attribute == null)
                    throw new Exception($"类型:{type}必须有MessageTypeAttribute特性器指定消息类型.");

                if (attribute.TypeCode == (int)BasicMessageType.Ignore)
                    continue;

                MsgCodes[type] = attribute.TypeCode;
                CodeMsgTypes[attribute.TypeCode] = type;
            }

            var valTypes = GetValueTypeCode();
            foreach (var val in valTypes)
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
    }
}
