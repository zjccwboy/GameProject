using H6Game.Hotfix.Messages.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H6Game.Hotfix.Messages.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SubscriberCMDAttribute : Attribute
    {
        public List<int> MessageCmds { get;} = new List<int>();

        public SubscriberCMDAttribute(params int[] commands)
        {
            if(!commands.Any())
            {
                throw new NullReferenceException("消息commands不能为空.");
            }
            this.MessageCmds.AddRange(commands.Select(c=>c));
        }

        public SubscriberCMDAttribute(params InnerMessageCMD[] commands)
        {
            if (!commands.Any())
            {
                throw new NullReferenceException("消息commands不能为空.");
            }
            this.MessageCmds.AddRange(commands.Select(c => (int)c));
        }

        public SubscriberCMDAttribute(params OutNetMessageCMD[] commands)
        {
            if (!commands.Any())
            {
                throw new NullReferenceException("消息commands不能为空.");
            }
            this.MessageCmds.AddRange(commands.Select(c => (int)c));
        }
    }
}
