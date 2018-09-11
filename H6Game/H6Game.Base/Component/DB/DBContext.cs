using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using H6Game.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace H6Game.Base
{

    public class DBContext<TEntity> : IContext<TEntity> where TEntity : BaseEntity
    {
        #region 构造函数
        public DBContext(IMongoDatabase database)
        {
            this.Database = database;
        }
        #endregion

        #region 公共接口
        public IMongoDatabase Database { get; }

        public void CreateCollectionIndex(string[] indexFields, CreateIndexOptions options = null)
        {
            var collectionName = typeof(TEntity).Name;
            CreateIndex(GetMongoCollection(collectionName), indexFields, options);
        }

        public void CreateCollection(string[] indexFields = null, CreateIndexOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            this.Database.CreateCollection(collectionName);
            CreateIndex(GetMongoCollection(collectionName), indexFields, options);
        }

        public List<TEntity> Find(Expression<Func<TEntity, bool>> filter, FindOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            return colleciton.Find(filter, options).ToList();
        }

        public List<TEntity> FindTest(Expression<Func<TEntity, bool>> filter, FindOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);            
            return colleciton.Find(filter, options).ToList();
        }

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, FindOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            return colleciton.Find(filter, options).ToListAsync();
        }

        public List<TEntity> FindById(string objectId, FindOptions options = null)
        {
            return Find(t => t.Id == objectId);
        }

        public Task<List<TEntity>> FindByIdAsync(string objectId, FindOptions options = null)
        {
            return FindAsync(t => t.Id == objectId);
        }

        public List<TEntity> FindByPage<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> keySelector, int pageIndex, int pageSize, out int rsCount)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            rsCount = colleciton.AsQueryable().Where(filter).Count();

            int pageCount = rsCount / pageSize + ((rsCount % pageSize) > 0 ? 1 : 0);
            if (pageIndex > pageCount) pageIndex = pageCount;
            if (pageIndex <= 0) pageIndex = 1;

            return colleciton.AsQueryable(new AggregateOptions { AllowDiskUse = true }).Where(filter).OrderByDescending(keySelector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public Task<List<TEntity>> FindByPageAsync<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> keySelector, int pageIndex, int pageSize, out int rsCount)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            rsCount = colleciton.AsQueryable().Where(filter).Count();

            int pageCount = rsCount / pageSize + ((rsCount % pageSize) > 0 ? 1 : 0);
            if (pageIndex > pageCount) pageIndex = pageCount;
            if (pageIndex <= 0) pageIndex = 1;

            var result = colleciton.AsQueryable(new AggregateOptions { AllowDiskUse = true }).Where(filter).OrderByDescending(keySelector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var tcs = new TaskCompletionSource<List<TEntity>>();
            tcs.TrySetResult(result);
            return tcs.Task;

        }

        public void Insert(TEntity entity, InsertOneOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            colleciton.InsertOne(entity, options);
        }

        public Task InsertAsync(TEntity entity, InsertOneOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            return colleciton.InsertOneAsync(entity, options);
        }


        public void InsertMany(IEnumerable<TEntity> docs, InsertManyOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            colleciton.InsertMany(docs, options);
        }

        public Task InsertManyAsync(IEnumerable<TEntity> docs, InsertManyOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            return colleciton.InsertManyAsync(docs, options);
        }

        public void Update(TEntity entity, Expression<Func<TEntity, bool>> filter, UpdateOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            List<UpdateDefinition<TEntity>> updateList = BuildUpdateDefinition(entity, null, null);
            colleciton.UpdateOne(filter, Builders<TEntity>.Update.Combine(updateList), options);
        }

        public Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> filter, UpdateOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            List<UpdateDefinition<TEntity>> updateList = BuildUpdateDefinition(entity, null, null);
            return colleciton.UpdateOneAsync(filter, Builders<TEntity>.Update.Combine(updateList), options);
        }

        public void Update(Expression<Func<TEntity, bool>> filter, UpdateDefinition<TEntity> updateFields, UpdateOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            colleciton.UpdateOne(filter, updateFields, options);
        }

        public Task UpdateAsync(Expression<Func<TEntity, bool>> filter, UpdateDefinition<TEntity> updateFields, UpdateOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            return colleciton.UpdateOneAsync(filter, updateFields, options);
        }

        public void UpdateMany(TEntity entity, Expression<Func<TEntity, bool>> filter, UpdateOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            List<UpdateDefinition<TEntity>> updateList = BuildUpdateDefinition(entity, null, null);
            colleciton.UpdateMany(filter, Builders<TEntity>.Update.Combine(updateList), options);
        }

        public void UpdateMany(TEntity entity, Expression<Func<TEntity, bool>> filter, IEnumerable<string> updateFields, UpdateOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            List<UpdateDefinition<TEntity>> updateList = BuildUpdateDefinition(entity, null, updateFields);
            colleciton.UpdateMany(filter, Builders<TEntity>.Update.Combine(updateList), options);
        }

        public Task UpdateManyAsync(TEntity entity, Expression<Func<TEntity, bool>> filter, UpdateOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            List<UpdateDefinition<TEntity>> updateList = BuildUpdateDefinition(entity, null, null);
            return colleciton.UpdateManyAsync(filter, Builders<TEntity>.Update.Combine(updateList), options);
        }

        public Task UpdateManyAsync(TEntity entity, Expression<Func<TEntity, bool>> filter, IEnumerable<string> updateFields, UpdateOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            List<UpdateDefinition<TEntity>> updateList = BuildUpdateDefinition(entity, null, updateFields);
            return colleciton.UpdateManyAsync(filter, Builders<TEntity>.Update.Combine(updateList), options);
        }

        public void Delete(Expression<Func<TEntity, bool>> filter, DeleteOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            colleciton.DeleteOne(filter, options);
        }

        public Task DeleteAsync(Expression<Func<TEntity, bool>> filter, DeleteOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            return colleciton.DeleteOneAsync(filter, options);
        }

        public DeleteResult DeleteMany(Expression<Func<TEntity, bool>> filter, DeleteOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            return colleciton.DeleteMany(filter, options);
        }

        public Task<DeleteResult> DeleteManyAsync(Expression<Func<TEntity, bool>> filter, DeleteOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            var result = colleciton.DeleteManyAsync(filter, options);
            return result;
        }

        public void ClearCollection()
        {
            var collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            var inddexs = colleciton.Indexes.List();
            List<IEnumerable<BsonDocument>> docIndexs = new List<IEnumerable<BsonDocument>>();
            while (inddexs.MoveNext())
            {
                docIndexs.Add(inddexs.Current);
            }

            this.Database.DropCollection(collectionName);

            if (!CollectionExists(collectionName))
            {
                CreateCollection();
            }

            if (docIndexs.Count > 0)
            {
                colleciton = this.Database.GetCollection<TEntity>(collectionName);
                foreach (var index in docIndexs)
                {
                    foreach (IndexKeysDefinition<TEntity> indexItem in index)
                    {
                        try
                        {
                            colleciton.Indexes.CreateOne(indexItem);
                        }
                        catch
                        { }
                    }
                }
            }

        }
        #endregion

        #region 内部方法
        private bool CollectionExists(string collectionName)
        {
            var options = new ListCollectionsOptions
            {
                Filter = Builders<BsonDocument>.Filter.Eq("name", collectionName)
            };
            return this.Database.ListCollections(options).ToEnumerable().Any();
        }

        private IMongoCollection<TEntity> GetMongoCollection(string name, MongoCollectionSettings settings = null)
        {
            if (!CollectionExists(name))
            {
                throw new KeyNotFoundException("此Collection名称不存在：" + name);
            }
            return this.Database.GetCollection<TEntity>(name, settings);
        }
        

        private List<UpdateDefinition<TEntity>> BuildUpdateDefinition(TEntity entity, string parent, IEnumerable<string> updateFields)
        {
            var updateList = new List<UpdateDefinition<TEntity>>();
            var properties = entity.GetPropertys();
            foreach (var property in properties)
            {
                if (updateFields != null && !updateFields.Contains(property.Name))
                    continue;

                var elmentName = entity.GetElementName(property.Name);

                string key;
                if(elmentName == null)
                    key = parent == null ? property.Name : $"{parent}.{property.Name}";
                else
                    key = parent == null ? property.Name : $"{parent}.{property.Name}";

                if (key == "Id")
                    continue;

                //非空的复杂类型
                if ((property.PropertyType.IsClass || property.PropertyType.IsInterface) && property.PropertyType != typeof(string) && property.GetValue(entity) != null)
                {
                    if (typeof(IList).IsAssignableFrom(property.PropertyType))
                    {
                        #region 集合类型
                        int i = 0;
                        var subObj = property.GetValue(entity);
                        foreach (var item in subObj as IList)
                        {
                            if (item.GetType().IsClass || item.GetType().IsInterface)
                            {
                                updateList.AddRange(BuildUpdateDefinition(entity, $"{key}.{i}", updateFields));
                            }
                            else
                            {
                                updateList.Add(Builders<TEntity>.Update.Set($"{key}.{i}", item));
                            }
                            i++;
                        }
                        #endregion
                    }
                    else
                    {
                        #region 实体类型
                        //复杂类型，导航属性，类对象和集合对象 
                        var subObj = property.GetValue(entity);
                        foreach (var sub in property.PropertyType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                        {
                            updateList.Add(Builders<TEntity>.Update.Set($"{key}.{sub.Name}", sub.GetValue(subObj)));
                        }
                        #endregion
                    }
                }
                else //简单类型
                {
                    updateList.Add(Builders<TEntity>.Update.Set(key, property.GetValue(entity)));
                }
            }

            return updateList;
        }

        private void CreateIndex(IMongoCollection<TEntity> col, string[] indexFields, CreateIndexOptions options = null)
        {
            if (indexFields == null)
            {
                return;
            }
            var indexKeys = Builders<TEntity>.IndexKeys;
            IndexKeysDefinition<TEntity> keys = null;
            if (indexFields.Length > 0)
            {
                keys = indexKeys.Descending(indexFields[0]);
            }
            for (var i = 1; i < indexFields.Length; i++)
            {
                var strIndex = indexFields[i];
                keys = keys.Descending(strIndex);
            }

            if (keys != null)
            {
                col.Indexes.CreateOne(keys, options);
            }

        }
        #endregion
    }
}
