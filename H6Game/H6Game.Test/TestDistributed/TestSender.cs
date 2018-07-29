using H6Game.Base;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDistributed
{
    public class TestSender : BaseComponent
    {
        private uint time = TimeUitls.Now();

        public void Update()
        {
            Broadcast();
        }

        private void Broadcast()
        {
            if(TimeUitls.Now() - time >= 1000)
            {
                var inNetComponent = SinglePool.Get<InNetComponent>();
                inNetComponent.BroadcastMessage(Encoding.UTF8.GetBytes("Test"), (int)MessageCMD.TestCMD1);
                time = TimeUitls.Now();
            }
        }
    }
}
