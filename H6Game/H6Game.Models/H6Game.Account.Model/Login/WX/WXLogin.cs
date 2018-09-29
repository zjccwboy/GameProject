﻿using H6Game.Hotfix.Entities;
using H6Game.Hotfix.Enums;
using H6Game.Hotfix.Messages.OutNet;
using System;

namespace H6Game.Account.Model
{
    public class WXLogin : ALogin
    {
        public override LoginType LoginType { get { return LoginType.WXLogin; } }
        public override LoginResponeMessage VerifyLogin(LoginRequestMessage request, TAccount account)
        {
            throw new NotImplementedException();
        }
    }
}
