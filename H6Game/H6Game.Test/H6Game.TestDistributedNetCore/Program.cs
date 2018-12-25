using H6Game.Base;
using System.Threading;
using H6Game.Gate;
using H6Game.Actor;
using H6Game.Base.Component;

namespace H6Game.TestDistributedNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<NetDistributionsComponent>().OnConnect += network => { TestBenckmark.Start(network);};
            Game.Scene.AddComponent<ActorComponentStorage>();
            Game.Scene.AddComponent<GateComponent>();
            Game.Scene.AddComponent<ProxyComponent>();
            Game.Start();

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
