using System.Threading;
using H6Game.Base;
using H6Game.Hotfix.Enums;

namespace TestDistributed
{
    class Program
    {
        static void Main(string[] args)
        {

            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<DistributionsComponent>().OnInnerClientConnected += c=> { TestBenckmark.Start(c.Network); };
            Game.Scene.AddComponent<ActorPoolComponent>();

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
