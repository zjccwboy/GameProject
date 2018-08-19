using H6Game.Entitys;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace H6Game.Base
{
    public interface IContext<TDoc> where TDoc : BaseEntity
    {
        IMongoDatabase Database { get;}

        void CreateCollectionIndex(string[] indexFields, CreateIndexOptions options = null);
        void CreateCollection(string[] indexFields = null, CreateIndexOptions options = null);

        List<TDoc> Find(Expression<Func<TDoc, bool>> filter, FindOptions options = null);
        Task<List<TDoc>> FindAsync(Expression<Func<TDoc, bool>> filter, FindOptions options = null);

        List<TDoc> FindById(string objectId, FindOptions options = null);
        Task<List<TDoc>> FindByIdAsync(string objectId, FindOptions options = null);

        List<TDoc> FindByPage<TResult>(Expression<Func<TDoc, bool>> filter, Expression<Func<TDoc, TResult>> keySelector, int pageIndex, int pageSize, out int rsCount);
        Task<List<TDoc>> FindByPageAsync<TResult>(Expression<Func<TDoc, bool>> filter, Expression<Func<TDoc, TResult>> keySelector, int pageIndex, int pageSize, out int rsCount);

        void Insert(TDoc doc, InsertOneOptions options = null);
        Task InsertAsync(TDoc doc, InsertOneOptions options = null);

        void InsertMany(IEnumerable<TDoc> docs, InsertManyOptions options = null);
        Task InsertManyAsync(IEnumerable<TDoc> docs, InsertManyOptions options = null);

        void Update(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateOptions options = null);
        Task UpdateAsync(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateOptions options = null);

        void Update(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateDefinition<TDoc> updateFields, UpdateOptions options = null);
        Task UpdateAsync(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateDefinition<TDoc> updateFields, UpdateOptions options = null);

        void UpdateMany(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateOptions options = null);
        Task UpdateManyAsync(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateOptions options = null);

        void Delete(Expression<Func<TDoc, bool>> filter, DeleteOptions options = null);
        Task DeleteAsync(Expression<Func<TDoc, bool>> filter, DeleteOptions options = null);

        DeleteResult DeleteMany(Expression<Func<TDoc, bool>> filter, DeleteOptions options = null);
        Task<DeleteResult> DeleteManyAsync(Expression<Func<TDoc, bool>> filter, DeleteOptions options = null);

        void ClearCollection();
    }
}
