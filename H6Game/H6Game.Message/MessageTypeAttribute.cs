using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Message
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageTypeAttribute : Attribute
    {
        public int TypeCode { get; }

        public MessageTypeAttribute(MessageType type)
        {
            TypeCode = (int)type;
        }
    }
}
