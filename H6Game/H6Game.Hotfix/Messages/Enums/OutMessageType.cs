

namespace H6Game.Hotfix.Messages.Enums
{
    public enum OutMessageType
    {
        #region Account 201 - 500
        LoginRequestMessage = 201,
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
