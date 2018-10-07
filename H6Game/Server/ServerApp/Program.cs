using System.Threading;
using H6Game.Base;

namespace ServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<NetDistributionsComponent>();
            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
