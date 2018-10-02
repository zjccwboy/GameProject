using H6Game.Base;
using H6Game.Hotfix.Entities.Enums;
using H6Game.Hotfix.Messages.OutNet;

namespace H6Game.Account.Model
{
    public class AccountLogin : ALogin
    {
        public override LoginType LoginType { get { return LoginType.Account; } }
        public override LoginResponeMessage VerifyLogin(LoginRequestMessage request, TAccount account)
        {
            var result = new LoginResponeMessage { LoginResult = LoginResutlCode.UnKnown};
            if (request.Password == account.FPassword)
            {
                result.LoginResult = LoginResutlCode.Success;
                SetRespose(result, account);
            }
            return result;
        }
    }
}
