using H6Game.Message;
using System;
using System.Collections.Concurrent;

namespace H6Game.Base
{
    public static class MessageDeserialize
    {
        private static ConcurrentDictionary<Type, uint> messageTypeCmd = new ConcurrentDictionary<Type, uint>();

        public static bool TryGetMessage<T>(Packet packet, out T message) where T : IMessage
        {
            var type = typeof(T);
            if (!messageTypeCmd.ContainsKey(type))
            {
                message = default(T);
                return false;
            }
            message = (T)packet.Data.ConvertToObject(type);
            return true;
        }
    }
}
