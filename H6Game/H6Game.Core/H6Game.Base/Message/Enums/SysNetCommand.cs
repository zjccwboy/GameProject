﻿
namespace H6Game.Base
{
    /// <summary>
    /// 系统保留网络指令类型，该类型范围为1-1000
    /// </summary>
    public enum SysNetCommand
    {
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
    }
}