using H6Game.Actor;
using H6Game.Base;
using H6Game.Base.Component;
using H6Game.Base.Config;
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
                    Game.Scene.AddComponent<ActorComponentStorage>();
                    break;
                case ApplicationType.CenterServer:
                    Game.Scene.AddComponent<MongoConfig>();
                    Game.Scene.AddComponent<NetDistributionsComponent>();
                    break;
                case ApplicationType.GateProxyServer:
                    Game.Scene.AddComponent<MongoConfig>();
                    Game.Scene.AddComponent<NetDistributionsComponent>();
                    Game.Scene.AddComponent<ActorComponentStorage>();
                    Game.Scene.AddComponent<ProxyComponent>();
                    Game.Scene.AddComponent<GateComponent>();
                    break;
                case ApplicationType.Benckmark:
                    Game.Scene.AddComponent<NetDistributionsComponent>().OnConnected += network => { TestBenckmark.Start(network); };
                    break;
            }
            Game.Start();

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
