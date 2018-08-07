using H6Game.Message;
using System;

namespace H6Game.Base
{
    /// <summary>
    /// 消息分发接口
    /// </summary>
    public interface IHandler<Message> : IHandler
    {

    }

    public interface IHandler
    {
        Type ResponseType { get; }
        void Receive(Session session, ANetChannel channel);
    }
}
