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
            Account.SetCreator("Admin");
            await rpository.AddAsync(Account);
        }

        [Fact]
        public async void TestUpdate()
        {
            Game.InitDB();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            Account.SetUpdater("Admin");
            var updates = new string[] { nameof(Account.FUpdater), nameof(Account.FUpdateTime)};
            await rpository.DBContext.UpdateManyAsAsync(Account, a => a.FAccountName == "SAM", updates);
        }

        [Fact]
        public async void TestFind()
        {
            Game.InitDB();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var result = await rpository.DBContext.FindAsync(a => a.FAccountName == Account.FAccountName);
        }

        [Fact]
        public async static void TestFindAs()
        {
            Game.InitDB();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var account = new TAccount { FAccountName = "SAM" };
            var fs = new string[] { account.BsonElementName(nameof(account.FType)), account.BsonElementName(nameof(account.FSex)) };
            var q = await rpository.DBContext.FindAsAsync(a => a.FAccountName, account.FAccountName, fs);
        }

        [Fact]
        public async void TestDelete()
        {
            Game.InitDB();
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            await rpository.DBContext.DeleteAsync(a => a.FAccountName == Account.FAccountName);
        }
    }
}
