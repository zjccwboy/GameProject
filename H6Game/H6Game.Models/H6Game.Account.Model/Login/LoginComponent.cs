using H6Game.Base;
using H6Game.Hotfix.Enums;
using H6Game.Hotfix.Messages.Attributes;
using H6Game.Hotfix.Messages.Enums;
using H6Game.Hotfix.Messages.OutNet;
using H6Game.Rpository;
using System.Collections.Generic;

namespace H6Game.Account.Model
{
    [SubscriberCMD(OutNetMessageCMD.ClientLogin)]
    public class LoginSubscriber : AMsgSubscriber<LoginRequestMessage>
    {
        protected override void Subscribe(Network network, LoginRequestMessage message)
        {
            Game.Scene.GetComponent<LoginComponent>().OnLogin(network, message);
        }
    }

    [Event(EventType.Awake)]
    [SingletCase]
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
                            Game.Scene.AddComponent<PlayerComponent>().AddLocal(account);
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
