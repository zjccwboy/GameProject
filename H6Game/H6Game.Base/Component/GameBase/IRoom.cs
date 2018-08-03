
namespace H6Game.Base
{
    public interface IRoom : ICompoentRoom
    {
        void AddPalyer(BasePlayerComponent player);

        void RemovePlayer(BasePlayerComponent player);

        void AddScene();

        void RemoveScene();

        void GameStart();

        void GameStop();
    }

    public interface ICompoentRoom
    {

    }
}
