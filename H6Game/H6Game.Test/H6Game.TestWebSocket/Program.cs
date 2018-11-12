using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using H6Game.Base;

namespace H6Game.TestWebSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            //Game.Scene.AddComponent<MongoConfig>();

            var server = Network.CreateWebSocketAcceptor("http://127.0.0.1:8067/");
            var client = Network.CreateWebSocketConnector("ws://127.0.0.1:8067/", n=> {
                n.Send(1000);
            },network=>
            {


            });

            try
            {
                Game.Start();
            }
            catch{}

            while (true)
            {
                Game.Update();
                server.Session.Update();
                client.Session.Update();
                Thread.Sleep(1);
            }
        }
    }

    [NetCommand(1000)]
    public class Sub1000 : NetSubscriber
    {
        protected override void Subscribe(Network network, int netCommand)
        {
            network.Send(netCommand);
        }
    }
}
