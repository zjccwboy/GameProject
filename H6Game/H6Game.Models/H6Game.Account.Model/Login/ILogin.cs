using H6Game.Hotfix.Entities;
using H6Game.Hotfix.Messages.OutNet;

namespace H6Game.Account.Model
{
    public interface ILogin
    {
        LoginResponeMessage VerifyLogin(LoginRequestMessage request, TAccount account);
        void SetRespose(LoginResponeMessage response, TAccount account);
    }
}
