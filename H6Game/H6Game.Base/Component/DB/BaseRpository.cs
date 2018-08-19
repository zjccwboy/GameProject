
using H6Game.Entitys;
using MongoDB.Driver;

namespace H6Game.Base
{
    public abstract class BaseRpository<TDoc> : BaseComponent, IRpository<TDoc> where TDoc : BaseEntity
    {
        public IContext<TDoc> DBContext { get;private set; }

        public IMongoDatabase DataBase { get;private set; }

        public void SetDBContext(IMongoDatabase database)
        {
            this.DataBase = database;
            this.DBContext = new DBContext<TDoc>(this.DataBase);
        }

    }
}
