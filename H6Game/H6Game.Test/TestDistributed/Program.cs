using System.Threading;
using H6Game.Base;

namespace TestDistributed
{
    class Program
    {
        static void Main(string[] args)
        {
            var netComponent = SinglePool.Get<InNetComponent>();

            while (true)
            {
                netComponent.Update();
                Thread.Sleep(1);
            }
        }
    }
}
