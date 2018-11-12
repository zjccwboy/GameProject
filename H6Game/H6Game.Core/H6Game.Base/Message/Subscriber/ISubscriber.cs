using System;

namespace H6Game.Base
{

    /// <summary>
    /// 消息分发接口
    /// </summary>
    public interface ISubscriber<Message> : ISubscriber
    {
    }

    public interface ISubscriber
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        Type MessageType { get; }

        /// <summary>
        /// 接收网络消息
        /// </summary>
        /// <param name="network"></param>
        void Receive(Network network);

        /// <summary>
        /// 通知本地订阅消息接口
        /// </summary>
        /// <param name="message"></param>
        /// <param name="command"></param>
        /// <param name="rpcId"></param>
        void Notify(object message, int command, int rpcId);
    }
}
