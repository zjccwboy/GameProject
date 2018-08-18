
namespace H6Game.Base
{
    public abstract class BaseRpository : BaseComponent, IRpository
    {
        public IContext DBContext { get; set; }
    }
}
