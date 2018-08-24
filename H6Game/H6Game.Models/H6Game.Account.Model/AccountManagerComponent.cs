using H6Game.Account.Model.Component;
using H6Game.Base;

namespace H6Game.Account.Model
{
    [Event(EventType.Awake)]
    [SingletCase]
    public sealed class AccountManagerComponent : BaseComponent
    {
        public AlipayLoginComponent AlipayLogin { get; private set; }
        public WXLoginComponent WXLogin { get; private set; }
        public SMSLoginComponent SMSLogin { get; private set; }

        public override void Awake()
        {
            AlipayLogin = Game.Scene.AddComponent<AlipayLoginComponent>();
            WXLogin = Game.Scene.AddComponent<WXLoginComponent>();
            SMSLogin = Game.Scene.AddComponent<SMSLoginComponent>();
        }
    }
}