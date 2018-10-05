using System;

namespace H6Game.Base
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class NetMessageTypeAttribute : Attribute
    {
        public int TypeCode { get; }

        public NetMessageTypeAttribute(int type)
        {
            TypeCode = type;
        }

        public NetMessageTypeAttribute(object type)
        {
            TypeCode = (int)type;
        }
    }
}
