using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    /// <summary>
    /// 消息分发接口
    /// </summary>
    public interface IMessageDispatcher<Request, Response> : IDispatcher where Request : IRequest where Response : IResponse
    {
        void Receive(Response response);
        void Receive(Packet packet);
    }

    public interface IDispatcher
    {

    }
}
