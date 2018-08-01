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
        #region 服务端分布式消息指令 100开始
        /// <summary>
        /// 新增一个监听内网服务连接
        /// </summary>
        AddInServer = 100,
        /// <summary>
        /// 请求获取外网连接信息
        /// </summary>
        GetOutServer,
        #endregion

        #region 游戏客户端交互消息指令 200
        /// <summary>
        /// 获取一个网关连接地址
        /// </summary>
        GetGateEndPoint,
        /// <summary>
        /// 客户端登陆
        /// </summary>
        ClientLogin = 200,
        /// <summary>
        /// 客户端登出
        /// </summary>
        ClientLogOut,

        #endregion


        TestCMD = 100000,

        TestCMD1,
    }
}
