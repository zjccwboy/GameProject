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
        /// 新增一个服务连接
        /// </summary>
        AddOneServer = 100,
        /// <summary>
        /// 更新当前所有服务连接列表
        /// </summary>
        UpdateInNetConnections,
        /// <summary>
        /// 更新当前所有服务外网监听IP端口列表
        /// </summary>
        UpdateOutNetConnections,

        TestCMD = 100000,
    }
}
