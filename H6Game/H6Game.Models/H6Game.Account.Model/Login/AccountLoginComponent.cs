using H6Game.Base;
using H6Game.Entities;
using H6Game.Entities.Enums;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Text;

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
