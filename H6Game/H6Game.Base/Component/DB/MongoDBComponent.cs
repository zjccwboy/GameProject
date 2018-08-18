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
        private IMongoDatabase MongoDatabase { get; set; }
        public IMongoDatabase Database { get { return MongoDatabase; } }
        private IContext Context { get; set; }

        public override void Awake()
        {
            this.Config = Game.Scene.GetComponent<NetConfigComponent>().ConfigEntity;
            this.DBClient = new MongoClient(Config.DbConfig.ConnectionString);
            this.DatabaseNaeme = this.Config.DbConfig.DatabaseName;
            SetMongoDatabase();

            this.Context = new DBContext(this.Database);
            AddRpositoryComponents();
        }

        private void AddRpositoryComponents()
        {
            var types = TypePool.GetTypes<BaseRpository>();
            foreach(var type in types)
            {
                AddComponent(type);
            }
        }

        private void AddComponent(Type type)
        {
            if (type.BaseType != typeof(BaseRpository))
                return;

            var isSingle = ComponentPool.IsSingleType(type);
            var component = ComponentPool.Fetch(type);
            (component as BaseRpository).DBContext = this.Context;

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
        }

        public override T AddComponent<T>()
        {
            var type = typeof(T);
            if (type.BaseType != typeof(BaseRpository))
                return default;

            var isSingle = ComponentPool.IsSingleType(type);
            var component = ComponentPool.Fetch<T>();
            (component as BaseRpository).DBContext = this.Context;

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

        private void SetMongoDatabase()
        {
            if (Database == null)
            {                
                if (!DatabaseExists(this.DBClient, this.DatabaseNaeme))
                {
                    throw new KeyNotFoundException("此MongoDB名称不存在：" + this.DatabaseNaeme);
                }

                this.MongoDatabase = this.DBClient.GetDatabase(this.DatabaseNaeme);
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
