using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace H6Game.Base
{
    [Event(EventType.Awake)]
    [SingletCase]
    public sealed class MongoDBComponent : BaseComponent
    {
        private SysConfig Config { get; set; }
        private MongoClient DBClient { get; set; }
        private string DatabaseNaeme { get; set; }
        public IMongoDatabase Database { get;private set; }

        public override void Awake()
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
            foreach(var type in types)
            {
                AddComponent(type);
            }
        }

        private BaseComponent AddComponent(Type type)
        {
            if (!typeof(IRpository).IsAssignableFrom(type))
                return null;

            var isSingle = ComponentPool.IsSingleType(type);
            var component = ComponentPool.Fetch(type);
            (component as IRpository).SetDBContext(this.Database);

            if (isSingle)
            {
                SingleDictionary[type] = component;
            }
            else
            {
                IdComponent.AddOrUpdate(component.Id, component, (k, v) => { return component; });
                if (!TypeComponent.TryGetValue(type, out HashSet<BaseComponent> components))
                {
                    components = new HashSet<BaseComponent>();
                    TypeComponent[type] = components;
                }
                components.Add(component);
            }
            return component;
        }

        public override T AddComponent<T>()
        {
            var type = typeof(T);
            if (!typeof(IRpository).IsAssignableFrom(type))
                return default;

            return (T)AddComponent(type);
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
