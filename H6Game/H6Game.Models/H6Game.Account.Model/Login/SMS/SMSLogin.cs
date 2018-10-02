using H6Game.Base;
using H6Game.Hotfix.Entities.Enums;
using H6Game.Hotfix.Messages.OutNet;
using System;

namespace H6Game.Account.Model
{
    public class SMSLogin : ALogin
    {
        public override LoginType LoginType { get { return LoginType.SMSLogin; } }
        public override LoginResponeMessage VerifyLogin(LoginRequestMessage request, TAccount account)
        {
            throw new NotImplementedException();
        }
    }
}
