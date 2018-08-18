using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using H6Game.Base;
using H6Game.Entitys;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace TestMogodb
{
    class Program
    {
        static void Main(string[] args)
        {

            TestDBContext();


            Console.Read();
        }

        static async void TestDBContext()
        {
           var mongoComponent = Game.Scene.AddComponent<MongoDBComponent>();
            var context = new DBContext(mongoComponent.MongoDB);

            await context.DeleteManyAsync<TestAccount>(t => t.FAccount != null);

            var accountInfo = new TestAccount
            {
                FAccount = "Insert",
                FAmt = 100m,
                FCreateTime = DateTime.Now,
                FVIPLevel = 1,
            };
            context.Insert(accountInfo);

            accountInfo = new TestAccount
            {
                FAccount = "InsertAsync",
                FAmt = 100m,
                FCreateTime = DateTime.Now,
                FVIPLevel = 2,
            };
            await context.InsertAsync(accountInfo);

            
            var inserts = new List<TestAccount>();
            for (var i = 0; i < 10; i++)
            {
                accountInfo = new TestAccount
                {
                    FAccount = "InsertMany",
                    FAmt = 100m,
                    FCreateTime = DateTime.Now,
                    FVIPLevel = 1,
                };
                inserts.Add(accountInfo);
            }
            context.InsertMany(inserts);


            inserts.Clear();
            for (var i = 0; i < 10; i++)
            {
                accountInfo = new TestAccount
                {
                    FAccount = "InsertManyAsync",
                    FAmt = 100m,
                    FCreateTime = DateTime.Now,
                    FVIPLevel = 2,
                };
                inserts.Add(accountInfo);
            }
            await context.InsertManyAsync(inserts);
            

            var findResult = context.Find<TestAccount>(t => t.FAccount == "InsertMany");
            findResult = await context.FindAsync<TestAccount>(t => t.FAccount == "InsertManyAsync");

            var pageResult = context.FindByPage<TestAccount, string>(t => t.FAccount == "InsertMany", t => t.FAccount, 1, 20, out int pageCount);
            pageResult = await context.FindByPageAsync<TestAccount, string>(t => t.FAccount == "InsertManyAsync", t => t.FAccount, 1, 20, out pageCount);


            context.Update(new TestAccount
            {
                FAccount = "Update",
                FAmt = 100m,
                FCreateTime = DateTime.Now,
                FVIPLevel = 2,
            }, t => t.FAccount == "Insert");


            await context.UpdateAsync(new TestAccount 
            {
                FAccount = "UpdateAsync",
                FAmt = 100m,
                FCreateTime = DateTime.Now,
                FVIPLevel = 2,
            }, t => t.FAccount == "InsertAsync");


            context.Update(new TestAccount
            {
                FAccount = "Update1",
                FAmt = 100m,
                FCreateTime = DateTime.Now,
                FVIPLevel = 2,
            }, t => t.FAccount == "InsertMany", Builders<TestAccount>.Update.Set("FAmt", 102));

            await context.UpdateAsync(new TestAccount
            {
                FAccount = "UpdateAsync1",
                FAmt = 100m,
                FCreateTime = DateTime.Now,
                FVIPLevel = 2,
            }, t => t.FAccount == "InsertManyAsync", Builders<TestAccount>.Update.Set("FAmt", 103));


            context.UpdateMany(new TestAccount
            {
                FAccount = "UpdateMany",
                FAmt = 100m,
                FCreateTime = DateTime.Now,
                FVIPLevel = 2,
            }, t => t.FAccount == "InsertAsync");

            await context.UpdateManyAsync(new TestAccount
            {
                FAccount = "UpdateManyAsync",
                FAmt = 100m,
                FCreateTime = DateTime.Now,
                FVIPLevel = 2,
            }, t => t.FAccount == "InsertManyAsync");

            context.Delete<TestAccount>(t => t.FAccount == "UpdateMany");

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


            var config = Game.Scene.GetComponent<NetConfigComponent>();
            var mongoDbHelper = new MongoDbCsharpHelper(config.ConfigEntity.DbConfig.ConnectionString, "H6Game");

            var tableName = accountInfo.GetType().Name;

            //mongoDbHelper.CreateCollection<TestAccount>(tableName, new[] { "LogDT" });

            var listFromLevel = mongoDbHelper.Find<TestAccount>(tableName, t => t.FVIPLevel == 1);

            int rsCount = 0;
            var pages = mongoDbHelper.FindByPage<TestAccount, byte>(tableName, t => t.FVIPLevel == 1, t => t.FVIPLevel, 1, 20, out rsCount);

            mongoDbHelper.Insert<TestAccount>(tableName, new TestAccount { FCreateTime = DateTime.Now, FVIPLevel = 1, FAccount = "测试消息" });

            mongoDbHelper.Update<TestAccount>(tableName, new TestAccount { FCreateTime = DateTime.Now, FVIPLevel = 1, FAccount = "测试消息2" }, t => t.FCreateTime == new DateTime(1900, 1, 1));

            mongoDbHelper.Delete<TestAccount>(t => t.FVIPLevel == 1);

            mongoDbHelper.ClearCollection<TestAccount>(tableName);


            Console.WriteLine(accountInfo.ToJson());

            await collection.InsertOneAsync(accountInfo);

            var bson = accountInfo.ToJson();
            var obj = BsonToObject<TestAccount>(bson);

            //通过Document查找
            var doc = accountInfo.ToBsonDocument();
            var list = await collection.Find(doc).ToListAsync();

            //通过_id查找
            var objectId = new ObjectId(accountInfo.Id);
            list = await collection.Find(new BsonDocument("_id", objectId)).ToListAsync();

            var filter = Builders<TestAccount>.Filter.Eq("_id", objectId);
            var update = Builders<TestAccount>.Update.Set("FVIPLevel", 110);
            collection.UpdateOne(filter, update);


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
        [BsonElement]
        public string FAccount { get; set; }
        [BsonElement]
        public decimal FAmt { get; set; }
        [BsonElement]
        public DateTime FCreateTime { get; set; }
        [BsonElement]
        public byte FVIPLevel { get; set; }
    }
}
