using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H6Game.Base;

namespace H6Game.TestWebSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = Network.CreateWebSocketAcceptor("http://127.0.0.1:8067/");
            var client = Network.CreateWebSocketConnector("http://127.0.0.1:8067/");
        }
    }
}
