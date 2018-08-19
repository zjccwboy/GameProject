using System;
using System.Threading;
using H6Game.Base;
using H6Game.Entitys;

namespace TestDistributed
{
    class Program
    {
        static void Main(string[] args)
        {
            var netComponent = Game.Scene.AddComponent<InNetComponent>();
            var actorComponent = Game.Scene.AddComponent<ActorComponent>();
            var dbComponent = Game.Scene.AddComponent<MongoDBComponent>();
            var testComponent = Game.Scene.AddComponent<TestSender>();


            var accountInfo = new TAccount
            {
                FType = AccountType.Default,
                FVIPLevel = VIPLevel.None,
                FWXOpenId = string.Empty,
                FPhoneNumber = "13800138000",
                FPassword = "abcdefg",
                FBalance = 0m,
                FAccumulativeRecharge = 0m,
                FCumulativeConsumption = 0m,
                FCreateTime = DateTime.UtcNow,
                FUpdateTime = DateTime.UtcNow,
            };
            Game.Scene.GetComponent<MongoDBComponent>().GetComponent<AccountRpositoryComponent>().DBContext.Insert(accountInfo);
            Game.Scene.GetComponent<ActorComponent>().AddComponent<PlayerComponent>().LoadAccountInfo(accountInfo.Id);

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
