using H6Game.Message;

namespace H6Game.Base
{
    public class NetEndPointMessage : IMessage
    {
        public int Port { get; set; }
        public string IP { get; set; }

        private int hashCode;
        public int HashCode()
        {
            if (hashCode == 0)
            {
                hashCode = $"{IP}:{Port}".GetHashCode();
            }
            return hashCode;
        }
    }
}
