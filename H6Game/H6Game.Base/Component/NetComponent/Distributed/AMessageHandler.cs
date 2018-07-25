using H6Game.Message;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public abstract class AMessageHandler<Request, Response> where Request : IRequest where Response : IResponse
    {

    }
}
