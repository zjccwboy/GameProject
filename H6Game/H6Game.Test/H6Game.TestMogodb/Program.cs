using System;
using System.Collections.Generic;
using System.Threading;
using H6Game.Base;
using H6Game.Base.Component;
using H6Game.Base.Config;
using H6Game.Hotfix.Entities;
using H6Game.Hotfix.Entities.Enums;
using H6Game.Rpository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace TestMogodb
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.GetComponent<AccountRpository>();

            DeleteAllAsync();

            TestInsert();
            TestInsertAsync();

            TestInsertMany();
            TestInsertManyAsync();

            TestWhere();
            TestWhereAsync();

            TestFindByPage();

            Update();
            UpdateAsync();

            UpdateMany();
            UpdateManyAsync();
            UpdateManyAsAsync();

            Delete();
            DeleteAsync();

            DeleteMany();
            DeleteManyAsync();

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }

        static void TestInsert()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var account = new TAccount
            {
                FCreateTime = DateTime.Now,
                FAccountName = "Sam",
            };
            rpository.DBContext.Insert(account);
        }

        static async void TestInsertAsync()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var account = new TAccount
            {
                FCreateTime = DateTime.Now,
                FAccountName = "Sam",
            };
            await rpository.DBContext.InsertAsync(account);
        }

        static void TestInsertMany()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var inserts = new List<TAccount>();
            for (var i = 0; i < 10; i++)
            {
                var account = new TAccount
                {
                    FCreateTime = DateTime.Now,
                    FAccountName = $"Sam{i}",
                };
                inserts.Add(account);
            }
            rpository.DBContext.InsertMany(inserts);
        }

        static async void TestInsertManyAsync()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var inserts = new List<TAccount>();
            for (var i = 0; i < 10; i++)
            {
                var account = new TAccount
                {
                    FCreateTime = DateTime.Now,
                    FAccountName = $"SamAsync{i}",
                };
                inserts.Add(account);
            }
            await rpository.DBContext.InsertManyAsync(inserts);
        }

        static void TestWhere()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var result = rpository.DBContext.Where(t => t.FAccountName.Contains("Sam"));
        }

        static async void TestWhereAsync()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var result = await rpository.DBContext.WhereAsync(t => t.FAccountName.Contains("Sam"));
        }

        static void TestFindByPage()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var result = rpository.DBContext.FindByPage<string>(t => t.FAccountName.Contains("Sam"), t => t.FAccountName, 1, 20, out int pageCount);
        }

        static void Update()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var reuslt = rpository.DBContext.Update(t => t.FAccountName.Contains("Sam"), Builders<TAccount>.Update.Set("FAmt", 102));
            reuslt = rpository.DBContext.Update(new TAccount{}, t => t.FAccountName.Contains("Sam"));
        }

        static async void UpdateAsync()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var reuslt = await rpository.DBContext.UpdateAsync(t => t.FAccountName.Contains("Sam"), Builders<TAccount>.Update.Set("FAmt", 102));
            reuslt = await rpository.DBContext.UpdateAsync(new TAccount { }, t => t.FAccountName.Contains("Sam"));
        }

        static void UpdateMany()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var result = rpository.DBContext.UpdateMany(new TAccount{FCreateTime = DateTime.Now,}, t => t.FAccountName.Contains("Sam"));
        }

        static async void UpdateManyAsync()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var result = await rpository.DBContext.UpdateManyAsync(new TAccount { FCreateTime = DateTime.Now, }, t => t.FAccountName.Contains("Sam"));
        }

        static async void UpdateManyAsAsync()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var ecp = Game.Scene.GetComponent<EntityComponent>();
            var updateFileds = new string[]
            {
                nameof(TAccount.FAccountName),
                nameof(TAccount.FAccumulativeRecharge),
                nameof(TAccount.FAlipayHeadImgUrl),
            };
            var result = await rpository.DBContext.UpdateManyAsAsync(new TAccount { FCreateTime = DateTime.Now, }, t => t.FAccountName.Contains("Sam"), updateFileds);
        }

        static void Delete()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var result = rpository.DBContext.Delete(t => t.FAccountName.Contains("Sam"));
        }

        static async void DeleteAsync()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var result =await rpository.DBContext.DeleteAsync(t => t.FAccountName.Contains("Sam"));
        }

        static void DeleteMany()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var result = rpository.DBContext.DeleteMany(t => t.FAccountName.Contains("Sam"));
        }

        static async void DeleteManyAsync()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var result = await rpository.DBContext.DeleteManyAsync(t => t.FAccountName.Contains("Sam"));
        }

        static async void DeleteAllAsync()
        {
            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var result = await rpository.DBContext.DeleteManyAsync(t => t.FAccountName == null);
        }
    }
}
