

namespace H6Game.Base
{
    public class NetConfigEntity
    {
        public class InnerListenConfigEntity
        {
            /// <summary>
            /// 中心服务的配置信息，当IsCenterServer=true时，该配置的IP端口为监听IP端口，当IsCenterServer=false时
            /// 该配置的IP端口为远程中心服务的连接IP端口。
            /// </summary>
            public EndPointConfigEntity CenterEndPoint { get; set; }

            /// <summary>
            /// 本地服务监听IP端口配置信息。
            /// </summary>
            public EndPointConfigEntity LocalEndPoint { get; set; }
        }

        /// <summary>
        /// 是否配置成分布式中心服务，分布式系统只能配置一个中心服务，当被设置成ture时即为中心服务，那么当前该
        /// 服务处理处理分布式系统服务之间的连接，不参与其他任何的业务处理。
        /// </summary>
        public bool IsCenterServer { get; set; }

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
        /// 服务监听IP端口配置信息。
        /// </summary>
        public InnerListenConfigEntity InnerListenConfig { get; set; }

        /// <summary>
        /// 监听外网连接IP端口配置信息，该配置只有在IsCenterServer=false时即非中心服务时才会被使用。
        /// 当IsProxyServer=true也即该服务为代理服务时客户端的连接配置ProxyHost Port必须指向此服务。
        /// </summary>
        public EndPointConfigEntity OutListenConfig { get; set; }

        public NetEndPointMessage GetCenterMessage()
        {
            return this.InnerListenConfig.CenterEndPoint.GetMessage();
        }

        public NetEndPointMessage GetOutMessage()
        {
            return OutListenConfig.GetMessage();
        }

        public NetEndPointMessage GetInnerMessage()
        {
            return this.InnerListenConfig.LocalEndPoint.GetMessage();
        }
    }

    public class EndPointConfigEntity
    {
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 协议类型，0 TCP, 1 KCP
        /// </summary>
        public int ProtocalType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        private NetEndPointMessage message;
        public NetEndPointMessage GetMessage()
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
        /// 协议类型，0 TCP, 1 KCP
        /// </summary>
        public int ProtocalType { get; set; }

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