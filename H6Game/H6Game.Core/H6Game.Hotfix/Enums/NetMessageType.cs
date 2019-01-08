

namespace H6Game.Hotfix.Enums
{
    /// <summary>
    /// 网络消息类型，1-200为系统保留消息类型，不能在该枚举中定义。
    /// </summary>
    public enum NetMessageType : ushort
    {
        #region Account 201 - 500
        LoginRequestMessage = 201,
        LoginResponeMessage = 202,
        LogoutRequestMessage = 203,
        LogoutResponeMessage = 204,
        AddClientActor = 205,
        #endregion

        #region 测试类型 60000-61000
        TestDistributedTestMessage = 60000,
        TestGServerTestMessage,
        #endregion
    }
}
