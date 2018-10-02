using System;
using System.Collections.Generic;
using H6Game.Base;
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
            //TestDBContext();
            //Game.InitDB();

            //TestRpository();

            Test();


            Console.Read();
        }

        static async void Test()
        {
            Game.Scene.AddComponent<MongoConfig>();

            //MongoClient client = new MongoClient(ConnectionString + DBName + AuthSource);
            //MongoServer server = new MongoServer(MongoServerSettings.FromClientSettings(client.Settings));
            //var yourCollection = server.GetDatabase(DBName).GetCollection<ItemEntity>(CollectionName);

            //var servers = MongoServer.GetAllServers();

            //var server = (MongoConfig.Database.Client as MongoClient).GetServer();
            //var settings = new MongoGridFSSettings();
            ////settings.WriteConcern = database.Settings.WriteConcern;

            //MongoCollection<TAccount> books = server.GetDatabase("H6Game").GetCollection<TAccount>("TAccount");


            ////var query = Query.EQ("_id", "5b97ca7c90d43696d4de4f83");
            //var fields = Fields.Include("FA");

            //var query = Query<TAccount>.EQ(b => b.Id, "5b97ca7c90d43696d4de4f83");

            //foreach (TAccount book in books.FindAs<TAccount>(query).SetFields(fields))
            //{
            //    var b = book;
            //    // do something with book
            //}

            var rpository = Game.Scene.GetComponent<AccountRpository>();
            var account = new TAccount { FAccountName = "SAM" };
            var fs = new string[] { account.BsonElementName(nameof(account.FType)), account.BsonElementName(nameof(account.FSex)) };
            var q = await rpository.DBContext.FindAsAsync(a => a.FAccountName, account.FAccountName,fs);

            // var q = chuncks.Find(query).SetFields(fields).FirstOrDefault();
            //var q = gridFS.Find(query).SetFields(fields);
        }


        static async void TestRpository()
        {
            
            var account = new TAccount
            {
                FAccountName = "Sam",
                FSex = UserSex.Man,
                FCreateTime = DateTime.UtcNow,
                FUpdateTime = DateTime.UtcNow,
                FType = AccountType.Agent,
            };
            //await Game.Scene.GetComponent<AccountRpository>().DBContext.DeleteManyAsync(a => a.FAccountName == "Sam");

            //await Game.Scene.GetComponent<AccountRpository>().AddAsync(account);

            await Game.Scene.GetComponent<AccountRpository>().DBContext.UpdateManyAsync(account, a => a.Id == "5b976935ed0bd7a9d8c87908");
        }

        static async void TestDBContext()
        {
            //var context = new DBContext<TestAccount>(Game.Scene.GetComponent<MongoConfig>().Database);

            var context = Game.Scene.GetComponent<AccountRpository>().DBContext;

            var delResult =  await context.DeleteManyAsync(t => t.FAccountName != null);

            var accountInfo = new TAccount
            {
                FCreateTime = DateTime.Now,
            };
            context.Insert(accountInfo);

            accountInfo = new TAccount
            {
                FCreateTime = DateTime.Now,
            };
            await context.InsertAsync(accountInfo);

            
            var inserts = new List<TAccount>();
            for (var i = 0; i < 10; i++)
            {
                accountInfo = new TAccount
                {
                    FCreateTime = DateTime.Now,
                };
                inserts.Add(accountInfo);
            }
            context.InsertMany(inserts);


            inserts.Clear();
            for (var i = 0; i < 10; i++)
            {
                accountInfo = new TAccount
                {
                    FCreateTime = DateTime.Now,
                };
                inserts.Add(accountInfo);
            }
            await context.InsertManyAsync(inserts);
            

            var findResult = context.Find(t => t.FAccountName == "InsertMany");
            findResult = await context.FindAsync(t => t.FAccountName == "InsertManyAsync");

            var pageResult = context.FindByPage<string>(t => t.FAccountName == "InsertMany", t => t.FAccountName, 1, 20, out int pageCount);
            pageResult = await context.FindByPageAsync<string>(t => t.FAccountName == "InsertManyAsync", t => t.FAccountName, 1, 20, out pageCount);


            context.Update(t => t.FAccountName == "Insert", Builders<TAccount>.Update.Set("FAmt", 102));

            context.Update(new TAccount
            {
            }, t => t.FAccountName == "Insert");


            await context.UpdateAsync(new TAccount
            {
                FCreateTime = DateTime.Now,
            }, t => t.FAccountName == "InsertAsync");


            context.Update(t => t.FAccountName == "InsertMany", Builders<TAccount>.Update.Set("FAmt", 102));

            //var updateAccount = BaseEntity.Create<TestAccount>();
            //var elementName = updateAccount.GetElementName(nameof(updateAccount.FAccount));

            var ecp = Game.Scene.AddComponent<EntityComponent>();
            var  elementName = ecp[typeof(TestAccount), "FAmt"];
            await context.UpdateAsync(t => t.FAccountName == "Update", Builders<TAccount>.Update.Set("FAmt", 103));


            context.UpdateMany(new TAccount
            {
                FCreateTime = DateTime.Now,
            }, t => t.FAccountName == "InsertAsync");

            await context.UpdateManyAsync(new TAccount
            {
                FCreateTime = DateTime.Now,
            }, t => t.FAccountName == "InsertManyAsync");

            context.Delete(t => t.FAccountName == "UpdateMany");

        }



        static async void Insert(IMongoDatabase database)
        {
            var collection = database.GetCollection<TestAccount>("TestAccount");

            var accountInfo = new TestAccount
            {
                FAccount = "Sam",
                FAmt = 100m,
                FCreateTime = DateTime.Now,
                FVIPLevel = 1,
            };




            var tableName = accountInfo.GetType().Name;

            //mongoDbHelper.CreateCollection<TestAccount>(tableName, new[] { "LogDT" });


            Console.WriteLine(accountInfo.ToJson());

            await collection.InsertOneAsync(accountInfo);

            var bson = accountInfo.ToJson();
            var obj = BsonToObject<TestAccount>(bson);

            //通过Document查找
            var doc = accountInfo.ToBsonDocument();
            var list = await collection.Find(doc).ToListAsync();

            //通过_id查找
            //var objectId = new ObjectId(accountInfo.Id);
            //list = await collection.Find(new BsonDocument("_id", objectId)).ToListAsync();

            //var filter = Builders<TestAccount>.Filter.Eq("_id", objectId);
            //var update = Builders<TestAccount>.Update.Set("FVIPLevel", 110);
            //collection.UpdateOne(filter, update);


            foreach (var document in list)
            {
                Console.WriteLine(document.ToJson());
            }
        }


        //public static string ToBson<T>(T data) where T : class
        //{
        //    data.ToBson()

        //    var result = data.ToJson();
        //    return result;

        //    //using (var stream = new MemoryStream())
        //    //{
        //    //    using (var writer = new BsonBinaryWriter(stream))
        //    //    {
        //    //        BsonSerializer.Serialize(writer, typeof(T), data);
        //    //        stream.Seek(0, SeekOrigin.Begin);
        //    //        using (var reader = new BsonBinaryReader(stream))
        //    //        {
        //    //            var context = BsonDeserializationContext.CreateRoot(reader);
        //    //            BsonDocument doc = BsonDocumentSerializer.Instance.Deserialize(context);
        //    //            return doc.ToString();
        //    //        }
        //    //    }
        //    //}
        //}

        public static T BsonToObject<T>(string bson)
        {
            var retsult = BsonSerializer.Deserialize(bson, typeof(T));
            return (T)retsult;
        }
    }

    public class TestAccount : BaseEntity
    {
        [BsonElement("FAcc")]
        public string FAccount { get; set; }
        [BsonElement("FAmt")]
        public decimal FAmt { get; set; }
        [BsonElement("FCT")]
        public DateTime FCreateTime { get; set; }
        [BsonElement("FVL")]
        public byte FVIPLevel { get; set; }
    }
}
