using H6Game.Message;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public abstract class AMessageHandler<Message> where Message : IMessage
    {
        private ConcurrentDictionary<uint, MessageCMDAttribute> cmdDictionary = new ConcurrentDictionary<uint, MessageCMDAttribute>();


    }
}
