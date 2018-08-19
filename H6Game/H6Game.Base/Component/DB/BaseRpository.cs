
using H6Game.Entitys;
using MongoDB.Driver;

namespace H6Game.Base
{
    public abstract class BaseRpository<TDoc> : BaseComponent, IRpository<TDoc> where TDoc : BaseEntity
    {
        public IContext<TDoc> DBContext { get; set; }

        public void SetDBContext(IMongoDatabase database)
        {
            DBContext = new DBContext<TDoc>(database);
        }

    }
}
