using H6Game.Account.Model.Login;
using H6Game.Base;
using H6Game.Entitys;
using H6Game.Entitys.Enums;
using H6Game.Message;
using H6Game.Rpository;
using System;
using System.Threading.Tasks;

namespace H6Game.Account.Model
{
    [Event(EventType.Awake)]
    public sealed class AccountManager : BaseComponent
    {
        private ILogin AlipayLogin { get; }

        private ILogin WXLogin { get; }

        private ILogin SMSLogin { get; }

        public AccountManager()
        {
            this.AlipayLogin = new AlipayLogin();
            this.WXLogin = new WXLogin();
            this.SMSLogin = new SMSLogin();
        }

        public Task<LoginResponeMessage> Login(LoginRequestMessage loginMessage)
        {
            switch (loginMessage.LoginType)
            {
                case LoginType.SMSLogin:
                    return HandlerSMS(loginMessage);
                case LoginType.AliPayLogin:
                    return HandlerAlipay(loginMessage);
                case LoginType.WXLogin:
                    return HandlerWX(loginMessage);
                case LoginType.Account:
                    return HandlerAccount(loginMessage);
            }

            var tcs = new TaskCompletionSource<LoginResponeMessage>();
            tcs.SetResult(new LoginResponeMessage { LoginResult = LoginResutlCode.UnKnown });
            return tcs.Task;
        }

        private async Task<LoginResponeMessage> HandlerWX(LoginRequestMessage loginMessage)
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var account = await rpository.GetByOpenId(loginMessage.OpenId);
            var result = new LoginResponeMessage();
            
            var loginInfo = new AlipayLoginInfo
            {

            };

            var loginResultRpository = Game.Scene.GetComponent<LoginInfoRpository>();
            if (account == null) //新用户
            {
                if (this.AlipayLogin.StartLogin(loginInfo))
                {
                    result.LoginResult = LoginResutlCode.AlipayIdError;
                    return result;
                }
                account = new TAccount
                {
                    FAlipayUserId = loginMessage.AlipayId,

                };
                await rpository.DBContext.InsertAsync(account);
                result.IsNewAccount = true;
            }

            result.LoginResult = LoginResutlCode.Success;
            loginResultRpository.DBContext.Insert(new TLoginInfo
            {
                FAccountId = account.Id,
                FLoginResult = result.LoginResult,
                FCreateTime = DateTime.UtcNow,
            });

            return result;
        }

        private async Task<LoginResponeMessage> HandlerAlipay(LoginRequestMessage loginMessage)
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var account = await rpository.GetByOpenId(loginMessage.OpenId);
            var result = new LoginResponeMessage();

            var loginInfo = new AlipayLoginInfo
            {

            };

            var loginResultRpository = Game.Scene.GetComponent<LoginInfoRpository>();
            if (account == null) //新用户
            {
                if (this.AlipayLogin.StartLogin(loginInfo))
                {
                    result.LoginResult = LoginResutlCode.AlipayIdError;
                    return result;
                }
                account = new TAccount
                {
                    FAlipayUserId = loginMessage.AlipayId,

                };
                await rpository.DBContext.InsertAsync(account);
                result.IsNewAccount = true;
            }

            result.LoginResult = LoginResutlCode.Success;
            loginResultRpository.DBContext.Insert(new TLoginInfo
            {
                FAccountId = account.Id,
                FLoginResult = result.LoginResult,
                FCreateTime = DateTime.UtcNow,
            });

            return result;
        }

        private async Task<LoginResponeMessage> HandlerSMS(LoginRequestMessage loginMessage)
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var account = await rpository.GetByOpenId(loginMessage.OpenId);
            var result = new LoginResponeMessage();

            var loginInfo = new AlipayLoginInfo
            {

            };

            var loginResultRpository = Game.Scene.GetComponent<LoginInfoRpository>();
            if (account == null) //新用户
            {
                if (this.AlipayLogin.StartLogin(loginInfo))
                {
                    result.LoginResult = LoginResutlCode.AlipayIdError;
                    return result;
                }
                account = new TAccount
                {
                    FAlipayUserId = loginMessage.AlipayId,

                };
                await rpository.DBContext.InsertAsync(account);
                result.IsNewAccount = true;
            }

            result.LoginResult = LoginResutlCode.Success;
            loginResultRpository.DBContext.Insert(new TLoginInfo
            {
                FAccountId = account.Id,
                FLoginResult = result.LoginResult,
                FCreateTime = DateTime.UtcNow,
            });

            return result;
        }

        private async Task<LoginResponeMessage> HandlerAccount(LoginRequestMessage loginMessage)
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var account = await rpository.GetByOpenId(loginMessage.OpenId);
            var result = new LoginResponeMessage();

            var loginInfo = new AlipayLoginInfo
            {

            };

            var loginResultRpository = Game.Scene.GetComponent<LoginInfoRpository>();
            if (account == null) //新用户
            {
                if (this.AlipayLogin.StartLogin(loginInfo))
                {
                    result.LoginResult = LoginResutlCode.AlipayIdError;
                    return result;
                }
                account = new TAccount
                {
                    FAlipayUserId = loginMessage.AlipayId,

                };
                await rpository.DBContext.InsertAsync(account);
                result.IsNewAccount = true;
            }

            result.LoginResult = LoginResutlCode.Success;
            loginResultRpository.DBContext.Insert(new TLoginInfo
            {
                FAccountId = account.Id,
                FLoginResult = result.LoginResult,
                FCreateTime = DateTime.UtcNow,
            });

            return result;
        }
    }
}