using H6Game.Base;
using H6Game.Entitys;
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
            var loginRespone = new LoginResponeMessage();
            var accountRpository = Game.Scene.GetComponent<AccountRpository>();
            TAccount accountInfo = null;
            switch (message.LoginType)
            {
                case Entitys.Enums.LoginType.Default:
                    {
                        accountInfo = await accountRpository.GetByName(message.Account);
                        if (accountInfo == null)
                        {
                            loginRespone.Result = Entitys.Enums.LoginResutlCode.AccountNotExist;
                            break;
                        }

                        if (accountInfo.FPassword != message.Password)
                        {
                            loginRespone.Result = Entitys.Enums.LoginResutlCode.PasswordError;
                            break;
                        }

                        loginRespone.Result = Entitys.Enums.LoginResutlCode.Success;
                        loginRespone.SessionKey = "";
                    }
                    break;
                case Entitys.Enums.LoginType.AliPayLogin:
                    {
                        accountInfo = await accountRpository.GetByName(message.AlipayId);
                        var alipayLogin = Game.Scene.GetComponent<AlipayLoginComponent>();

                        if(accountInfo == null) //新用户
                        {
                            if (!alipayLogin.GetAlipayUserInfo())
                            {
                                loginRespone.Result = Entitys.Enums.LoginResutlCode.AlipayIdError;
                                break;
                            }

                            accountInfo = new TAccount
                            {

                            };
                            await accountRpository.DBContext.InsertAsync(accountInfo);

                            loginRespone.IsNewAccount = true;
                        }
                        loginRespone.Result = Entitys.Enums.LoginResutlCode.Success;
                        loginRespone.SessionKey = "";
                    }
                    break;
                case Entitys.Enums.LoginType.WXLogin:
                    {

                    }
                    break;
                case Entitys.Enums.LoginType.SMSLogin:
                    {

                    }
                    break;
            }

            if(loginRespone.Result == Entitys.Enums.LoginResutlCode.Success)
            {
                loginRespone.AccountId = accountInfo.Id;
                Game.Scene.AddComponent<PlayerComponent>().Add(accountInfo);
            }

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
