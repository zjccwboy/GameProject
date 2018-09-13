using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H6Game.Base
{
    [Event(EventType.Awake)]
    [SingletCase]
    public class MongoConfig : BaseComponent
    {
        private DbConfig Config { get; set; }
        public string DatabaseNaeme { get; set; }
        public MongoClient DBClient { get; private set; }
        public MongoServer DBServer  => DBClient.GetServer();
        public IMongoDatabase Database { get; private set; }

        public override void Awake()
        {
            Game.Scene.AddComponent<EntityComponent>();
            Config = Game.Scene.AddComponent<DBConfigComponent>().DBConfig;
            DBClient = new MongoClient(Config.ConnectionString);
            DatabaseNaeme = Config.DatabaseName;
            SetMongoDatabase();
            AddRpositoryComponents();

            Log.Logger.Info("MongoDB初始化成功。");
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
            if (!isSingle)
                throw new ComponentException("规定Rpository类型组件只能定义成单例(SingleCase)组件。");

            var component = ComponentPool.Fetch(type);
            (component as IRpository).SetDBContext(Database);
            Game.Scene.AddComponent(component);
        }

        private void SetMongoDatabase()
        {
            if (Database == null)
            {
                if (!DatabaseExists(DBClient, DatabaseNaeme))
                {
                    throw new KeyNotFoundException("此MongoDB名称不存在：" + DatabaseNaeme);
                }

                Database = DBClient.GetDatabase(DatabaseNaeme);
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
