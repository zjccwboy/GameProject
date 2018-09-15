using System;
using System.Collections.Generic;
using System.Linq;

namespace H6Game.Message
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HandlerCMDAttribute : Attribute
    {
        public List<int> MessageCmds { get;} = new List<int>();

        public HandlerCMDAttribute(params int[] commands)
        {
            if(!commands.Any())
            {
                throw new NullReferenceException("消息commands不能为空.");
            }
            this.MessageCmds.AddRange(commands.Select(c=>c));
        }

        public HandlerCMDAttribute(params InnerMessageCMD[] commands)
        {
            if (!commands.Any())
            {
                throw new NullReferenceException("消息commands不能为空.");
            }
            this.MessageCmds.AddRange(commands.Select(c => (int)c));
        }

        public HandlerCMDAttribute(params OutNetMessageCMD[] commands)
        {
            if (!commands.Any())
            {
                throw new NullReferenceException("消息commands不能为空.");
            }
            this.MessageCmds.AddRange(commands.Select(c => (int)c));
        }
    }
}
