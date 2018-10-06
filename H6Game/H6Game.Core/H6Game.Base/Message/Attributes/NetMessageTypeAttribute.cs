using System;

namespace H6Game.Base
{
    /// <summary>
    /// 标识网络消息类型特性器
    /// </summary>
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
