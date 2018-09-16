using H6Game.Base;
using H6Game.Hotfix.Enums;
using H6Game.Hotfix.Messages.Attributes;
using H6Game.Hotfix.Messages.Enums;
using H6Game.Hotfix.Messages.OutNet;
using H6Game.Rpository;

namespace H6Game.Account.Model
{
    [HandlerCMD(OutNetMessageCMD.ClientLogin)]
    public class LoginHandler : AMessageHandler<LoginRequestMessage>
    {
        protected override void Handler(Network network, LoginRequestMessage message)
        {
            Game.Scene.GetComponent<LoginComponent>().LoginHandler(network, message);
        }
    }

    [SingletCase]
    public class LoginComponent : BaseComponent
    {
        public async void LoginHandler(Network network, LoginRequestMessage message)
        {
            LoginResponeMessage response = null;
            switch (message.LoginType)
            {
                case LoginType.Account:
                    using (var component = Game.Scene.AddComponent<AccountLoginComponent>())
                    {
                        var account = await Game.Scene.GetComponent<AccountRpository>().GetByName(message.Account);
                        response = component.VerifyLogin(message, account);
                        if (response.LoginResult == LoginResutlCode.Success)
                            Game.Scene.AddComponent<PlayerComponent>().AddLocal(account);
                    }
                    break;

                case LoginType.AliPayLogin:
                    using (var component = Game.Scene.AddComponent<AlipayLoginComponent>())
                    {

                    }
                    break;

                case LoginType.SMSLogin:
                    using (var component = Game.Scene.AddComponent<SMSLoginComponent>())
                    {

                    }
                    break;

                case LoginType.WXLogin:
                    using (var component = Game.Scene.AddComponent<WXLoginComponent>())
                    {

                    }
                    break;
                default:
                    response = new LoginResponeMessage() { LoginResult = LoginResutlCode.LoginTypeError };
                    break;
            }

            network.RpcCallBack(response);
        }
    }
}
