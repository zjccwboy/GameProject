using H6Game.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace H6Game.Base
{
    public interface IContext<TEntity> where TEntity : BaseEntity
    {
        IMongoDatabase Database { get;}
        IMongoCollection<TEntity> Collection { get; }

        void CreateCollectionIndex(string[] indexFields, CreateIndexOptions options = null);
        void CreateCollection(string[] indexFields = null, CreateIndexOptions options = null);

        List<TEntity> Find(Expression<Func<TEntity, bool>> filter, FindOptions options = null);

        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, FindOptions options = null);

        List<TEntity> FindAs<TMember>(Expression<Func<TEntity, TMember>> memberExpression, TMember value, string[] fields);
        Task<List<TEntity>> FindAsAsync<TMember>(Expression<Func<TEntity, TMember>> memberExpression, TMember value, string[] fields);

        List<TEntity> FindById(string objectId, FindOptions options = null);
        Task<List<TEntity>> FindByIdAsync(string objectId, FindOptions options = null);

        List<TEntity> FindByPage<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> keySelector, int pageIndex, int pageSize, out int rsCount);
        Task<List<TEntity>> FindByPageAsync<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> keySelector, int pageIndex, int pageSize, out int rsCount);

        void Insert(TEntity entity, InsertOneOptions options = null);
        Task InsertAsync(TEntity entity, InsertOneOptions options = null);

        void InsertMany(IEnumerable<TEntity> entitys, InsertManyOptions options = null);
        Task InsertManyAsync(IEnumerable<TEntity> entitys, InsertManyOptions options = null);

        int Update(TEntity entity, Expression<Func<TEntity, bool>> filter, UpdateOptions options = null);
        Task<int> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> filter, UpdateOptions options = null);

        int Update(Expression<Func<TEntity, bool>> filter, UpdateDefinition<TEntity> updateFields, UpdateOptions options = null);
        Task<int> UpdateAsync(Expression<Func<TEntity, bool>> filter, UpdateDefinition<TEntity> updateFields, UpdateOptions options = null);

        int UpdateMany(TEntity entity, Expression<Func<TEntity, bool>> filter, UpdateOptions options = null);
        Task<int> UpdateManyAsync(TEntity entity, Expression<Func<TEntity, bool>> filter, UpdateOptions options = null);

        int UpdateManyAs(TEntity entity, Expression<Func<TEntity, bool>> filter, IEnumerable<string> updateFields);
        Task<int> UpdateManyAsAsync(TEntity entity, Expression<Func<TEntity, bool>> filter, IEnumerable<string> updateFields);

        bool Delete(Expression<Func<TEntity, bool>> filter, DeleteOptions options = null);
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> filter, DeleteOptions options = null);

        DeleteResult DeleteMany(Expression<Func<TEntity, bool>> filter, DeleteOptions options = null);
        Task<DeleteResult> DeleteManyAsync(Expression<Func<TEntity, bool>> filter, DeleteOptions options = null);

        void ClearCollection();
    }
}
