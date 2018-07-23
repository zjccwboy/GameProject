using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Message
{
    public class MessageCMDAttribute : Attribute
    {
        public uint MessageCmd { get; private set; }

        public MessageCMDAttribute(uint command)
        {
            this.MessageCmd = command;
        }
    }
}
