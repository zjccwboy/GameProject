﻿using H6Game.Base.Component;
using H6Game.Base.Message;
using MongoDB.Bson.Serialization.Attributes;

namespace H6Game.Base.Config
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
        public EndPointConfigEntity OuterKcpAcceptConfig { get; set; }

        public EndPointConfigEntity OuterTcpAcceptConfig { get; set; }

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
        [BsonIgnore]
        public string HttpPrefixed
        {
            get
            {
                return $"{this.HttpType}://{this.Host}:{this.Port}/";
            }
        }

        public string HttpType { get; set; }

        public override NetEndPointMessage GetMessage()
        {
            if (this.message == null)
                this.message = new NetEndPointMessage { Port = this.Port, Host = this.Host};
            return this.message;
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
        public string Host { get; set; }

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
                this.message = new NetEndPointMessage{ Port = this.Port, Host = this.Host };
            return this.message;
        }
    }

    public class NetConnectConfigEntity
    {
        /// <summary>
        /// 如果服务端使用分布式部署Host是代理服务的IP或者域名。
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 如果服务端使用分布式部署，Port需要设置成代理服务端口。
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 协议类型，1 TCP, 2 KCP 3 WCP
        /// </summary>
        public ProtocalType ProtocalType { get; set; }

        [BsonIgnore]
        public string HttpPrefixed
        {
            get
            {
                return $"{this.HttpType}://{this.Host}:{this.Port}/";
            }
        }

        public string HttpType { get; set; }

        /// <summary>
        /// 是否启用代理连接
        /// </summary>
        public bool ProxyEnable { get; set; }

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