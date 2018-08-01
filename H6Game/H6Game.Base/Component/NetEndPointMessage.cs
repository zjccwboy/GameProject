using H6Game.Message;
using ProtoBuf;

namespace H6Game.Base
{
    [ProtoContract]
    public class NetEndPointMessage : IMessage
    {
        [ProtoMember(1)]
        public int Port { get; set; }

        [ProtoMember(2)]
        public string IP { get; set; }

        //重载==运算符
        public static bool operator ==(NetEndPointMessage left, NetEndPointMessage right)
        {
            if ((right as object) == null)
                return ((right as object) == null);

            return left.GetHashCode() == right.GetHashCode();
        }

        //重载!=运算符
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

        public override int GetHashCode()
        {
            var hashCode = $"{IP}:{Port}".GetHashCode();
            return hashCode;
        }
    }
}
