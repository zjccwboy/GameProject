using H6Game.Base;
using H6Game.Hotfix.Enums;

namespace H6Game.TestActorMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<DistributionsComponent>();
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<ActorPoolComponent, ActorType>(ActorType.Player);
            Game.Scene.AddComponent<ActorPoolComponent, ActorType>(ActorType.Room);
            Game.Scene.AddComponent<ActorPoolComponent, ActorType>(ActorType.Game);

            TestAccountActor.Start();
            TestGameActor.Start();
            TestRoomActor.Start();

            while (true)
            {
                Game.Update();
            }
        }
    }
}
