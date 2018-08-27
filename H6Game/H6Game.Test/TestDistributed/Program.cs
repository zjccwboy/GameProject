using System;
using System.Threading;
using H6Game.Base;
using H6Game.Entitys;
using H6Game.Rpository;

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
            Game.Scene.GetComponent<AccountRpository>().DBContext.Insert(accountInfo);
            Game.Scene.AddComponent<PlayerComponent>().Add(accountInfo);

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
