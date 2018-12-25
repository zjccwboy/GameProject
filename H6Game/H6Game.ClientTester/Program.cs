using H6Game.Base;
using H6Game.Base.Component;
using System.Threading;

namespace H6Game.ClientTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var connector = Game.Scene.AddComponent<NetConnectorComponent>();
            connector.OnConnect += (n, t) => { TestBenckmark.Start(n);};
            Game.Start();

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
