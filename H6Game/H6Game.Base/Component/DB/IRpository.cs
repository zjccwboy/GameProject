using H6Game.Entitys;

namespace H6Game.Base
{
    public interface IRpository<TDoc> : IRpository where TDoc : BaseEntity
    {

    }

    public interface IRpository
    {
        IContext DBContext { get; set; }
    }
}
