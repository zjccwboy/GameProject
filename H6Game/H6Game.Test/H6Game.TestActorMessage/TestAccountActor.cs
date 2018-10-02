using H6Game.Base;
using H6Game.Rpository;
using System;
using System.Threading.Tasks;

namespace H6Game.TestActorMessage
{
    public class TestAccountActor
    {
        public static async void Start()
        {
            if (Game.Scene.GetComponent<DistributionsComponent>().IsCenterServer)
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
            if (!await rpository.AddAsync(account))
            {
                throw new Exception("写账号信息到数据库失败");
            }
            return account;
        }

        public static void AddActor(TAccount account)
        {
            var actor = Game.Scene.AddComponent<PlayerComponent>();
            actor.SetLocal(account);
            Game.Scene.GetComponent<ActorPoolComponent>().AddLocal(actor);
        }
    }
}
