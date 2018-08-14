using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H6Game.Base;
using MongoDB.Bson;
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
            var collection = database.GetCollection<BsonDocument>("TAccount");
            await collection.InsertOneAsync(new BsonDocument("Name", "Jack"));

            var list = await collection.Find(new BsonDocument("Name", "Jack"))
                .ToListAsync();

            foreach (var document in list)
            {
                Console.WriteLine(document["Name"]);
            }
        }
    }
}
