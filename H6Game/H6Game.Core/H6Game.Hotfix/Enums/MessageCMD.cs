
namespace H6Game.Base
{
    /// <summary>
    /// 消息CMD定义，1开始
    /// </summary>
    public enum MessageCMD
    {
        #region 游戏客户端交互消息指令1-5000
        /// <summary>
        /// 获取一个网关连接地址
        /// </summary>
        GetGateEndPoint = 1,
        /// <summary>
        /// 客户端登陆
        /// </summary>
        ClientLogin,
        /// <summary>
        /// 客户端登出
        /// </summary>
        ClientLogOut,
        /// <summary>
        /// 告诉网关添加一个客户端Actor
        /// </summary>
        AddClientActor,
        #endregion

        #region Actor CMD,5001 - 5100开始
        /// <summary>
        /// 新增Actor消息Cmd
        /// </summary>
        AddActorCmd = 51001,
        /// <summary>
        /// 删除Actor消息Cmd
        /// </summary>
        RemoveActorCmd,
        /// <summary>
        /// 获取Actor信息
        /// </summary>
        SyncActorInfoCmd,
        #endregion
    }
}
