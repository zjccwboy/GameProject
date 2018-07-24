using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace TestGetCurrentPort
{
    public class FreePort
    {
        /// <summary>
        /// 测试TCP端口是否可以用
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool TCPPortNoUsed(int port)
        {
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] endPoints = ipGlobalProperties.GetActiveTcpListeners();
            return endPoints.Where((p) => p.Port == port).Count() == 0;
        }

        /// <summary>
        /// 测试UDP端口是否可以用
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool UDPPortNoUsed(int port)
        {
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] endPoints = ipGlobalProperties.GetActiveUdpListeners();
            return endPoints.Where((p) => p.Port == port).Count() == 0;
        }
    }
}