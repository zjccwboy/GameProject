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


        TestCMD = 100000,

        TestCMD1,
    }
}
