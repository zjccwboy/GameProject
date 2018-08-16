using H6Game.Entitys;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace H6Game.Base
{
    public interface IContext
    {
        IMongoDatabase Database { get; set; }

        void Insert<DBEntity>(DBEntity entity) where DBEntity : BaseEntity;
        Task InsertAsync<DBEntity>(DBEntity entity) where DBEntity : BaseEntity;

        void Remove<DBEntity>(DBEntity entity) where DBEntity : BaseEntity;
        Task RemoveAsync<DBEntity>(DBEntity entity) where DBEntity : BaseEntity;

        PEntity Query<QEntity, PEntity>(QEntity entity) where QEntity : BaseEntity where PEntity : BaseEntity;
        Task<PEntity> QueryAsync<QEntity, PEntity>(QEntity entity) where QEntity : BaseEntity where PEntity : BaseEntity;

        DBEntity Query<DBEntity>(string objectId) where DBEntity : BaseEntity;
        Task<DBEntity> QueryAsync<DBEntity>(string objectId) where DBEntity : BaseEntity;

        void Modify<DBEntity>(DBEntity entity) where DBEntity : BaseEntity;
        Task ModifyAsync<DBEntity>(DBEntity entity) where DBEntity : BaseEntity;

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
