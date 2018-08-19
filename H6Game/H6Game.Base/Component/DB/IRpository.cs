using H6Game.Entitys;
using MongoDB.Driver;

namespace H6Game.Base
{
    public interface IRpository<TEntity> : IRpository where TEntity : BaseEntity
    {
        IContext<TEntity> DBContext { get;}
    }

    public interface IRpository
    {
        void SetDBContext(IMongoDatabase database);
    }
}
