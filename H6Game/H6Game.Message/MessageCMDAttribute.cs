using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H6Game.Message
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageCMDAttribute : Attribute
    {
        public List<int> MessageCmds { get;} = new List<int>();

        public MessageCMDAttribute(params int[] commands)
        {
            if(commands == null || !commands.Any())
            {
                throw new NullReferenceException("消息commands不能为空.");
            }
            this.MessageCmds.AddRange(commands.Select(c=>c));
        }
    }
}
