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

    public class LogoutMessage : IMessage
    {

    }
}
