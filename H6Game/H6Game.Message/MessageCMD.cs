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
        /// 删除一个监听内网服务连接
        /// </summary>
        DeleteServer,
        /// <summary>
        /// 更新当前所有服务内网监听IP端口列表
        /// </summary>
        UpdateInNetConnections,
        /// <summary>
        /// 新增一个监听外网服务连接
        /// </summary>
        AddOutServer,
        /// <summary>
        /// 删除一个监听外网服务连接
        /// </summary>
        DeleteOutServer,
        /// <summary>
        /// 更新当前所有服务外网监听IP端口列表
        /// </summary>
        UpdateOutNetConnections,
        #endregion

        #region 游戏客户端交互消息指令 200
        /// <summary>
        /// 客户端登陆
        /// </summary>
        ClientLogin = 200,
        /// <summary>
        /// 客户端登出
        /// </summary>
        ClientLogOut,
        /// <summary>
        /// 获取一个网关连接地址
        /// </summary>
        GetGateEndPoint,
        /// <summary>
        /// 发送一个网关地址给客户端
        /// </summary>
        SendGateEndPoint,

        #endregion


        TestCMD = 100000,

        TestCMD1,
    }
}
