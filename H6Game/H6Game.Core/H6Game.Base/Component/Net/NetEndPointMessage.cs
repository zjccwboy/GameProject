using H6Game.Base.Message;
using ProtoBuf;

namespace H6Game.Base.Component
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
            if ((left as object) == null)
                return ((right as object) == null);

            if ((right as object) == null)
                return ((left as object) == null);

            return left.GetHashCode() == right.GetHashCode();
        }

        public static bool operator !=(OuterEndPointMessage left, OuterEndPointMessage right)
        {
            if ((left as object) == null)
                return ((right as object) != null);

            if ((right as object) == null)
                return ((left as object) != null);

            return !(left.GetHashCode() == right.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return this.GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            var kcpHashCode = this.KcpEndPointMessage == null ? 0: this.KcpEndPointMessage.GetHashCode();
            var tcpHashCode = this.TcpEndPointMessage == null ? 0 : this.TcpEndPointMessage.GetHashCode();
            var wcpHashCode = this.WcpEndPointMessage == null ? 0 : this.WcpEndPointMessage.GetHashCode();
            return $"{kcpHashCode}:{tcpHashCode}:{wcpHashCode}".GetHashCode();
        }
    }

    /// <summary>
    /// 网络连接信息消息
    /// </summary>
    [ProtoContract]
    [NetMessageType(SysNetMessageType.InnerEndPointMessage)]
    public class NetEndPointMessage : IMessage
    {
        [ProtoMember(1)]
        public int Port { get; set; }

        [ProtoMember(2)]
        public string Host { get; set; }

        public static bool operator ==(NetEndPointMessage left, NetEndPointMessage right)
        {
            if ((left as object) == null)
                return ((right as object) == null);

            if ((right as object) == null)
                return ((left as object) == null);

            return left.GetHashCode() == right.GetHashCode();
        }

        public static bool operator !=(NetEndPointMessage left, NetEndPointMessage right)
        {
            if ((left as object) == null)
                return ((right as object) != null);

            if ((right as object) == null)
                return ((left as object) != null);

            return !(left.GetHashCode() == right.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return this.GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return $"{this.Host}:{this.Port}".GetHashCode();
        }
    }
}
