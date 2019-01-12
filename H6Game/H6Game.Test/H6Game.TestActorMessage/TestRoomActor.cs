using H6Game.Actor;
using H6Game.Base;
using H6Game.Base.Component;
using H6Game.Hotfix.Entities;
using H6Game.Rpository;
using System.Threading.Tasks;
using System.Threading;

namespace H6Game.TestActorMessage
{
    public class TestRoomActor
    {
        public static async void Start()
        {
            if (Game.Scene.GetComponent<NetDistributionsComponent>().IsCenterServer)
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
            Game.Scene.GetComponent<ActorComponentStorage>().AddActor<RoomComponent, TRoom>(room);
        }
    }
}
