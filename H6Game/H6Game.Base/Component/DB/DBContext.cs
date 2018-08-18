﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using H6Game.Entitys;
using MongoDB.Bson;
using MongoDB.Driver;

namespace H6Game.Base
{

    public class DBContext : IContext
    {
        private EntityComponent EntityComponent { get; set; }

        #region 构造函数
        public DBContext(IMongoDatabase database)
        {
            this.Database = database;
            this.EntityComponent = Game.Scene.AddComponent<EntityComponent>();
        }
        #endregion

        #region 公共接口
        public IMongoDatabase Database { get; }

        public void CreateCollectionIndex<TDoc>(string[] indexFields, CreateIndexOptions options = null) where TDoc : BaseEntity
        {
            var collectionName = typeof(TDoc).Name;
            CreateIndex(GetMongoCollection<TDoc>(collectionName), indexFields, options);
        }

        public void CreateCollection<TDoc>(string[] indexFields = null, CreateIndexOptions options = null) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            this.Database.CreateCollection(collectionName);
            CreateIndex(GetMongoCollection<TDoc>(collectionName), indexFields, options);
        }

        public List<TDoc> Find<TDoc>(Expression<Func<TDoc, bool>> filter, FindOptions options = null) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            return colleciton.Find(filter, options).ToList();
        }

        public Task<List<TDoc>> FindAsync<TDoc>(Expression<Func<TDoc, bool>> filter, FindOptions options = null) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            return colleciton.Find(filter, options).ToListAsync();
        }

        public List<TDoc> FindByPage<TDoc, TResult>(Expression<Func<TDoc, bool>> filter, Expression<Func<TDoc, TResult>> keySelector, int pageIndex, int pageSize, out int rsCount) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            rsCount = colleciton.AsQueryable().Where(filter).Count();

            int pageCount = rsCount / pageSize + ((rsCount % pageSize) > 0 ? 1 : 0);
            if (pageIndex > pageCount) pageIndex = pageCount;
            if (pageIndex <= 0) pageIndex = 1;

            return colleciton.AsQueryable(new AggregateOptions { AllowDiskUse = true }).Where(filter).OrderByDescending(keySelector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public Task<List<TDoc>> FindByPageAsync<TDoc, TResult>(Expression<Func<TDoc, bool>> filter, Expression<Func<TDoc, TResult>> keySelector, int pageIndex, int pageSize, out int rsCount) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            rsCount = colleciton.AsQueryable().Where(filter).Count();

            int pageCount = rsCount / pageSize + ((rsCount % pageSize) > 0 ? 1 : 0);
            if (pageIndex > pageCount) pageIndex = pageCount;
            if (pageIndex <= 0) pageIndex = 1;

            var result = colleciton.AsQueryable(new AggregateOptions { AllowDiskUse = true }).Where(filter).OrderByDescending(keySelector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var tcs = new TaskCompletionSource<List<TDoc>>();
            tcs.TrySetResult(result);
            return tcs.Task;

        }

        public void Insert<TDoc>(TDoc doc, InsertOneOptions options = null) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            colleciton.InsertOne(doc, options);
        }

        public Task InsertAsync<TDoc>(TDoc doc, InsertOneOptions options = null) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            return colleciton.InsertOneAsync(doc, options);
        }


        public void InsertMany<TDoc>(IEnumerable<TDoc> docs, InsertManyOptions options = null) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            colleciton.InsertMany(docs, options);
        }

        public Task InsertManyAsync<TDoc>(IEnumerable<TDoc> docs, InsertManyOptions options = null) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            return colleciton.InsertManyAsync(docs, options);
        }

        public void Update<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateOptions options = null) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            List<UpdateDefinition<TDoc>> updateList = BuildUpdateDefinition<TDoc>(doc, null);
            colleciton.UpdateOne(filter, Builders<TDoc>.Update.Combine(updateList), options);
        }

        public Task UpdateAsync<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateOptions options = null) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            List<UpdateDefinition<TDoc>> updateList = BuildUpdateDefinition<TDoc>(doc, null);
            return colleciton.UpdateOneAsync(filter, Builders<TDoc>.Update.Combine(updateList), options);
        }

        public void Update<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateDefinition<TDoc> updateFields, UpdateOptions options = null) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            colleciton.UpdateOne(filter, updateFields, options);
        }

        public Task UpdateAsync<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateDefinition<TDoc> updateFields, UpdateOptions options = null) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            return colleciton.UpdateOneAsync(filter, updateFields, options);
        }

        public void UpdateMany<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateOptions options = null) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            List<UpdateDefinition<TDoc>> updateList = BuildUpdateDefinition<TDoc>(doc, null);
            colleciton.UpdateMany(filter, Builders<TDoc>.Update.Combine(updateList), options);
        }

        public Task UpdateManyAsync<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateOptions options = null) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            List<UpdateDefinition<TDoc>> updateList = BuildUpdateDefinition<TDoc>(doc, null);
            return colleciton.UpdateManyAsync(filter, Builders<TDoc>.Update.Combine(updateList), options);
        }

        public void Delete<TDoc>(Expression<Func<TDoc, bool>> filter, DeleteOptions options = null) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            colleciton.DeleteOne(filter, options);
        }

        public Task DeleteAsync<TDoc>(Expression<Func<TDoc, bool>> filter, DeleteOptions options = null) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            return colleciton.DeleteOneAsync(filter, options);
        }

        public DeleteResult DeleteMany<TDoc>(Expression<Func<TDoc, bool>> filter, DeleteOptions options = null) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            return colleciton.DeleteMany(filter, options);
        }

        public Task<DeleteResult> DeleteManyAsync<TDoc>(Expression<Func<TDoc, bool>> filter, DeleteOptions options = null) where TDoc : BaseEntity
        {
            string collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            var result = colleciton.DeleteManyAsync(filter, options);
            return result;
        }

        public void ClearCollection<TDoc>() where TDoc : BaseEntity
        {
            var collectionName = typeof(TDoc).Name;
            var colleciton = GetMongoCollection<TDoc>(collectionName);
            var inddexs = colleciton.Indexes.List();
            List<IEnumerable<BsonDocument>> docIndexs = new List<IEnumerable<BsonDocument>>();
            while (inddexs.MoveNext())
            {
                docIndexs.Add(inddexs.Current);
            }

            this.Database.DropCollection(collectionName);

            if (!CollectionExists(collectionName))
            {
                CreateCollection<TDoc>();
            }

            if (docIndexs.Count > 0)
            {
                colleciton = this.Database.GetCollection<TDoc>(collectionName);
                foreach (var index in docIndexs)
                {
                    foreach (IndexKeysDefinition<TDoc> indexItem in index)
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

        private IMongoCollection<TDoc> GetMongoCollection<TDoc>(string name, MongoCollectionSettings settings = null)
        {
            if (!CollectionExists(name))
            {
                throw new KeyNotFoundException("此Collection名称不存在：" + name);
            }
            return this.Database.GetCollection<TDoc>(name, settings);
        }

        private List<UpdateDefinition<TDoc>> BuildUpdateDefinition<TDoc>(TDoc doc, string parent) where TDoc : BaseEntity
        {
            var updateList = new List<UpdateDefinition<TDoc>>();
            var properties = this.EntityComponent.GetPropertys<TDoc>();
            foreach (var property in properties)
            {
                var elmentName = this.EntityComponent.GetPropertyElementName(doc, property.Name);

                string key;                
                if(elmentName == null)
                    key = parent == null ? property.Name : string.Format("{0}.{1}", parent, property.Name);
                else
                    key = parent == null ? property.Name : string.Format("{0}.{1}", parent, elmentName);

                if (key == "Id")
                    continue;

                //非空的复杂类型
                if ((property.PropertyType.IsClass || property.PropertyType.IsInterface) && property.PropertyType != typeof(string) && property.GetValue(doc) != null)
                {
                    if (typeof(IList).IsAssignableFrom(property.PropertyType))
                    {
                        #region 集合类型
                        int i = 0;
                        var subObj = property.GetValue(doc);
                        foreach (var item in subObj as IList)
                        {
                            if (item.GetType().IsClass || item.GetType().IsInterface)
                            {
                                updateList.AddRange(BuildUpdateDefinition<TDoc>(doc, string.Format("{0}.{1}", key, i)));
                            }
                            else
                            {
                                updateList.Add(Builders<TDoc>.Update.Set(string.Format("{0}.{1}", key, i), item));
                            }
                            i++;
                        }
                        #endregion
                    }
                    else
                    {
                        #region 实体类型
                        //复杂类型，导航属性，类对象和集合对象 
                        var subObj = property.GetValue(doc);
                        foreach (var sub in property.PropertyType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                        {
                            updateList.Add(Builders<TDoc>.Update.Set(string.Format("{0}.{1}", key, sub.Name), sub.GetValue(subObj)));
                        }
                        #endregion
                    }
                }
                else //简单类型
                {
                    updateList.Add(Builders<TDoc>.Update.Set(key, property.GetValue(doc)));
                }
            }

            return updateList;
        }

        private void CreateIndex<TDoc>(IMongoCollection<TDoc> col, string[] indexFields, CreateIndexOptions options = null) where TDoc : BaseEntity
        {
            if (indexFields == null)
            {
                return;
            }
            var indexKeys = Builders<TDoc>.IndexKeys;
            IndexKeysDefinition<TDoc> keys = null;
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
