using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Message
{
    public enum MessageType
    {
        None,

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
        #endregion

        #region 框架自带类型 101 - 200
        NetEndPointMessage = 101,
        ActorMessage,
        SyncActorInfoEntity,
        #endregion

        #region Account 201 - 500
        LoginRequestMessage,
        LoginResponeMessage,
        LogoutRequestMessage,
        LogoutResponeMessage,
        #endregion

        #region 测试类型 10000001-20000000
        TestDistributedTestMessage = 10000001,
        TestGServerTestMessage,
        #endregion
    }
}
