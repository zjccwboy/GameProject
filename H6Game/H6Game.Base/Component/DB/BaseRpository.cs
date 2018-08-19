
using H6Game.Entitys;
using MongoDB.Driver;
using System.Linq;

namespace H6Game.Base
{
    public abstract class BaseRpository<TDoc> : BaseComponent, IRpository<TDoc> where TDoc : BaseEntity
    {
        public IContext<TDoc> DBContext { get;private set; }

        public IMongoDatabase DataBase { get;private set; }

        public void SetDBContext(IMongoDatabase database)
        {
            this.DataBase = database;
            this.DBContext = new DBContext<TDoc>(this.DataBase);

            CreateCollection();
        }

        private void CreateCollection()
        {
            var name = typeof(TDoc).Name;
            var collections = this.DataBase.ListCollections().ToList();
            foreach(var collection in collections)
            {
                var bsonValue = collection.AsBsonValue;
                var doc = bsonValue.AsBsonDocument;
                var oldName = doc["name"];
                if (oldName == name)
                {
                    return;
                }
            }
            this.DBContext.CreateCollection();
        }

    }
}
