using H6Game.Entitys;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace H6Game.Base
{
    public interface IContext
    {
        IMongoDatabase Database { get;}

        void CreateCollectionIndex<TDoc>(string[] indexFields, CreateIndexOptions options = null) where TDoc:BaseEntity;
        void CreateCollection<TDoc>(string[] indexFields = null, CreateIndexOptions options = null) where TDoc : BaseEntity;

        List<TDoc> Find<TDoc>(Expression<Func<TDoc, bool>> filter, FindOptions options = null) where TDoc : BaseEntity;
        Task<List<TDoc>> FindAsync<TDoc>(Expression<Func<TDoc, bool>> filter, FindOptions options = null) where TDoc : BaseEntity;

        List<TDoc> FindByPage<TDoc, TResult>(Expression<Func<TDoc, bool>> filter, Expression<Func<TDoc, TResult>> keySelector, int pageIndex, int pageSize, out int rsCount) where TDoc : BaseEntity;
        Task<List<TDoc>> FindByPageAsync<TDoc, TResult>(Expression<Func<TDoc, bool>> filter, Expression<Func<TDoc, TResult>> keySelector, int pageIndex, int pageSize, out int rsCount) where TDoc : BaseEntity;

        void Insert<TDoc>(TDoc doc, InsertOneOptions options = null) where TDoc : BaseEntity;
        Task InsertAsync<TDoc>(TDoc doc, InsertOneOptions options = null) where TDoc : BaseEntity;

        void InsertMany<TDoc>(IEnumerable<TDoc> docs, InsertManyOptions options = null) where TDoc : BaseEntity;
        Task InsertManyAsync<TDoc>(IEnumerable<TDoc> docs, InsertManyOptions options = null) where TDoc : BaseEntity;

        void Update<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateOptions options = null) where TDoc : BaseEntity;
        Task UpdateAsync<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateOptions options = null) where TDoc : BaseEntity;

        void Update<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateDefinition<TDoc> updateFields, UpdateOptions options = null) where TDoc : BaseEntity;
        Task UpdateAsync<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateDefinition<TDoc> updateFields, UpdateOptions options = null) where TDoc : BaseEntity;

        void UpdateMany<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateOptions options = null) where TDoc : BaseEntity;
        Task UpdateManyAsync<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateOptions options = null) where TDoc : BaseEntity;

        void Delete<TDoc>(Expression<Func<TDoc, bool>> filter, DeleteOptions options = null) where TDoc : BaseEntity;
        Task DeleteAsync<TDoc>(Expression<Func<TDoc, bool>> filter, DeleteOptions options = null) where TDoc : BaseEntity;

        DeleteResult DeleteMany<TDoc>(Expression<Func<TDoc, bool>> filter, DeleteOptions options = null) where TDoc : BaseEntity;
        Task<DeleteResult> DeleteManyAsync<TDoc>(Expression<Func<TDoc, bool>> filter, DeleteOptions options = null) where TDoc : BaseEntity;

        void ClearCollection<TDoc>() where TDoc : BaseEntity;
    }
}
