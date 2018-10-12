using ProtoBuf;

namespace H6Game.Base
{
    /// <summary>
    /// 外网连接信息
    /// </summary>
    [ProtoContract]
    [NetMessageType(SysNetMessageType.OuterEndPointMessage)]
    public class OuterEndPointMessage : IMessage
    {
        [ProtoMember(1)]
        public NetEndPointMessage KcpEndPointMessage { get; set; }

        [ProtoMember(2)]
        public NetEndPointMessage TcpEndPointMessage { get; set; }

        [ProtoMember(3)]
        public NetEndPointMessage WcpEndPointMessage { get; set; }

        public static bool operator ==(OuterEndPointMessage left, OuterEndPointMessage right)
        {
            if ((right as object) == null)
                return ((right as object) == null);

            return left.GetHashCode() == right.GetHashCode();
        }

        public static bool operator !=(OuterEndPointMessage left, OuterEndPointMessage right)
        {
            if ((right as object) == null)
                return ((right as object) != null);

            return !(left.GetHashCode() == right.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return this.GetHashCode() == obj.GetHashCode();
        }

        private int HashCode;
        public override int GetHashCode()
        {
            if (this.HashCode != 0)
                return this.HashCode;

            var kcpHashCode = this.KcpEndPointMessage == null ? 0: this.KcpEndPointMessage.GetHashCode();
            var tcpHashCode = this.TcpEndPointMessage == null ? 0 : this.TcpEndPointMessage.GetHashCode();
            var wcpHashCode = this.WcpEndPointMessage == null ? 0 : this.WcpEndPointMessage.GetHashCode();
            this.HashCode = $"{kcpHashCode}:{tcpHashCode}:{wcpHashCode}".GetHashCode();
            return this.HashCode;
        }
    }

    /// <summary>
    /// 网络连接信息消息
    /// </summary>
    [ProtoContract]
    [NetMessageType(SysNetMessageType.NetEndPointMessage)]
    public class NetEndPointMessage : IMessage
    {
        [ProtoMember(1)]
        public int Port { get; set; }

        [ProtoMember(2)]
        public string IP { get; set; }

        [ProtoMember(3, IsRequired = true)]
        public string WsPrefixed { get; set; }

        public static bool operator ==(NetEndPointMessage left, NetEndPointMessage right)
        {
            if ((right as object) == null)
                return ((right as object) == null);

            return left.GetHashCode() == right.GetHashCode();
        }

        public static bool operator !=(NetEndPointMessage left, NetEndPointMessage right)
        {
            if ((right as object) == null)
                return ((right as object) != null);

            return !(left.GetHashCode() == right.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return this.GetHashCode() == obj.GetHashCode();
        }

        private int HashCode;
        public override int GetHashCode()
        {
            if (this.HashCode != 0)
                return this.HashCode;

            this.HashCode = $"{this.IP}:{this.Port}:{this.WsPrefixed}".GetHashCode();
            return this.HashCode;
        }
    }
}
