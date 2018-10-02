using MongoDB.Driver;

namespace H6Game.Base
{
    public interface IRpository<TEntity> : IRpository where TEntity : BaseEntity
    {
        DBContext<TEntity> DBContext { get;}
    }

    public interface IRpository
    {
        DBType DBType { get; }
        void SetDBContext(IMongoDatabase database, string databaseName, MongoClient dbClient);
    }
}
