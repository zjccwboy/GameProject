using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using H6Game.Hotfix.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace H6Game.Base
{
    /// <summary>
    /// MongoDB 驱动上下文类。
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DBContext<TEntity> where TEntity : BaseEntity
    {
        private string DatabaseName { get; set; }
        private MongoClient DBClient { get; set; }

        /// <summary>
        /// Mongo server.
        /// </summary>
        public MongoServer DBServer => DBClient.GetServer();
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="database">Mongo database.</param>
        /// <param name="databaseName">Mongo database name.</param>
        /// <param name="dbClient">Mongo database client.</param>
        public DBContext(IMongoDatabase database, string databaseName, MongoClient dbClient)
        {
            this.Database = database;
            this.DatabaseName = databaseName;
            this.DBClient = dbClient;
        }
        #endregion

        #region 公共接口

        /// <summary>
        /// Mongo database.
        /// </summary>
        public IMongoDatabase Database { get; }

        /// <summary>
        /// Mongo collection.
        /// </summary>
        public IMongoCollection<TEntity> Collection => GetMongoCollection(typeof(TEntity).Name);

        /// <summary>
        /// 创建一个 mongo collection.
        /// </summary>
        /// <param name="indexFields">索引字段名。</param>
        /// <param name="options">创建索引配置选项。</param>
        public void CreateCollectionIndex(string[] indexFields, CreateIndexOptions options = null)
        {
            var collectionName = typeof(TEntity).Name;
            CreateIndex(GetMongoCollection(collectionName), indexFields, options);
        }

        /// <summary>
        /// 创建一个 mongo collection
        /// </summary>
        /// <param name="indexFields">索引字段名。</param>
        /// <param name="options">创建索引配置选项。</param>
        public void CreateCollection(string[] indexFields = null, CreateIndexOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            this.Database.CreateCollection(collectionName);
            CreateIndex(GetMongoCollection(collectionName), indexFields, options);
        }

        /// <summary>
        /// 查找。
        /// </summary>
        /// <param name="filter">过滤表达式。</param>
        /// <param name="options">查找配置项。</param>
        /// <returns>返回实体集合。</returns>
        public List<TEntity> Find(Expression<Func<TEntity, bool>> filter, FindOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            return colleciton.Find(filter, options).ToList();
        }

        /// <summary>
        /// 异步查找。
        /// </summary>
        /// <param name="filter">过滤表达式。</param>
        /// <param name="options">查找配置项。</param>
        /// <returns>返回实体集合。</returns>
        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, FindOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            return colleciton.Find(filter, options).ToListAsync();
        }

        /// <summary>
        /// 按需查找，可以指定字段名查找，实现要多少取多少的功能。
        /// </summary>
        /// <typeparam name="TMember">查找条件数据类型。</typeparam>
        /// <param name="memberExpression">查找成员表达式。</param>
        /// <param name="value">查找匹配结果值。</param>
        /// <param name="fields">查找结果实体成员字段名。</param>
        /// <returns>返回实体集合。</returns>
        public List<TEntity> FindAs<TMember>(Expression<Func<TEntity, TMember>> memberExpression, TMember value, params string[] fields)
        {
            var collection = this.DBServer.GetDatabase(this.DatabaseName).GetCollection<TEntity>(typeof(TEntity).Name);
            var fs = Fields.Include(fields);
            var query = Query<TEntity>.EQ(memberExpression, value);
            var q = collection.FindAs<TEntity>(query).SetFields(fs);

            if (q == null)
                return null;

            return q.ToList();
        }

        /// <summary>
        /// 按需查找异步，可以指定字段名查找，实现要多少取多少的功能。
        /// </summary>
        /// <typeparam name="TMember">查找条件数据类型。</typeparam>
        /// <param name="memberExpression">查找成员表达式。</param>
        /// <param name="value">查找匹配结果值。</param>
        /// <param name="fields">查找结果实体成员字段名。</param>
        /// <returns>返回实体集合。</returns>
        public Task<List<TEntity>> FindAsAsync<TMember>(Expression<Func<TEntity, TMember>> memberExpression, TMember value, params string[] fields)
        {
            var collection = this.DBServer.GetDatabase(this.DatabaseName).GetCollection<TEntity>(typeof(TEntity).Name);
            var fs = Fields.Include(fields);
            var query = Query<TEntity>.EQ(memberExpression, value);
            var q = collection.FindAs<TEntity>(query).SetFields(fs);

            if (q == null)
                return null;

            var result = q.ToList();
            return Task.FromResult(result);
        }

        /// <summary>
        /// 使用Mongo object id查找。
        /// </summary>
        /// <param name="objectId">object id.</param>
        /// <param name="options">查找配置项。</param>
        /// <returns>返回实体集合。</returns>
        public List<TEntity> FindById(string objectId, FindOptions options = null)
        {
            return Find(t => t.Id == objectId);
        }

        /// <summary>
        /// 使用Mongo object id查找异步。
        /// </summary>
        /// <param name="objectId">object id.</param>
        /// <param name="options">查找配置项。</param>
        /// <returns>返回实体集合。</returns>
        public Task<List<TEntity>> FindByIdAsync(string objectId, FindOptions options = null)
        {
            return FindAsync(t => t.Id == objectId);
        }

        /// <summary>
        /// 分页查找。
        /// </summary>
        /// <typeparam name="TResult">查找选择器表达式返回数据类型。</typeparam>
        /// <param name="filter">查找过滤器表达式。</param>
        /// <param name="keySelector">查找选择器表达式。</param>
        /// <param name="pageIndex">页码。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="rsCount">分页数量。</param>
        /// <returns>返回实体集合。</returns>
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

        /// <summary>
        /// 分页查找异步。
        /// </summary>
        /// <typeparam name="TResult">查找选择器表达式返回数据类型。</typeparam>
        /// <param name="filter">查找过滤器表达式。</param>
        /// <param name="keySelector">查找选择器表达式。</param>
        /// <param name="pageIndex">页码。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="rsCount">分页数量。</param>
        /// <returns>返回实体集合。</returns>
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

        /// <summary>
        /// 插入一行。
        /// </summary>
        /// <param name="entity">插入数据实体。</param>
        /// <param name="options">插入数据配置项。</param>
        public void Insert(TEntity entity, InsertOneOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            colleciton.InsertOne(entity, options);
        }

        /// <summary>
        /// 插入一行异步。
        /// </summary>
        /// <param name="entity">插入数据实体。</param>
        /// <param name="options">插入数据配置项。</param>
        /// <returns>Task</returns>
        public Task InsertAsync(TEntity entity, InsertOneOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            return colleciton.InsertOneAsync(entity, options);
        }

        /// <summary>
        /// 批量插入。
        /// </summary>
        /// <param name="entities">插入实体集合。</param>
        /// <param name="options">插入数据配置项。</param>
        public void InsertMany(IEnumerable<TEntity> entities, InsertManyOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            colleciton.InsertMany(entities, options);
        }

        /// <summary>
        /// 批量插入。
        /// </summary>
        /// <param name="entities">插入实体集合。</param>
        /// <param name="options">插入数据配置项。</param>
        /// <returns>Task</returns>
        public Task InsertManyAsync(IEnumerable<TEntity> entities, InsertManyOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            return colleciton.InsertManyAsync(entities, options);
        }

        /// <summary>
        /// 更新一行。
        /// </summary>
        /// <param name="entity">更新实体。</param>
        /// <param name="filter">过滤表达式。</param>
        /// <param name="options">更新配置项。</param>
        /// <returns>修改行数。</returns>
        public int Update(TEntity entity, Expression<Func<TEntity, bool>> filter, UpdateOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            List<UpdateDefinition<TEntity>> updateList = BuildUpdateDefinition(entity, null, null);
            var result = colleciton.UpdateOne(filter, Builders<TEntity>.Update.Combine(updateList), options);
            return (int)result.ModifiedCount;
        }

        /// <summary>
        /// 更新一行异步。
        /// </summary>
        /// <param name="entity">更新实体。</param>
        /// <param name="filter">过滤表达式。</param>
        /// <param name="options">更新配置项。</param>
        /// <returns>修改行数。</returns>
        public async Task<int> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> filter, UpdateOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            List<UpdateDefinition<TEntity>> updateList = BuildUpdateDefinition(entity, null, null);
            var result = await colleciton.UpdateOneAsync(filter, Builders<TEntity>.Update.Combine(updateList), options);
            return (int)result.ModifiedCount;
        }

        /// <summary>
        /// 根据指定字段更新某一行。
        /// </summary>
        /// <param name="filter">过滤表达式。</param>
        /// <param name="updateFields">指定更新字段。</param>
        /// <param name="options">更新配置项。</param>
        /// <returns>修改行数。</returns>
        public int Update(Expression<Func<TEntity, bool>> filter, UpdateDefinition<TEntity> updateFields, UpdateOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            var result = colleciton.UpdateOne(filter, updateFields, options);
            return (int)result.ModifiedCount;
        }

        /// <summary>
        /// 根据指定字段更新某一行异步。
        /// </summary>
        /// <param name="filter">过滤表达式。</param>
        /// <param name="updateFields">指定更新字段。</param>
        /// <param name="options">更新配置项。</param>
        /// <returns>修改行数。</returns>
        public async Task<int> UpdateAsync(Expression<Func<TEntity, bool>> filter, UpdateDefinition<TEntity> updateFields, UpdateOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            var result = await colleciton.UpdateOneAsync(filter, updateFields, options);
            return (int)result.ModifiedCount;
        }

        /// <summary>
        /// 更新多行。
        /// </summary>
        /// <param name="entity">更新实体。</param>
        /// <param name="filter">更新过滤表达式。</param>
        /// <param name="options">更新配置项。</param>
        /// <returns>修改行数。</returns>
        public int UpdateMany(TEntity entity, Expression<Func<TEntity, bool>> filter, UpdateOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            List<UpdateDefinition<TEntity>> updateList = BuildUpdateDefinition(entity, null, null);
            var result = colleciton.UpdateMany(filter, Builders<TEntity>.Update.Combine(updateList), options);
            return (int)result.ModifiedCount;
        }

        /// <summary>
        /// 更新多行异步。
        /// </summary>
        /// <param name="entity">更新实体。</param>
        /// <param name="filter">更新过滤表达式。</param>
        /// <param name="options">更新配置项。</param>
        /// <returns>修改行数。</returns>
        public async Task<int> UpdateManyAsync(TEntity entity, Expression<Func<TEntity, bool>> filter, UpdateOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            List<UpdateDefinition<TEntity>> updateList = BuildUpdateDefinition(entity, null, null);
            var result = await colleciton.UpdateManyAsync(filter, Builders<TEntity>.Update.Combine(updateList), options);
            return (int)result.ModifiedCount;
        }

        /// <summary>
        /// 批量更新，并指定更新的字段。
        /// </summary>
        /// <param name="entity">更新实体。</param>
        /// <param name="filter">过滤表达式。</param>
        /// <param name="updateFields">更新字段名。</param>
        /// <returns>修改行数。</returns>
        public int UpdateManyAs(TEntity entity, Expression<Func<TEntity, bool>> filter, params string[] updateFields)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            List<UpdateDefinition<TEntity>> updateList = BuildUpdateDefinition(entity, null, updateFields);
            var result = colleciton.UpdateMany(filter, Builders<TEntity>.Update.Combine(updateList), null);
            return (int)result.ModifiedCount;
        }

        /// <summary>
        /// 批量更新，并指定更新的字段异步。
        /// </summary>
        /// <param name="entity">更新实体。</param>
        /// <param name="filter">过滤表达式。</param>
        /// <param name="updateFields">更新字段名。</param>
        /// <returns>修改行数。</returns>
        public async Task<int> UpdateManyAsAsync(TEntity entity, Expression<Func<TEntity, bool>> filter, params string[] updateFields)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            List<UpdateDefinition<TEntity>> updateList = BuildUpdateDefinition(entity, null, updateFields);
            var result = await colleciton.UpdateManyAsync(filter, Builders<TEntity>.Update.Combine(updateList), null);
            return (int)result.ModifiedCount;
        }

        /// <summary>
        /// 删除一行。
        /// </summary>
        /// <param name="filter">过滤表达式。</param>
        /// <param name="options">删除配置项。</param>
        /// <returns>删除结果，删除成功为true，删除失败为false。</returns>
        public bool Delete(Expression<Func<TEntity, bool>> filter, DeleteOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            var result = colleciton.DeleteOne(filter, options);
            return result.DeletedCount > 0;
        }

        /// <summary>
        /// 删除一行异步。
        /// </summary>
        /// <param name="filter">过滤表达式。</param>
        /// <param name="options">删除配置项。</param>
        /// <returns>删除结果，删除成功为true，删除失败为false。</returns>
        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> filter, DeleteOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            var result = await colleciton.DeleteOneAsync(filter, options);
            return result.DeletedCount > 0;
        }

        /// <summary>
        /// 删除多行。
        /// </summary>
        /// <param name="filter">过滤表达式。</param>
        /// <param name="options">删除配置项。</param>
        /// <returns>删除结果。</returns>
        public DeleteResult DeleteMany(Expression<Func<TEntity, bool>> filter, DeleteOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            return colleciton.DeleteMany(filter, options);
        }

        /// <summary>
        /// 删除多行异步。
        /// </summary>
        /// <param name="filter">过滤表达式。</param>
        /// <param name="options">删除配置项。</param>
        /// <returns>删除结果。</returns>
        public Task<DeleteResult> DeleteManyAsync(Expression<Func<TEntity, bool>> filter, DeleteOptions options = null)
        {
            string collectionName = typeof(TEntity).Name;
            var colleciton = GetMongoCollection(collectionName);
            var result = colleciton.DeleteManyAsync(filter, options);
            return result;
        }

        /// <summary>
        /// 清除Mongo Collection.
        /// </summary>
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

                var elmentName = entity.BsonElementName(property.Name);

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
