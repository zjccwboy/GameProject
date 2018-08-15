using System.Threading;
using H6Game.Base;

namespace TestDistributed
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<InNetComponent>();
            Game.Scene.AddComponent<TestSender>();
            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
