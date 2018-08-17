using H6Game.Entitys;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace H6Game.Base
{
    public interface IContext
    {
        IMongoDatabase Database { get;}

        void Insert<DBEntity>(DBEntity entity) where DBEntity : BaseEntity;
        Task InsertAsync<DBEntity>(DBEntity entity) where DBEntity : BaseEntity;

        void InsertMany<DBEntity>(IEnumerable<DBEntity> entities) where DBEntity : BaseEntity;
        Task InsertManyAsync<DBEntity>(IEnumerable<DBEntity> entities) where DBEntity : BaseEntity;

        void Remove<DBEntity>(DBEntity entity) where DBEntity : BaseEntity;
        Task RemoveAsync<DBEntity>(DBEntity entity) where DBEntity : BaseEntity;

        PEntity Query<QEntity, PEntity>(QEntity entity) where QEntity : BaseEntity where PEntity : BaseEntity;
        Task<PEntity> QueryAsync<QEntity, PEntity>(QEntity entity) where QEntity : BaseEntity where PEntity : BaseEntity;

        DBEntity Query<DBEntity>(string objectId) where DBEntity : BaseEntity;
        Task<DBEntity> QueryAsync<DBEntity>(string objectId) where DBEntity : BaseEntity;

        bool Modify<DBEntity>(DBEntity entity) where DBEntity : BaseEntity;
        Task<bool> ModifyAsync<DBEntity>(DBEntity entity) where DBEntity : BaseEntity;
    }
}
