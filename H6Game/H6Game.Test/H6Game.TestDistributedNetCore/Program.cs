﻿using H6Game.Base;
using System.Threading;

namespace H6Game.TestDistributedNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<NetDistributionsComponent>().OnConnect += network => { TestBenckmark.Start(network); };

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
