

namespace H6Game.Base
{
    public class NetConfigEntity
    {
        /// <summary>
        /// 是否配置成代理服务，当被设置成true时即为代理服务，代理服务提供一个类似于DNS服务的功能，但代理服务实际
        /// 上也负责负载均衡的调度，调度算法会根据当前分布式服务节点中负载最少的节点优先分配给连接客户端。
        /// </summary>
        public bool IsProxyServer { get; set; }

        /// <summary>
        /// 数据传输是否启用压缩传输。
        /// </summary>
        public bool IsCompress { get; set; }

        /// <summary>
        /// 数据传输是否启用加密传输。
        /// </summary>
        public bool IsEncrypt { get; set; }

        /// <summary>
        /// 中心服务的配置信息，当IsCenterServer=true时，该配置的IP端口为监听IP端口，当IsCenterServer=false时
        /// 该配置的IP端口为远程中心服务的连接IP端口。
        /// </summary>
        public EndPointConfigEntity CenterAcceptConfig { get; set; }

        /// <summary>
        /// 本地服务监听IP端口配置信息。
        /// </summary>
        public EndPointConfigEntity LocalAcceptConfig { get; set; }

        /// <summary>
        /// 外网监听配置
        /// </summary>
        public OuterAcceptConfigEntity OuterAcceptConfig { get; set; }

        public NetEndPointMessage GetCenterMessage()
        {
            return this.CenterAcceptConfig.GetMessage();
        }

        public NetEndPointMessage GetLocalMessage()
        {
            return this.LocalAcceptConfig.GetMessage();
        }

        private OuterEndPointMessage OuterMessage;
        public OuterEndPointMessage GetOuterMessage()
        {
            if(this.OuterMessage == null)
                this.OuterMessage = this.OuterAcceptConfig.GetMessage();

            return this.OuterMessage;
        }
    }

    public class OuterAcceptConfigEntity
    {
        /// <summary>
        /// 监听外网KCP(UDP)连接IP端口配置信息，该配置只有在IsCenterServer=false时即非中心服务时才会被使用。
        /// 当IsProxyServer=true也即该服务为代理服务时客户端的连接配置ProxyHost Port必须指向此服务。
        /// </summary>
        public EndPointConfigEntity OuterKcpAcceptConfig { get; set; }

        /// <summary>
        /// 监听外网TCP连接IP端口配置信息，该配置只有在IsCenterServer=false时即非中心服务时才会被使用。
        /// 当IsProxyServer=true也即该服务为代理服务时客户端的连接配置ProxyHost Port必须指向此服务。
        /// </summary>
        public EndPointConfigEntity OuterTcpAcceptConfig { get; set; }

        /// <summary>
        /// 监听外网WebSocket连接IP端口配置信息，该配置只有在IsCenterServer=false时即非中心服务时才会被使用。
        /// 当IsProxyServer=true也即该服务为代理服务时客户端的连接配置ProxyHost Port必须指向此服务。
        /// </summary>
        public EndPointWebSocketConfigEntity OuterWebSocketConfig { get; set; }

        public OuterEndPointMessage GetMessage()
        {
            var result = new OuterEndPointMessage
            {
                KcpEndPointMessage = this.GetOuterKcpMessage(),
                TcpEndPointMessage = this.GetOuterTcpMessage(),
                WcpEndPointMessage = this.GetOuterWcpMessage(),
            };
            return result;
        }

        private NetEndPointMessage GetOuterKcpMessage()
        {
            return OuterKcpAcceptConfig.GetMessage();
        }

        private NetEndPointMessage GetOuterTcpMessage()
        {
            return OuterTcpAcceptConfig.GetMessage();
        }

        private NetEndPointMessage GetOuterWcpMessage()
        {
            return OuterWebSocketConfig.GetMessage();
        }
    }

    public class EndPointWebSocketConfigEntity : EndPointConfigEntity
    {
        public string HttpPrefixed { get; set; }

        public override NetEndPointMessage GetMessage()
        {
            if (this.message == null)
                this.message = new NetEndPointMessage { Port = this.Port, IP = this.IP , WsPrefixed = this.GetWsPrefied()};
            return this.message;
        }
        private string GetWsPrefied()
        {
            var result = string.Empty;
            if (this.HttpPrefixed.StartsWith("http"))
                result = this.HttpPrefixed.Replace("http", "ws");
            else if (this.HttpPrefixed.StartsWith("https"))
                result = this.HttpPrefixed.Replace("https", "wss");
            return result;
        }
    }

    public class EndPointConfigEntity
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 协议类型，1 TCP, 2 KCP 3 WCP
        /// </summary>
        public ProtocalType ProtocalType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        protected NetEndPointMessage message;
        public virtual NetEndPointMessage GetMessage()
        {
            if(this.message == null)
                this.message = new NetEndPointMessage{ Port = this.Port, IP = this.IP};
            return this.message;
        }
    }

    public class NetConnectConfigEntity
    {
        /// <summary>
        /// 如果服务端使用分布式部署ProxyHost是代理服务的IP或者域名。
        /// </summary>
        public string ProxyHost { get; set; }

        /// <summary>
        /// 如果服务端使用分布式部署，Port需要设置成代理服务端口。
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 协议类型，1 TCP, 2 KCP 3 WCP
        /// </summary>
        public ProtocalType ProtocalType { get; set; }

        /// <summary>
        /// 数据传输是否启用压缩传输。
        /// </summary>
        public bool IsCompress { get; set; }

        /// <summary>
        /// 数据传输是否启用加密传输。
        /// </summary>
        public bool IsEncrypt { get; set; }

        /// <summary>
        /// 配置项描述。
        /// </summary>
        public string Desc { get; set; }
    }
}