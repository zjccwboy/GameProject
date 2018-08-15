using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using H6Game.Base;
using H6Game.Message;

namespace TestGServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<InNetComponent>();
            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
