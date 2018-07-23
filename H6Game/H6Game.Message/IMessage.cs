using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Message
{
    public interface IMessage
    {
        uint MessageCommand { get; set; }
    }
}