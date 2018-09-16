

using H6Game.Hotfix.Entities;
using H6Game.Hotfix.Enums;
using H6Game.Hotfix.Messages.OutNet;

namespace H6Game.Account.Model
{
    public class AccountLoginComponent : BaseLoginComponent
    {
        public override LoginResponeMessage VerifyLogin(LoginRequestMessage request, TAccount account)
        {
            var result = new LoginResponeMessage { LoginResult = LoginResutlCode.UnKnown};
            if (request.Password == account.FPassword)
            {
                result.LoginResult = LoginResutlCode.Success;
                SetRespose(result, account);
                return result;
            }

            return result;
        }
    }
}
