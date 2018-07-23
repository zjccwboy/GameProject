using H6Game.Message;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public abstract class AMessageHandler<MessageRq, MessageRp> where MessageRq : IRequest where MessageRp : IResponse
    {
        private ConcurrentDictionary<uint, AMessageDispatcher> dispatcherDictionary = new ConcurrentDictionary<uint, AMessageDispatcher>();

        public void HandleMesage(MessageRp message)
        {
            if(dispatcherDictionary.TryGetValue(message.MessageCommand, out AMessageDispatcher dispatcher))
            {

            }
        }
    }
}
