using H6Game.Base;
using H6Game.Hotfix.Enums;
using System.Threading;

namespace H6Game.TestDistributedNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<DistributionsComponent>().OnInnerClientConnected += c => { TestBenckmark.Start(c.Network); };
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
