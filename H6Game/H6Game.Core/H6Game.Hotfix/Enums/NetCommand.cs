
namespace H6Game.Base
{
    /// <summary>
    /// 网络消息指令，系统保留指令为1-1000，不能在该枚举中定义。
    /// </summary>
    public enum NetCommand : ushort
    {
        #region 游戏客户端交互消息指令1001开始
        /// <summary>
        /// 客户端登陆
        /// </summary>
        ClientLogin = 1001,
        /// <summary>
        /// 客户端登出
        /// </summary>
        ClientLogOut = 1002,
        #endregion

        #region Actor CMD,2001开始
        /// <summary>
        /// 新增Actor消息Cmd
        /// </summary>
        AddActorCmd = 2001,
        /// <summary>
        /// 删除Actor消息Cmd
        /// </summary>
        RemoveActorCmd = 2002,
        /// <summary>
        /// 获取Actor信息
        /// </summary>
        SyncActorInfoCmd = 2003,
        #endregion
    }
}
