using H6Game.Base;
using H6Game.Entitys;
using H6Game.Entitys.Enums;
using H6Game.Message;
using H6Game.Rpository;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Account.Model.Login
{
    [HandlerCMD(MessageCMD.ClientLogin)]
    public class LoginHandler : AHandler<LoginRequestMessage>
    {
        protected async override void Handler(Network network, LoginRequestMessage message)
        {
            var accountManager = Game.Scene.GetComponent<AccountManager>();
            var loginRespone = await accountManager.Login(message);
            network.RpcCallBack(loginRespone);
        }
    }

    [HandlerCMD(MessageCMD.ClientLogOut)]
    public class LogouHandler : AHandler<LogoutRequestMessage>
    {
        protected override void Handler(Network network, LogoutRequestMessage message)
        {

            Game.Actor.RemoveActor(ActorType.Player, message.AccountId);

            var playerEnity = Game.Actor.GetLocalActor(ActorType.Player, message.AccountId);
            var playerComponent = Game.Scene.GetComponent<PlayerComponent>(playerEnity.ActorId);
            Game.Scene.Remove(playerComponent);
        }
    }
}
