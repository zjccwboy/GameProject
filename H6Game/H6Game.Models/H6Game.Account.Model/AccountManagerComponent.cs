using H6Game.Account.Model.Login;
using H6Game.Base;

namespace H6Game.Account.Model
{
    [Event(EventType.Awake)]
    [SingletCase]
    public sealed class AccountManagerComponent : BaseComponent
    {
        public override void Awake()
        {
            Game.Scene.AddComponent<AlipayLoginComponent>();
            Game.Scene.AddComponent<WXLoginComponent>();
            Game.Scene.AddComponent<SMSLoginComponent>();
        }
    }
}