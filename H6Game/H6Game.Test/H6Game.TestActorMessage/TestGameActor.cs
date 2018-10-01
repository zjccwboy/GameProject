using H6Game.Base;
using H6Game.Hotfix.Entities;
using H6Game.Rpository;
using System.Threading.Tasks;

namespace H6Game.TestActorMessage
{
    public class TestGameActor
    {
        public static async void Start()
        {
            if (Game.Scene.GetComponent<DistributionsComponent>().IsCenterServer)
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
            var actor = Game.Scene.AddComponent<GameComponent>();
            actor.SetLocal(game);
            Game.Scene.GetComponent<ActorPoolComponent>().AddLocal(actor);
        }
    }
}
