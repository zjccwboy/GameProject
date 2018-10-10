
namespace H6Game.Base
{
    public interface IComponentSubscriber
    {
        void Invoke(MetodContext context, Network network);
    }
}
