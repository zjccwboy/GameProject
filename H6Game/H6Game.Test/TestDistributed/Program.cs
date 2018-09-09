using System;
using System.Threading;
using H6Game.Base;
using H6Game.Entitys;
using H6Game.Entitys.Enums;
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
