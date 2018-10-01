using H6Game.Base;
using System;
using System.Threading;

namespace H6Game.TestKcpServerBenckmarkCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<DistributionsComponent>();
            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
