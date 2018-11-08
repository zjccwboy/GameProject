using System;
using System.Collections.Generic;
using System.Linq;

namespace H6Game.Base
{
    /// <summary>
    /// 标识网络消息命令特性器。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class NetCommandAttribute : Attribute
    {
        public List<int> MessageCmds { get;} = new List<int>();

        public NetCommandAttribute(params int[] commands)
        {
            if(!commands.Any())
            {
                throw new NullReferenceException("消息commands不能为空.");
            }
            this.MessageCmds.AddRange(commands.Select(c=>c));
        }

        public NetCommandAttribute(params object[] commands)
        {
            if (!commands.Any())
            {
                throw new NullReferenceException("消息commands不能为空.");
            }
            this.MessageCmds.AddRange(commands.Select(c => (int)c));
        }
    }
}
