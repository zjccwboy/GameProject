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

            var timer = Game.Scene.AddComponent<TimerComponent>();
            timer.SetTimer(() =>
            {
                TestAccountActor.Start();
                TestGameActor.Start();
                TestRoomActor.Start();
            }, 3000);

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
