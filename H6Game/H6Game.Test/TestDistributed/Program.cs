using System.Threading;
using H6Game.Base;

namespace TestDistributed
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventCpt = SinglePool.Get<EventComponent>();
            var test = SinglePool.Get<TestSender>();
            while (true)
            {
                eventCpt.Update();
                Thread.Sleep(1);
            }
        }
    }
}
