using H6Game.Base;
using System.Threading;

namespace H6Game.TestKcpServerBenckmark
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<LoggerConfigComponent>();
            Game.Scene.AddComponent<DistributionsComponent>();
            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
