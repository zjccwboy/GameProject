using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Message
{
    /// <summary>
    /// 消息分发接口
    /// </summary>
    public interface IMessageDispatcher<Request, Response> : IDispatcher where Request : IRequest where Response : IResponse
    {

    }

    public interface IDispatcher
    {

    }
}
