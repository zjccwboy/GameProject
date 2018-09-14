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
    public class TestRoomActor
    {
        public static async void Start()
        {
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
            Game.Scene.AddComponent<RoomComponent>().Add(room);
        }
    }
}
