using H6Game.Entities;
using MongoDB.Driver;

namespace H6Game.Base
{
    public interface IRpository<TEntity> : IRpository where TEntity : BaseEntity
    {
        DBContext<TEntity> DBContext { get;}
    }

    public interface IRpository
    {
        void SetDBContext(IMongoDatabase database);
    }
}
