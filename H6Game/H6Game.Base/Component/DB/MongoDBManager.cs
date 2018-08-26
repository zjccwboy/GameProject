using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H6Game.Base
{
    public sealed class MongoDBManager
    {
        private SysConfig Config { get; set; }
        private MongoClient DBClient { get; set; }
        private string DatabaseNaeme { get; set; }
        public IMongoDatabase Database { get; private set; }

        public static MongoDBManager Instance { get; } = new MongoDBManager();

        public void Init()
        {
            this.Config = Game.Scene.GetComponent<NetConfigComponent>().ConfigEntity;
            this.DBClient = new MongoClient(Config.DbConfig.ConnectionString);
            this.DatabaseNaeme = this.Config.DbConfig.DatabaseName;
            SetMongoDatabase();
            AddRpositoryComponents();
        }

        private void AddRpositoryComponents()
        {
            var types = TypePool.GetTypes<IRpository>();
            foreach (var type in types)
            {
                AddComponent(type);
            }
        }

        private void AddComponent(Type type)
        {
            if (!typeof(IRpository).IsAssignableFrom(type))
                return;

            var isSingle = ComponentPool.IsSingleType(type);
            var component = ComponentPool.Fetch(type);
            (component as IRpository).SetDBContext(this.Database);
            Game.Scene.AddComponent(component);
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
