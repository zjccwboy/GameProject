using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public interface IActorHandler<Message> : IActorHandler
    {

    }

    public interface IActorHandler : IHandler
    {
        int GetActorId();
    }
}
