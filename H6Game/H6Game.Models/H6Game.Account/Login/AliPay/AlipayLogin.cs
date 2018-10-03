using H6Game.Hotfix.Entities;
using H6Game.Hotfix.Entities.Enums;
using H6Game.Hotfix.Messages.OutNet;
using System;

namespace H6Game.Account
{
    public class AliPayLogin : ALogin
    {
        public override LoginType LoginType { get { return LoginType.AliPayLogin; } }
        public override LoginResponeMessage VerifyLogin(LoginRequestMessage request, TAccount account)
        {
            throw new NotImplementedException();
        }
    }
}
