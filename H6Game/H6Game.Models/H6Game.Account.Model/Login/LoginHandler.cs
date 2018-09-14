﻿using H6Game.Base;
using H6Game.Entities.Enums;
using H6Game.Message;

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
            using (var playerComponent = Game.Scene.AddComponent<PlayerHandlerComponent>())
            {
                playerComponent.Remove(playerEnity.Id);
            }
        }
    }
}
