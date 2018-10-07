using H6Game.Base;
using System.Threading;

namespace H6Game.TestKcpServerBenckmark
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<NetDistributionsComponent>();
            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
