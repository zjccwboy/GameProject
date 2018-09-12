using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using H6Game.Base;
using H6Game.Rpository;
using H6Game.Entities;
using H6Game.Entities.Enums;
using MongoDB.Driver;

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
            Game.InitDB();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            Account.SetCreator("AddAsync");
            var success = await rpository.AddAsync(Account);
            Assert.True(success);
        }

        [Fact]
        public void TestUpdate()
        {
            Game.InitDB();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            Account.SetUpdater("Update");
            var result = rpository.DBContext.Update(Account, a => a.FAccountName == "SAM");
            Assert.True(result > 0);
        }

        [Fact]
        public async void TestUpdateAsync()
        {
            Game.InitDB();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            Account.SetUpdater("UpdateAsync");
            var result = await rpository.DBContext.UpdateAsync(Account, a => a.FAccountName == "SAM");
            Assert.True(result > 0);
        }

        [Fact]
        public void TestUpdateMany()
        {
            Game.InitDB();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            Account.SetUpdater("UpdateMany");
            var result = rpository.DBContext.UpdateMany(Account, a => a.FAccountName == "SAM");
            Assert.True(result > 0);
        }

        [Fact]
        public async void TestUpdateManyAsync()
        {
            Game.InitDB();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            Account.SetUpdater("UpdateManyAsync");
            var result = await rpository.DBContext.UpdateManyAsync(Account, a => a.FAccountName == "SAM");
            Assert.True(result > 0);
        }

        [Fact]
        public void TestUpdateManyAs()
        {
            Game.InitDB();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            Account.SetUpdater("UpdateManyAs");
            var updates = new string[] { nameof(Account.FUpdater), nameof(Account.FUpdateTime) };
            var result = rpository.DBContext.UpdateManyAs(Account, a => a.FAccountName == "SAM", updates);
            Assert.True(result > 0);
        }

        [Fact]
        public async void TestUpdateManyAsAsync()
        {
            Game.InitDB();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            Account.SetUpdater("UpdateManyAsAsync");
            var updates = new string[] { nameof(Account.FUpdater), nameof(Account.FUpdateTime)};
            var result = await rpository.DBContext.UpdateManyAsAsync(Account, a => a.FAccountName == "SAM", updates);
            Assert.True(result > 0);
        }

        [Fact]
        public async void TestFind()
        {
            Game.InitDB();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var q = await rpository.DBContext.FindAsync(a => a.FAccountName == Account.FAccountName);
            Assert.NotNull(q);
        }

        [Fact]
        public async static void TestFindAs()
        {
            Game.InitDB();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var account = new TAccount { FAccountName = "SAM" };
            var fs = new string[] { account.BsonElementName(nameof(account.FType)), account.BsonElementName(nameof(account.FSex)) };
            var q = await rpository.DBContext.FindAsAsync(a => a.FAccountName, account.FAccountName, fs);
            Assert.NotNull(q);
        }

        [Fact]
        public async void TestDelete()
        {
            Game.InitDB();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var result = await rpository.DBContext.DeleteAsync(a => a.FAccountName == Account.FAccountName);
            Assert.True(result);
        }
    }
}
