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
        /// 订阅本地消息，如果来源于本地进程的消息就在该接口中处理。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="command"></param>
        void Subscribe(object message, int command);
    }
}
