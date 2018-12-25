using H6Game.Base.Config;
using H6Game.Base.Exceptions;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace H6Game.Base.Component
{
    public class IPEndPointHelper
    {
        public static IPEndPoint GetIPEndPoint(NetEndPointMessage message)
        {
            var ip = IPAddress.Parse(message.IP);
            var port = message.Port;
            return new IPEndPoint(ip, port);
        }
        
        public static IPEndPoint GetIPEndPoint(NetConnectConfigEntity config)
        {
            const string ipOrDomainRegex = @"^(?=^.{3,255}$)[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+$";
            const string ipRegex = @"((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)";

            var regex = new Regex(ipOrDomainRegex);
            var isMatch = regex.IsMatch(config.Host);
            if (!isMatch)
                throw new ComponentException("连接服务端主机IP地址或者域名配置错误，请检查OutNetConfig.json文件配置。");

            regex = new Regex(ipRegex);
            var isIpadress = regex.IsMatch(config.Host);
            IPAddress ipAddress = null;
            if (isIpadress)
            {
                ipAddress = IPAddress.Parse(config.Host);
            }
            else
            {
                var hostInfo = Dns.GetHostEntry(config.Host);
                ipAddress = hostInfo.AddressList[0];
            }
            var endPoint = new IPEndPoint(ipAddress, config.Port);
            return endPoint;
        }
    }
}
