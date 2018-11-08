using H6Game.Actor;
using H6Game.Base;
using System.Threading;

namespace H6Game.TestActorMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<NetDistributionsComponent>();
            Game.Scene.AddComponent<ActorComponentStorage>();
            Game.Start();

            TestAccountActor.Start();
            TestGameActor.Start();
            TestRoomActor.Start();

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
