using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H6Game.Base;
using H6Game.Entitys;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace TestMogodb
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<MongoDBComponent>();

            var mongoComponent = Game.Scene.GetComponent<MongoDBComponent>();

            var db = mongoComponent.MongoDB;
            Insert(db);

            Console.Read();
        }

        static async void Insert(IMongoDatabase database)
        {
            //var collection = database.GetCollection<BsonDocument>("TAccount");
            //await collection.InsertOneAsync(new BsonDocument("Name", "Jack"));

            //var list = await collection.Find(new BsonDocument("Name", "Jack"))
            //    .ToListAsync();

            var collection = database.GetCollection<TestAccount>("TestAccount");

            var accountInfo = new TestAccount
            {
                FAccount = "Sam",
                FAmt = 100m,
                FCreateTime = DateTime.Now,
                FVIPLevel = 1,
            };

            var doc = new BsonDocument ( "name", "fuck" );
            var bson = doc.ToString();

            var json = ToJson(accountInfo);
            await collection.InsertOneAsync(accountInfo);

            var list = await collection.Find(new BsonDocument("FAccount", "Sam"))
                .ToListAsync();

            foreach (var document in list)
            {
                Console.WriteLine(document.FAccount);
            }
        }

        public static string ToJson<T>(T data)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BsonBinaryWriter(stream))
                {
                    BsonSerializer.Serialize(writer, typeof(T), data);
                    return writer.ToString();
                }
                //stream.Seek(0, SeekOrigin.Begin);
                //using (var reader = new Newtonsoft.Json.Bson.BsonReader(stream))
                //{
                //    var sb = new StringBuilder();
                //    var sw = new StringWriter(sb);
                //    using (var jWriter = new JsonTextWriter(sw))
                //    {
                //        jWriter.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                //        jWriter.WriteToken(reader);
                //    }
                //    return sb.ToString();
                //}
            }
        }
    }

    public class TestAccount : BaseEntity
    {
        public string FAccount { get; set; }
        public decimal FAmt { get; set; }
        public DateTime FCreateTime { get; set; }
        public byte FVIPLevel { get; set; }
    }
}
