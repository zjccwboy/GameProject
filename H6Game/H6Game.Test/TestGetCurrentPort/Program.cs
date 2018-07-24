using System;

namespace TestGetCurrentPort
{
    class Program
    {
        static void Main(string[] args)
        {
            var tcpResult = FreePort.TCPPortNoUsed(4550);

            var udpResult = FreePort.UDPPortNoUsed(4550);
        }
    }
}
