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
        /// 删除一个服务连接
        /// </summary>
        DeleteOneServer,
        /// <summary>
        /// 更新当前所有服务连接列表
        /// </summary>
        UpdateCurrentConnections,


        TestCMD = 100000,
    }
}
