using H6Game.Message;

namespace H6Game.Base
{
    public interface IMessageHandler
    {
        /// <summary>
        /// 通讯管道对象
        /// </summary>
        ANetChannel Channel { get; set; }

        /// <summary>
        /// 网络服务对象
        /// </summary>
        ANetService NetService { get; set; }

        /// <summary>
        /// 网络会话对象
        /// </summary>
        Session Session { get; set; }

        /// <summary>
        /// 接收消息处理回调接口，除了RPC以外的所有消息都会被回调到该接口中
        /// </summary>
        /// <param name="packet"></param>
        void DoReceive(Packet packet);
    }
}
