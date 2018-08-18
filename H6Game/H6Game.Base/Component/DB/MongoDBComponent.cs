using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace H6Game.Base
{
    [Event(EventType.Awake)]
    [SingletCase]
    public sealed class MongoDBComponent : BaseComponent
    {
        private SysConfig Config { get; set; }
        private MongoClient DBClient { get; set; }
        private IMongoDatabase Database { get; set; }
        private string DatabaseNaeme { get; set; }

        public IMongoDatabase MongoDB { get { return Database; } }

        public override void Awake()
        {
            this.Config = Game.Scene.GetComponent<NetConfigComponent>().ConfigEntity;
            this.DBClient = new MongoClient(Config.DbConfig.ConnectionString);
            this.DatabaseNaeme = this.Config.DbConfig.DatabaseName;
            SetMongoDatabase();
        }

        private void SetMongoDatabase()
        {
            if (Database == null)
            {                
                if (!DatabaseExists(this.DBClient, this.DatabaseNaeme))
                {
                    throw new KeyNotFoundException("此MongoDB名称不存在：" + this.DatabaseNaeme);
                }

                this.Database = this.DBClient.GetDatabase(this.DatabaseNaeme);
            }
        }

        private bool DatabaseExists(MongoClient client, string dbName)
        {
            try
            {
                var dbNames = client.ListDatabases().ToList().Select(db => db.GetValue("name").AsString);
                return dbNames.Contains(dbName);
            }
            catch
            {
                return true;
            }
        }

    }
}
