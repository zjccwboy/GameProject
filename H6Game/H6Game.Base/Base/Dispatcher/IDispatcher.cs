using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    /// <summary>
    /// 消息分发接口
    /// </summary>
    public interface IDispatcher<Response> : IDispatcher
    {

    }

    public interface IDispatcher
    {
        Type ResponseType { get;}
        void Receive(Session session, ANetChannel channel, Packet packet);
    }
}
