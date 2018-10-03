using System;
using System.Collections.Generic;
using System.Linq;

namespace H6Game.Base
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
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

        public SubscriberCMDAttribute(params object[] commands)
        {
            if (!commands.Any())
            {
                throw new NullReferenceException("消息commands不能为空.");
            }
            this.MessageCmds.AddRange(commands.Select(c => (int)c));
        }
    }
}
