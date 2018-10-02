using H6Game.Base;
using H6Game.Rpository;
using System.Threading.Tasks;

namespace H6Game.TestActorMessage
{
    public class TestRoomActor
    {
        public static async void Start()
        {
            if (Game.Scene.GetComponent<DistributionsComponent>().IsCenterServer)
                return;

            AddActor(await Add());
        }

        public async static Task<TRoom> Add()
        {
            var rpository = Game.Scene.GetComponent<RoomRpository>();
            var room = new TRoom();
            room.SetCreator("Admin");
            room.SetUpdater("Admin");
            await rpository.DBContext.InsertAsync(room);
            return room;
        }

        public static void AddActor(TRoom room)
        {
            var actor = Game.Scene.AddComponent<RoomComponent>();
            actor.SetLocal(room);
            Game.Scene.GetComponent<ActorPoolComponent>().AddLocal(actor);
        }
    }
}
