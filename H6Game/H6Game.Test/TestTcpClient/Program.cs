using H6Game.Base;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestTcpClient
{

    class Program
    {

        static void Main(string[] args)
        {
            var endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8989);
            var network = Network.CreateConnecting(endPoint, ProtocalType.Tcp);
            stopwatch.Start();
            while (true)
            {
                Subscribe(network);
                network.Update();
                Thread.Sleep(1);
            }
        }

        static Stopwatch stopwatch = new Stopwatch();
        static int sendCount = 0;
        static int recvCount = 0;
        static void Subscribe(Network network)
        {
            //优先处理完接收的包
            if (sendCount - recvCount > 0)
            {
                if (stopwatch.ElapsedMilliseconds > 5000)
                {
                    sendCount = 0;
                    recvCount = 0;
                    stopwatch.Restart();
                }
                return;
            }

            if (!network.Channel.Connected)
            {
                return;
            }

            var send = "111111111122222222223333333333444444444455555555556666666666777777777788888888889999999999";
            for (var i = 1; i <= 1000; i++)
            {
                sendCount++;
                network.CallRpc(send, (packet) =>
                {
                    recvCount++;
                    var data = packet.Read<string>();
                    if (data != send)
                    {
                        Console.WriteLine($"解包出错:{data}");                    }
                    if (recvCount % 1000 == 0)
                    {
                        LogRecord.Log(LogLevel.Info, "数据响应测试", $"响应:{1000}个包耗时{stopwatch.ElapsedMilliseconds}毫秒");
                        Thread.Sleep(1000);
                        stopwatch.Restart();
                    }
                }, (int)MessageCMD.TestCMD);
            }
        }
    }
}
