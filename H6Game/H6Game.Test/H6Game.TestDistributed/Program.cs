using System;
using System.Threading;
using H6Game.Base;
using H6Game.Entities;
using H6Game.Entities.Enums;
using H6Game.Rpository;

namespace TestDistributed
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<InnerComponent>();
            Game.Scene.AddComponent<ActorComponent, ActorType>(ActorType.Player);
            Game.Scene.AddComponent<ActorComponent, ActorType>(ActorType.Room);
            Game.Scene.AddComponent<ActorComponent, ActorType>(ActorType.Game);

            TestBenckmark.Start();

            while (true)
            {
                Game.Update();
            }
        }
    }
}
