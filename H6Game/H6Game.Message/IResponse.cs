using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Message
{
    public interface IResponse
    {
        IMessage Message { get; set; }
    }
}
