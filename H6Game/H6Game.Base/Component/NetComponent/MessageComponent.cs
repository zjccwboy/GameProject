using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base.Base.Message
{
    public class MessageComponent : BaseComponent
    {
        private HashSet<Type> messageTypes;

        private MessageComponent()
        {
            messageTypes = ObjectFactory.GetTypes<IMessage>();
        }

    }
}
