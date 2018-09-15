using H6Game.Base;
using H6Game.Entities;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Account.Model
{
    public class AlipayLoginComponent : BaseLoginComponent
    {
        public override LoginResponeMessage VerifyLogin(LoginRequestMessage request, TAccount account)
        {
            throw new NotImplementedException();
        }
    }
}
