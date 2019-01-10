
namespace H6Game.Base.Message
{
    /// <summary>
    /// 系统消息类型，该枚举范围为101-200
    /// </summary>
    public enum SysNetMessageType : ushort
    {
        #region 框架自带类型 101 - 200
        NetEndPointMessage = 101,
        OuterEndPointMessage = 102,
        ActorSyncMessage = 103,
        #endregion
    }
}
