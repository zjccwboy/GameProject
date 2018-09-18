using System.Threading;
using H6Game.Base;
using H6Game.Hotfix.Enums;

namespace TestDistributed
{
    class Program
    {
        static void Main(string[] args)
        {

            Game.Scene.AddComponent<DBConfigComponent>();
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<LoggerConfigComponent>();
            Game.Scene.AddComponent<DistributionsConfigComponent>();
            Game.Scene.AddComponent<DistributionsComponent>().OnInnerClientConnected += c=> { TestBenckmark.Start(c.Network); };
            Game.Scene.AddComponent<ActorPoolComponent, ActorType>(ActorType.Player);
            Game.Scene.AddComponent<ActorPoolComponent, ActorType>(ActorType.Room);
            Game.Scene.AddComponent<ActorPoolComponent, ActorType>(ActorType.Game);

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
