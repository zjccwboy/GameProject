using H6Game.Base;
using System.Threading;
using H6Game.Gate;
using H6Game.Actor;

namespace H6Game.TestDistributedNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<NetDistributionsComponent>().OnConnect += network => { /*TestBenckmark.Start(network);*/ };
            Game.Scene.AddComponent<ActorPoolComponent>();
            Game.Scene.AddComponent<GateComponent>();
            Game.Scene.AddComponent<ProxyComponent>();

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
