using System.Threading;
using H6Game.Account.Model;
using H6Game.Base;

namespace ServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var netComponent = Game.Scene.AddComponent<InNetComponent>();
            var actorComponent = Game.Scene.AddComponent<ActorComponent>();
            var dbComponent = Game.Scene.AddComponent<MongoDBComponent>();
            var accountManager = Game.Scene.AddComponent<AccountManagerComponent>();

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
