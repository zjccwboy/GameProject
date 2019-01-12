using H6Game.Actor;
using H6Game.Base;
using H6Game.Base.Component;
using H6Game.Hotfix.Entities;
using H6Game.Rpository;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace H6Game.TestActorMessage
{
    public class TestAccountActor
    {
        public static async void Start()
        {
            if (Game.Scene.GetComponent<NetDistributionsComponent>().IsCenterServer)
                return;

            AddActor(await Add());
        }

        public async static Task<TAccount> Add()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var account = new TAccount
            {
                FAccountName = "Sam" + new Random().Next(100000),
            };
            account.SetCreator("Admin");
            account.SetUpdater("Admin");
            await rpository.DBContext.InsertAsync(account);
            return account;
        }

        public static void AddActor(TAccount account)
        {
            Game.Scene.GetComponent<ActorComponentStorage>().AddActor<PlayerComponent, TAccount>( account);
        }
    }
}
