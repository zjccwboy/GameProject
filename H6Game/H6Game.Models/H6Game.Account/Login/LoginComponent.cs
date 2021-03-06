﻿using H6Game.Actor;
using H6Game.Base;
using H6Game.Base.Component;
using H6Game.Base.Message;
using H6Game.Hotfix.Entities;
using H6Game.Hotfix.Entities.Enums;
using H6Game.Hotfix.Messages.OutNet;
using H6Game.Rpository;

namespace H6Game.Account
{
    [NetCommand(MSGCommand.ClientLogin)]
    public class LoginSubscriber : NetSubscriber<LoginRequestMessage>
    {

        protected override void Subscribe(Network network, LoginRequestMessage message, ushort messageCmd)
        {
            Game.Scene.GetComponent<LoginComponent>().OnLogin(network, message);
        }
    }

    [ComponentEvent(EventType.Awake)]
    [SingleCase]
    public class LoginComponent : BaseComponent
    {
        public override void Awake()
        {
            LoginFactory.Load();
        }

        public async void OnLogin(Network network, LoginRequestMessage message)
        {
            LoginResponeMessage response = null;
            switch (message.LoginType)
            {
                case LoginType.Account:
                    {
                        var login = LoginFactory.Create(LoginType.Account);
                        var account = await Game.Scene.GetComponent<AccountRpository>().GetByName(message.Account);
                        response = login.VerifyLogin(message, account);
                        if (response.LoginResult == LoginResutlCode.Success)
                        {
                            Game.Scene.GetComponent<ActorComponentStorage>().AddActor<PlayerComponent, TAccount>(account);
                        }
                    }
                    break;
                case LoginType.AliPayLogin:

                    break;

                case LoginType.SMSLogin:

                    break;

                case LoginType.WXLogin:

                    break;
                default:
                    response = new LoginResponeMessage() { LoginResult = LoginResutlCode.LoginTypeError };
                    break;
            }

            network.Response(response);
        }
    }
}
