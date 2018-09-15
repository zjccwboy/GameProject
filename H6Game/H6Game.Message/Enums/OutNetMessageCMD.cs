﻿

namespace H6Game.Message
{
    /// <summary>
    /// 外网消息CMD定义，外网消息定义在1-50001之间
    /// </summary>
    public enum OutNetMessageCMD
    {
        #region 游戏客户端交互消息指令50001开始
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

        #endregion
    }
}
