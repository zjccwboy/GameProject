﻿
using H6Game.Entitys;
using MongoDB.Driver;
using System.Linq;

namespace H6Game.Base
{
    public abstract class BaseRpository<TEntity> : BaseComponent, IRpository<TEntity> where TEntity : BaseEntity, new()
    {
        public IContext<TEntity> DBContext { get;private set; }

        public IMongoDatabase DataBase { get;private set; }

        public TEntity DefaultEntity { get; private set; }

        public void SetDBContext(IMongoDatabase database)
        {
            this.DataBase = database;
            this.DBContext = new DBContext<TEntity>(this.DataBase);

            this.DefaultEntity = BaseEntity.Create<TEntity>();

            CreateCollection();
        }

        private void CreateCollection()
        {
            var name = typeof(TEntity).Name;
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