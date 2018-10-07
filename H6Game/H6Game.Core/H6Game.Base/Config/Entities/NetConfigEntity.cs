

namespace H6Game.Base
{
    public class NetConfigEntity
    {
        public class InnerListenConfigEntity
        {
            public EndPointConfigEntity CenterEndPoint { get; set; }

            public EndPointConfigEntity LocalEndPoint { get; set; }
        }

        public bool IsCenterServer { get; set; }

        public bool IsCompress { get; set; }

        public bool IsEncrypt { get; set; }

        public InnerListenConfigEntity InnerListenConfig { get; set; }

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
        public int Port { get; set; }

        public string IP { get; set; }

        /// <summary>
        /// 协议类型，0 TCP, 1 KCP
        /// </summary>
        public int ProtocalType { get; set; }

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
        public string OutNetHost { get; set; }

        public int Port { get; set; }

        /// <summary>
        /// 协议类型，0 TCP, 1 KCP
        /// </summary>
        public int ProtocalType { get; set; }

        public bool IsCompress { get; set; }

        public bool IsEncrypt { get; set; }

        public string Desc { get; set; }
    }
}