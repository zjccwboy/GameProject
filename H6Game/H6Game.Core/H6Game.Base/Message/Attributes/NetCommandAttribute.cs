using System;
using System.Collections.Generic;
using System.Linq;

namespace H6Game.Base.Message
{
    /// <summary>
    /// 标识网络消息命令特性器。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class NetCommandAttribute : Attribute
    {
        public List<ushort> MessageCmds { get;} = new List<ushort>();

        public NetCommandAttribute(params ushort[] commands)
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
            this.MessageCmds.AddRange(commands.Select(c => (ushort)c));
        }
    }
}
