

namespace H6Game.Hotfix.Enums
{
    public enum MessageType
    {
        #region Account 201 - 500
        LoginRequestMessage = 201,
        LoginResponeMessage = 202,
        LogoutRequestMessage = 203,
        LogoutResponeMessage = 204,
        AddClientActor = 205,
        #endregion

        #region 测试类型 10000001-20000000
        TestDistributedTestMessage = 10000001,
        TestGServerTestMessage,
        #endregion
    }
}
