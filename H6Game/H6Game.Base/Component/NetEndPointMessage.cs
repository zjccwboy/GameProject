using H6Game.Message;

namespace H6Game.Base
{
    public class NetEndPointMessage : IMessage
    {
        public int Port { get; set; }
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
            return this.GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            var hashCode = $"{IP}:{Port}".GetHashCode();
            return hashCode;
        }
    }
}
