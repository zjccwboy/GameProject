﻿using H6Game.Actor;
using H6Game.Base;
using H6Game.Gate;
using System.Threading;

namespace H6Game.ServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new AppTypeConfig().Config;
            switch (config.AppType)
            {
                case ApplicationType.Default:
                    Game.Scene.AddComponent<MongoConfig>();
                    Game.Scene.AddComponent<NetDistributionsComponent>();
                    Game.Scene.AddComponent<ActorPoolComponent>();
                    break;
                case ApplicationType.CenterServer:
                    Game.Scene.AddComponent<MongoConfig>();
                    Game.Scene.AddComponent<NetDistributionsComponent>();
                    break;
                case ApplicationType.GateProxyServer:
                    Game.Scene.AddComponent<MongoConfig>();
                    Game.Scene.AddComponent<NetDistributionsComponent>();
                    Game.Scene.AddComponent<ActorPoolComponent>();
                    Game.Scene.AddComponent<ProxyComponent>();
                    Game.Scene.AddComponent<GateComponent>();
                    break;
                case ApplicationType.Benckmark:
                    Game.Scene.AddComponent<NetDistributionsComponent>().OnConnect += network => { TestBenckmark.Start(network); };
                    break;
            }

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}