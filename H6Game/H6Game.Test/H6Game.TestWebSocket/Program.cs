using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using H6Game.Base;
using H6Game.Base.Message;

namespace H6Game.TestWebSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = Network.CreateWebSocketAcceptor("http://127.0.0.1:8067/");
            var client = Network.CreateWebSocketConnector("ws://127.0.0.1:8067/"
                , n=>
                {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    for (var i=0;i<1000;i++)
                        TestCall(n, stopwatch);

                }, n=>{});

            Game.Start();
            while (true)
            {
                client.Session.Update();
                server.Session.Update();
                Thread.Sleep(1);
            }
        }

        static int sender = 0;
        static async void TestCall(Network network, Stopwatch stopwatch)
        {
            for (int i = 1; i < 1000000000; i++)
            {
                await Call(network, ++sender, stopwatch);
            }
        }

        static async Task Call(Network network,int data, Stopwatch stopwatch)
        {
            var result = await network.CallMessageAsync<int, int>(data, 1000);
            if (result % 1000 == 0)
            {
                Console.WriteLine($"{1000}条RPS 耗时:{stopwatch.ElapsedMilliseconds}毫秒.");
                stopwatch.Restart();
            }
        }
    }

    [NetCommand(1000)]
    public class Sub1000 : NetSubscriber<int>
    {
        protected override void Subscribe(Network network, int message, ushort netCommand)
        {
            var tid = Thread.CurrentThread.ManagedThreadId;
            try
            {
                network.Response(message);
            }
            catch(Exception e)
            {
                Console.WriteLine($"ManagedThreadId:{tid} {e.ToString()}");
            }
        }
    }
}
