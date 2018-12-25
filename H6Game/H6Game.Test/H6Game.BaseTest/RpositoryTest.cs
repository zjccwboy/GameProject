using System.Collections.Generic;
using Xunit;
using H6Game.Base;
using H6Game.Rpository;
using H6Game.Hotfix.Entities;
using H6Game.Hotfix.Entities.Enums;
using System.Linq;
using System.Threading;
using H6Game.Base.Config;

namespace H6Game.BaseTest
{
    public class RpositoryTest
    {
        TAccount Account = new TAccount
        {
            FAccountName = "SAM",
            FType = AccountType.CustomerService,
            FEmail = "zjccwboy@yeah.net",
        };

        [Fact]
        public async void TestInsertMany()
        {
            Game.Scene.AddComponent<MongoConfig>();
            var rpository = Game.Scene.GetComponent<AccountRpository>();

            List<TAccount> accounts = new List<TAccount>();
            for(var i = 0; i < 100; i++)
            {
                var account = new TAccount
                {
                    FAccountName = "SAM" + i,
                    FType = AccountType.CustomerService,
                    FEmail = "zjccwboy@yeah.net",
                };
                account.SetCreator("TestInsertMany");
                accounts.Add(account);
            }
            rpository.DBContext.InsertMany(accounts);
        }

        [Fact]
        public void TestUpdate()
        {
            Game.Scene.AddComponent<MongoConfig>();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            Account.SetUpdater("Update");
            var result = rpository.DBContext.Update(Account, a => a.FAccountName == "SAM");
            Assert.True(result > 0);
        }


        [Fact]
        public void TestUpdateMany()
        {
            Game.Scene.AddComponent<MongoConfig>();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            Account.SetUpdater("UpdateMany");
            var result = rpository.DBContext.UpdateMany(Account, a => a.FAccountName == "SAM");
            Assert.True(result > 0);
        }

        [Fact]
        public void TestUpdateManyAs()
        {
            Game.Scene.AddComponent<MongoConfig>();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            Account.SetUpdater("UpdateManyAs");
            var updates = new string[] { nameof(Account.FUpdater), nameof(Account.FUpdateTime) };
            var result = rpository.DBContext.UpdateManyAs(Account, a => a.FAccountName == "SAM", updates);
            Assert.True(result > 0);
        }

        [Fact]
        public async void TestWhereEques()
        {
            Game.Scene.AddComponent<MongoConfig>();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var q = await rpository.DBContext.WhereAsync(a => a.FAccountName.Contains("Sam"));
            Assert.True(q.Any());
        }

        [Fact]
        public async void TestWhereContains()
        {
            Game.Scene.AddComponent<MongoConfig>();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var q = await rpository.DBContext.WhereAsync(a => a.FAccountName.Contains("Sam"));
            Assert.True(q.Any());
        }
    }
}
