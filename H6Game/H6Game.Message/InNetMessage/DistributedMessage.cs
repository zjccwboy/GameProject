using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Message.InNetMessage
{
    public class DistributedMessageRq : IRequest
    {
        public uint MessageCommand { get; set; }
        public int Port { get; set; }
        public string IP { get; set; }
    }

    public class DistributedMessageRp : IResponse
    {
        public uint MessageCommand { get; set; }
        public int Port { get; set; }
        public string IP { get; set; }
    }
}
