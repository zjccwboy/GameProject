using System;

namespace H6Game.Base.Message
{
    /// <summary>
    /// 标识网络消息类型特性器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class NetMessageTypeAttribute : Attribute
    {
        public ushort TypeCode { get; }

        public NetMessageTypeAttribute(int type)
        {
            TypeCode = (ushort)type;
        }

        public NetMessageTypeAttribute(object type)
        {
            TypeCode = (ushort)type;
        }
    }
}
