using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
