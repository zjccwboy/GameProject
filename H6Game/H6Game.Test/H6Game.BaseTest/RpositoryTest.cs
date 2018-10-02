using System.Collections.Generic;
using Xunit;
using H6Game.Base;
using H6Game.Rpository;
using MongoDB.Driver;
using H6Game.Hotfix.Entities;

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
        public async void TestInsert()
        {
            Game.Scene.AddComponent<MongoConfig>();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            Account.SetCreator("AddAsync");
            var success = await rpository.AddAsync(Account);
            Assert.True(success);
        }

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
            await rpository.DBContext.InsertManyAsync(accounts);
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
        public async void TestUpdateAsync()
        {
            Game.Scene.AddComponent<MongoConfig>();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            Account.SetUpdater("UpdateAsync");
            var result = await rpository.DBContext.UpdateAsync(Account, a => a.FAccountName == "SAM");
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
        public async void TestUpdateManyAsync()
        {
            Game.Scene.AddComponent<MongoConfig>();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            Account.SetUpdater("UpdateManyAsync");
            var result = await rpository.DBContext.UpdateManyAsync(Account, a => a.FAccountName == "SAM");
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
        public async void TestUpdateManyAsAsync()
        {
            Game.Scene.AddComponent<MongoConfig>();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            Account.SetUpdater("UpdateManyAsAsync");
            //var updates = new string[] { nameof(Account.FUpdater), nameof(Account.FUpdateTime) };
            var result = await rpository.DBContext.UpdateManyAsAsync(Account, a => a.FAccountName == "SAM", nameof(Account.FUpdater), nameof(Account.FUpdateTime));
            Assert.True(result > 0);
        }

        [Fact]
        public async void TestFind()
        {
            Game.Scene.AddComponent<MongoConfig>();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var q = await rpository.DBContext.FindAsync(a => a.FAccountName == Account.FAccountName);
            Assert.NotNull(q);
        }

        [Fact]
        public async void TestFindMany()
        {
            Game.Scene.AddComponent<MongoConfig>();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var q = await rpository.DBContext.FindAsync(a => a.FAccountName.Contains("SAM"));
            Assert.NotNull(q);
        }

        [Fact]
        public async static void TestFindAs()
        {
            Game.Scene.AddComponent<MongoConfig>();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var account = new TAccount { FAccountName = "SAM" };
            //var fs = new string[] { account.BsonElementName(nameof(account.FType)), account.BsonElementName(nameof(account.FSex)) };
            var q = await rpository.DBContext.FindAsAsync(a => a.FAccountName, account.FAccountName, account.BsonElementName(nameof(account.FType)), account.BsonElementName(nameof(account.FSex)));
            Assert.NotNull(q);
        }

        [Fact]
        public async void TestDelete()
        {
            Game.Scene.AddComponent<MongoConfig>();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var result = await rpository.DBContext.DeleteAsync(a => a.FAccountName == Account.FAccountName);
            Assert.True(result);
        }

        [Fact]
        public async void TestDeleteMany()
        {
            Game.Scene.AddComponent<MongoConfig>();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var result = await rpository.DBContext.DeleteManyAsync(a => a.FAccountName.Contains("SAM") && a.Id == "5b9aaa8a1bb3d785c8be34f9");
            //Assert.True(result);
            Assert.NotNull(result);
        }
    }
}
