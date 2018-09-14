using H6Game.Base;
using H6Game.Entities;
using H6Game.Entities.Enums;
using H6Game.Rpository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H6Game.TestActorMessage
{
    public class TestGameActor
    {
        public static async void Start()
        {
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
            Game.Scene.AddComponent<GameComponent>().Add(game);
        }
    }
}
