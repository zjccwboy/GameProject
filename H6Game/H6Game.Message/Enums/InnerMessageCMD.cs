

namespace H6Game.Message
{
    /// <summary>
    /// 内网消息CMD定义，目前内网的消息在50001开始
    /// </summary>
    public enum InnerMessageCMD
    {
        #region 服务端分布式消息指令50001开始
        /// <summary>
        /// 新增一个监听内网服务连接
        /// </summary>
        AddInServerCmd = 50001,
        /// <summary>
        /// 请求获取外网连接信息
        /// </summary>
        GetOutServerCmd,
        /// <summary>
        /// 请求获取内网连接信息
        /// </summary>
        GetInServerCmd,
        /// <summary>
        /// 新增Actor消息Cmd
        /// </summary>
        AddActorCmd,
        /// <summary>
        /// 删除Actor消息Cmd
        /// </summary>
        RemoveActorCmd,
        /// <summary>
        /// 获取Actor信息
        /// </summary>
        SyncActorInfoCmd,
        #endregion

        #region 测试指令 100000开始
        TestCMD = 100000,
        TestCMD1,
        #endregion
    }
}
