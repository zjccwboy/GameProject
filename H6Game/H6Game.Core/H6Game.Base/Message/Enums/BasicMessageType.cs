﻿
namespace H6Game.Base.Message
{
    /// <summary>
    /// 网络消息基础值类型，该枚举范围为1-100
    /// </summary>
    public enum BasicMessageType : ushort
    {
        Ignore,

        #region 值类型 1-100

        String,
        Int,
        Uint,
        Long,
        ULong,
        Short,
        UShort,
        Float,
        Double,
        Decimal,
        Byte,
        Sbyte,
        Char,
        BooLean,
        DateTime,
        Guid,
        Enum,

        #endregion
    }
}
