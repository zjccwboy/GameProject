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
            Game.Init();
            var testComponent = Game.Scene.AddComponent<TestSender>();

            var accountInfo = new TAccount
            {
                FType = AccountType.Default,
                FVIPLevel = VIPLevel.Default,
                FWXOpenId = string.Empty,
                FPhoneNumber = "13800138000",
                FPassword = "abcdefg",
                FBalance = 0m,
                FAccumulativeRecharge = 0m,
                FCumulativeConsumption = 0m,
                FCreateTime = DateTime.UtcNow,
                FUpdateTime = DateTime.UtcNow,
            };
            Game.Scene.GetComponent<AccountRpositoryComponent>().DBContext.Insert(accountInfo);
            Game.Scene.AddComponent<PlayerComponent>().LoadAccountById(accountInfo.Id);

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
