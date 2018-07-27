using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Message.InNetMessage
{
    public class DistributedMessage : IMessage
    {
        public int Port { get; set; }
        public string IP { get; set; }
    }
}
