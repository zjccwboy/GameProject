using H6Game.Message;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public abstract class AMessageHandler
    {
        private ConcurrentDictionary<uint, IDispatcher> dispatcherDictionary = new ConcurrentDictionary<uint, IDispatcher>();

    }
}
