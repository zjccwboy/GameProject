using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Message
{
    [MessageType(MessageType.LogoutResponeMessage)]
    public class LogoutResponseMessage : LogoutMessage
    {

    }

    [MessageType(MessageType.LogoutRequestMessage)]
    public class LogoutRequestMessage : LogoutMessage
    {

    }

    [MessageType(MessageType.Ignore)]
    public class LogoutMessage : IMessage
    {

    }
}
