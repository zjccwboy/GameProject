using H6Game.Message;

namespace H6Game.Base
{
    public class NetEndPointMessage : IMessage
    {
        public int Port { get; set; }
        public string IP { get; set; }
        
        public int HashCode
        {
            get
            {
                var hashCode = $"{IP}:{Port}".GetHashCode();
                return hashCode;
            }
        }
    }
}
