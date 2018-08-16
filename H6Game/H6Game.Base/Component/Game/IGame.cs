
using H6Game.Entitys;

namespace H6Game.Base
{
    public interface IGame : IComponentGame
    {
        GameType GType { get; set; }
        void Start();
        void Stop();
    }

    public interface IComponentGame
    {

    }
}
