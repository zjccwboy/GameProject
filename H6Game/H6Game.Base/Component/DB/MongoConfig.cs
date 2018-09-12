using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H6Game.Base
{
    public static class MongoConfig
    {
        private static DbConfigEntity Config { get; set; }
        public static string DatabaseNaeme { get; set; }
        public static MongoClient DBClient { get;}
        public static MongoServer DBServer  => DBClient.GetServer();
        public static IMongoDatabase Database { get; private set; }

        static MongoConfig()
        {
            Game.Scene.AddComponent<EntityComponent>();
            Config = Game.Scene.AddComponent<DBConfigComponent>().ConfigEntity;
            DBClient = new MongoClient(Config.ConnectionString);
            DatabaseNaeme = Config.DatabaseName;
            SetMongoDatabase();
            AddRpositoryComponents();

            Log.Logger.Info("MongoDB初始化成功.");
        }

        public static void Init()
        {

        }

        private static void AddRpositoryComponents()
        {
            var types = TypePool.GetTypes<IRpository>();
            foreach (var type in types)
            {
                AddComponent(type);
            }
        }

        private static void AddComponent(Type type)
        {
            if (!typeof(IRpository).IsAssignableFrom(type))
                return;

            var isSingle = ComponentPool.IsSingleType(type);
            var component = ComponentPool.Fetch(type);
            (component as IRpository).SetDBContext(Database);
            Game.Scene.AddComponent(component);
        }

        private static void SetMongoDatabase()
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

        private static bool DatabaseExists(MongoClient client, string dbName)
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
