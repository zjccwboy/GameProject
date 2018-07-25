using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    /// <summary>
    /// 消息分发接口
    /// </summary>
    public interface IMessageDispatcher<Response> : IDispatcher where Response : IResponse
    {

    }

    public interface IDispatcher
    {
        void Receive(Session session, ANetChannel channel, Packet packet);
    }
}
