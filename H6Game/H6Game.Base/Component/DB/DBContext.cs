using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using H6Game.Entitys;
using MongoDB.Bson;
using MongoDB.Driver;

namespace H6Game.Base.Component.DB
{

    public delegate void DBPostCallback<T>(T state);
    public delegate Task DBPostCallbackAsync<T>(T state);

    public class DBContext : IContext
    {
        public IMongoDatabase Database { get;}

        public DBContext(IMongoDatabase database)
        {
            this.Database = database;
        }

        public void Insert<DBEntity>(DBEntity entity) where DBEntity : BaseEntity
        {
            var tableName = typeof(DBEntity).Name;
            var collection = Database.GetCollection<DBEntity>(tableName);
            collection.InsertOne(entity);
        }

        public Task InsertAsync<DBEntity>(DBEntity entity) where DBEntity : BaseEntity
        {
            var tableName = typeof(DBEntity).Name;
            var collection = Database.GetCollection<DBEntity>(tableName);
            return collection.InsertOneAsync(entity);
        }

        public void InsertMany<DBEntity>(IEnumerable<DBEntity> entities) where DBEntity : BaseEntity
        {
            var tableName = typeof(DBEntity).Name;
            var collection = Database.GetCollection<DBEntity>(tableName);
            collection.InsertMany(entities);
        }

        public Task InsertManyAsync<DBEntity>(IEnumerable<DBEntity> entities) where DBEntity : BaseEntity
        {
            var tableName = typeof(DBEntity).Name;
            var collection = Database.GetCollection<DBEntity>(tableName);
            return collection.InsertManyAsync(entities);
        }


        public bool Modify<DBEntity>(DBEntity entity) where DBEntity : BaseEntity
        {
            var tableName = typeof(DBEntity).Name;
            var collection = Database.GetCollection<DBEntity>(tableName);
            var objectId = new ObjectId(entity.Id);
            var filter = Builders<DBEntity>.Filter.Eq("_id", objectId);
            var update = Builders<DBEntity>.Update.Set("FVIPLevel", 110);            
            var result = collection.UpdateOne(filter, update);
            return result.IsModifiedCountAvailable;
        }

        public async Task<bool> ModifyAsync<DBEntity>(DBEntity entity) where DBEntity : BaseEntity
        {
            var tableName = typeof(DBEntity).Name;
            var collection = Database.GetCollection<DBEntity>(tableName);
            var filter = Builders<DBEntity>.Filter.Eq("Id", entity.Id);
            var update = entity.ToBsonDocument();
            var result = await collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public PEntity Query<QEntity, PEntity>(QEntity entity)
            where QEntity : BaseEntity
            where PEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public DBEntity Query<DBEntity>(string objectId) where DBEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task<PEntity> QueryAsync<QEntity, PEntity>(QEntity entity)
            where QEntity : BaseEntity
            where PEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task<DBEntity> QueryAsync<DBEntity>(string objectId) where DBEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public void Remove<DBEntity>(DBEntity entity) where DBEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync<DBEntity>(DBEntity entity) where DBEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

    }
}
