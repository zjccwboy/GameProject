using H6Game.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Account.Model.Login
{
    [SingletCase]
    public class AlipayLoginComponent : BaseComponent
    {
        public bool SendLoginMessage()
        {
            return true;
        }

        public bool GetAlipayUserInfo()
        {
            return true;
        }
    }
}
