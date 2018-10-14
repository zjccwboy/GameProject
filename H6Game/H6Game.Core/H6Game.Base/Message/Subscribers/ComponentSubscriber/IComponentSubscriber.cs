
namespace H6Game.Base
{
    public interface IComponentSubscriber
    {
        void Invoke(MethodContext context, Network network);
    }
}
