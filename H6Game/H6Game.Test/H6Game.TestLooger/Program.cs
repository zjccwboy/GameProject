using H6Game.Base;
using H6Game.Hotfix.Enums;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Threading;

namespace H6Game.TestLooger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test", ConsoleColor.Red);

            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<LoggerConfigComponent>();
            Game.Scene.AddComponent<DistributionsComponent>();
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
