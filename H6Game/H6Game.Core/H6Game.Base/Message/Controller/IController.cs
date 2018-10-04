using System.Threading.Tasks;

namespace H6Game.Base
{
    public interface IController
    {
        void Invoke(MetodContext context, Network network);
    }
}
