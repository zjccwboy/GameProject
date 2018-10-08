using H6Game.Actor;
using H6Game.Base;
using System.Threading;

namespace H6Game.TestDistributedNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<NetDistributionsComponent>().OnInnerClientConnected += network => { TestBenckmark.Start(network); };
            Game.Scene.AddComponent<ActorPoolComponent>();

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
