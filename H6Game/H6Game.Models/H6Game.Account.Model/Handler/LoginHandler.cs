using H6Game.Base;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Account.Model.Handler
{
    [HandlerCMD(MessageCMD.ClientLogin)]
    public class LoginHandler : AHandler<LoginRequestMessage>
    {
        protected override void Handler(Network network, LoginRequestMessage message)
        {

        }
    }

    [HandlerCMD(MessageCMD.ClientLogOut)]
    public class LogouHandler : AHandler<LoginRequestMessage>
    {
        protected override void Handler(Network network, LoginRequestMessage message)
        {

        }
    }
}
