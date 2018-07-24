using H6Game.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestTcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var endPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 8989);
            var session = new Session(endPoint, ProtocalType.Tcp);
            session.Accept();
            while (true)
            {
                session.Update();
                Thread.Sleep(1);
            }
        }
    }
}
