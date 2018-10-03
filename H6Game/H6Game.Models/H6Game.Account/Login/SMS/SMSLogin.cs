using H6Game.Hotfix.Entities;
using H6Game.Hotfix.Entities.Enums;
using H6Game.Hotfix.Messages.OutNet;
using System;

namespace H6Game.Account
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
