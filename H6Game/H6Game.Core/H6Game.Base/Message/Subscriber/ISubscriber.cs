using System;

namespace H6Game.Base.Message
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
    }
}
