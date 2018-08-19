using H6Game.Entitys;
using MongoDB.Driver;

namespace H6Game.Base
{
    public interface IRpository<TDoc> : IRpository where TDoc : BaseEntity
    {
        IContext<TDoc> DBContext { get; set; }
    }

    public interface IRpository
    {
        void SetDBContext(IMongoDatabase database);
    }
}
