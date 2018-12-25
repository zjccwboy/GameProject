using MongoDB.Driver;

namespace H6Game.Base.Component
{
    public interface IRpository<TEntity> : IRpository where TEntity : BaseEntity, new ()
    {
        DBContext<TEntity> DBContext { get;}
    }

    public interface IRpository
    {
        DBType DBType { get; }
        void SetDBContext(IMongoDatabase database, string databaseName, MongoClient dbClient);
    }
}
