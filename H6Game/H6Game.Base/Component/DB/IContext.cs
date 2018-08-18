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

        void CreateCollectionIndex<TDoc>(string[] indexFields, CreateIndexOptions options = null);
        void CreateCollection<TDoc>(string[] indexFields = null, CreateIndexOptions options = null);

        List<TDoc> Find<TDoc>(Expression<Func<TDoc, bool>> filter, FindOptions options = null);
        Task<List<TDoc>> FindAsync<TDoc>(Expression<Func<TDoc, bool>> filter, FindOptions options = null);

        List<TDoc> FindByPage<TDoc, TResult>(Expression<Func<TDoc, bool>> filter, Expression<Func<TDoc, TResult>> keySelector, int pageIndex, int pageSize, out int rsCount);
        Task<List<TDoc>> FindByPageAsync<TDoc, TResult>(Expression<Func<TDoc, bool>> filter, Expression<Func<TDoc, TResult>> keySelector, int pageIndex, int pageSize, out int rsCount);

        void Insert<TDoc>(TDoc doc, InsertOneOptions options = null);
        Task InsertAsync<TDoc>(TDoc doc, InsertOneOptions options = null);

        void InsertMany<TDoc>(IEnumerable<TDoc> docs, InsertManyOptions options = null);
        Task InsertManyAsync<TDoc>(IEnumerable<TDoc> docs, InsertManyOptions options = null);

        void Update<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateOptions options = null);
        Task UpdateAsync<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateOptions options = null);

        void Update<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateDefinition<TDoc> updateFields, UpdateOptions options = null);
        Task UpdateAsync<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateDefinition<TDoc> updateFields, UpdateOptions options = null);

        void UpdateMany<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateOptions options = null);
        Task UpdateManyAsync<TDoc>(TDoc doc, Expression<Func<TDoc, bool>> filter, UpdateOptions options = null);

        void Delete<TDoc>(Expression<Func<TDoc, bool>> filter, DeleteOptions options = null);
        Task DeleteAsync<TDoc>(Expression<Func<TDoc, bool>> filter, DeleteOptions options = null);

        void DeleteMany<TDoc>(Expression<Func<TDoc, bool>> filter, DeleteOptions options = null);
        Task DeleteManyAsync<TDoc>(Expression<Func<TDoc, bool>> filter, DeleteOptions options = null);

        void ClearCollection<TDoc>();
    }
}
