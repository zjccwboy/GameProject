
namespace H6Game.Base
{
    /// <summary>
    /// 服务端分布式消息50001开始
    /// </summary>
    public enum DistributionCMD
    {
        /// <summary>
        /// 新增一个监听内网服务连接
        /// </summary>
        AddInServerCmd = 50001,
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
