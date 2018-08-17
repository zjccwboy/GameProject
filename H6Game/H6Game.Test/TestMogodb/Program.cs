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
            Game.Scene.AddComponent<MongoDBComponent>();

            var mongoComponent = Game.Scene.GetComponent<MongoDBComponent>();

            var db = mongoComponent.MongoDB;
            Insert(db);

            Console.Read();
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
