using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Message
{
    /// <summary>
    /// 全局消息定义
    /// </summary>
    public class MessageCMD
    {
        #region 分布式服务指令
        /// <summary>
        /// 新增一个服务连接
        /// </summary>
        public const uint AddOneServer = 1;
        /// <summary>
        /// 删除一个服务连接
        /// </summary>
        public const uint DeleteOneServer = 2;
        /// <summary>
        /// 更新当前所有服务连接列表
        /// </summary>
        public const uint UpdateCurrentConnections = 3;
        #endregion



    }
}
