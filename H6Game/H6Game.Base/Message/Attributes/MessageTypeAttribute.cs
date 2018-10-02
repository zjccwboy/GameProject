using System;

namespace H6Game.Base
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class MessageTypeAttribute : Attribute
    {
        public int TypeCode { get; }

        public MessageTypeAttribute(int type)
        {
            TypeCode = type;
        }

        public MessageTypeAttribute(object type)
        {
            TypeCode = (int)type;
        }

        public MessageTypeAttribute(SysMessageType type)
        {
            TypeCode = (int)type;
        }
    }
}
