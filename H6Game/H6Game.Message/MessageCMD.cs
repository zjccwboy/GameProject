using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Message
{
    /// <summary>
    /// 全局消息定义
    /// </summary>
    public enum MessageCMD
    {
        #region 服务端分布式消息指令 1开始
        /// <summary>
        /// 新增一个监听内网服务连接
        /// </summary>
        AddInServerCmd = 1,
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

        #region 游戏客户端交互消息指令 200
        /// <summary>
        /// 获取一个网关连接地址
        /// </summary>
        GetGateEndPoint = 200,
        /// <summary>
        /// 客户端登陆
        /// </summary>
        ClientLogin,
        /// <summary>
        /// 客户端登出
        /// </summary>
        ClientLogOut,

        #endregion

        #region 测试指令 100000开始
        TestCMD = 100000,
        TestCMD1,
        #endregion
    }
}
