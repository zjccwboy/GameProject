﻿using H6Game.Hotfix.Entities;
using H6Game.Hotfix.Messages.OutNet;
using System;

namespace H6Game.Account.Model
{
    public class WXLoginComponent : BaseLoginComponent
    {
        public override LoginResponeMessage VerifyLogin(LoginRequestMessage request, TAccount account)
        {
            throw new NotImplementedException();
        }
    }
}
