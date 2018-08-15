using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace H6Game.Base
{
    [Event(EventType.Awake)]
    public sealed class MongoDBComponent : BaseComponent
    {
        private SysConfig Config { get; set; }
        private MongoClient DBClient { get; set; }
        private IMongoDatabase Database { get; set; }

        public IMongoDatabase MongoDB { get { return Database; } }

        public override void Awake()
        {
            this.Config = Game.Scene.GetComponent<NetConfigComponent>().ConfigEntity;
            this.DBClient = new MongoClient(Config.DbConfig.ConnectionString);
            this.Database = this.DBClient.GetDatabase("H6Game");
        }

        public void Insert<T>(T data)
        {

        }

        public void Modfiy<T>(T data)
        {

        }

        public void Delete<T>(T data)
        {

        }
    }
}
