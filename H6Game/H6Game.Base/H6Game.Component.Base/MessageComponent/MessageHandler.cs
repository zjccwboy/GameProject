using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Component.Base.MessageComponent
{
    public class MessageHandler : IMessageHandler
    {
        public ANetChannel Channel { get; set; }
        public ANetService NetService { get; set; }

        public void DoReceive(Packet packet)
        {

        }
    }
}
