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
        Type MessageType { get; }
        void Receive(Network network);
    }
}
