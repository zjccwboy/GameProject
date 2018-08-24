using System;
using System.Collections.Generic;
using System.Text;
using H6Game.Account.Model.Component;
using H6Game.Base;

namespace H6Game.Account.Model
{
    public sealed class AccountManager
    {
        public AlipayLoginComponent AlipayLogin { get; }
        public WXLoginComponent WXLogin { get; }
        public SMSLoginComponent SMSLogin { get; }

        private AccountManager()
        {
            AlipayLogin = Game.Scene.AddComponent<AlipayLoginComponent>();
            WXLogin = Game.Scene.AddComponent<WXLoginComponent>();
            SMSLogin = Game.Scene.AddComponent<SMSLoginComponent>();
        }

        public static AccountManager Instance { get; } = new AccountManager();


    }
}
