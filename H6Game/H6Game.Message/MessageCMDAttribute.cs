using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Message
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageCMDAttribute : Attribute
    {
        public int MessageCmd { get; private set; }

        public MessageCMDAttribute(MessageCMD command)
        {
            this.MessageCmd = (int)command;
        }
    }
}
