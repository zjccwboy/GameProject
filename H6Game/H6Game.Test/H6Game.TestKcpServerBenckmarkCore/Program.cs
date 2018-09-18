﻿using H6Game.Base;
using System;
using System.Threading;

namespace H6Game.TestKcpServerBenckmarkCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<DBConfigComponent>();
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<LoggerConfigComponent>();
            Game.Scene.AddComponent<DistributionsConfigComponent>();
            Game.Scene.AddComponent<DistributionsComponent>();
            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}