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
            Game.Init();

            TestBenckmark.Start();

            while (true)
            {
                Game.Update();
            }
        }
    }
}
