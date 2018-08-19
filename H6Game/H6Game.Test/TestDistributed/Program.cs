using System.Threading;
using H6Game.Base;

namespace TestDistributed
{
    class Program
    {
        static void Main(string[] args)
        {
            var netComponent = Game.Scene.AddComponent<InNetComponent>();
            var actorComponent = Game.Scene.AddComponent<ActorComponent>();
            var dbComponent = Game.Scene.AddComponent<MongoDBComponent>();
            var testComponent = Game.Scene.AddComponent<TestSender>();


            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
                Game.Scene.GetComponent<ActorComponent>().AddComponent<PlayerComponent>().LoadAccountInfo(11111);
            }
        }
    }
}
