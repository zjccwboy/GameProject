using System.Threading;
using H6Game.Base;

namespace TestDistributed
{
    class Program
    {
        static void Main(string[] args)
        {
            var netComponent = SinglePool.Get<InNetComponent>();
            var testComponent = SinglePool.Get<TestSender>();

            while (true)
            {
                netComponent.Update();
                testComponent.Update();
                Thread.Sleep(1);
            }
        }
    }
}
