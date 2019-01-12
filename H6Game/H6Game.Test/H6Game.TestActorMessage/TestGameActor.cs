using H6Game.Actor;
using H6Game.Base;
using H6Game.Base.Component;
using H6Game.Hotfix.Entities;
using H6Game.Rpository;
using System.Threading;
using System.Threading.Tasks;

namespace H6Game.TestActorMessage
{
    public class TestGameActor
    {
        public static async void Start()
        {
            if (Game.Scene.GetComponent<NetDistributionsComponent>().IsCenterServer)
                return;

            AddActor(await Add());
        }

        public async static Task<TGame> Add()
        {
            var rpository = Game.Scene.GetComponent<GameRpository>();
            var game = new TGame();
            game.SetCreator("Admin");
            game.SetUpdater("Admin");
            await rpository.DBContext.InsertAsync(game);
            return game;
        }

        public static void AddActor(TGame game)
        {
            Game.Scene.GetComponent<ActorComponentStorage>().AddActor<GameComponent, TGame>(game);
        }
    }
}
