using H6Game.Base;
using System.Threading;

namespace H6Game.ClientTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var connector = Game.Scene.AddComponent<NetConnectorComponent>();
            connector.OnConnect += (n, t) => 
            {
                if (t == ConnectType.Proxy)
                    connector.ConnectingGate();
                else if(t == ConnectType.Gate)
                    TestBenckmark.Start(n);
            };

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
