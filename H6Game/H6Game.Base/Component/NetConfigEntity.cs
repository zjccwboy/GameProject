
namespace H6Game.Base.Entity
{
    public class SysConfig
    {
        public bool IsCenterServer { get; set; }
        public InNetConfigEntity InNetConfig { get; set; }
        public OutNetConfigEntity OuNetConfig { get; set; }
#if SERVER
        public DbConfigEntity DbConfig { get; set; }
#endif
    }

    public class InNetConfigEntity
    {
        public EndPointEntity CenterEndPoint { get; set; }
        public EndPointEntity LocalEndPoint { get; set; }
    }

    public class OutNetConfigEntity
    {
        public int Port { get; set; }
        public string Host { get; set; }
    }

    public class DbConfigEntity
    {
        public string ConnectionString { get; set; }
    }

    public class EndPointEntity
    {
        public int Port { get; set; }
        public string IP { get; set; }
        public string Desc { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public NetEndPointMessage DMessage
        {
            get
            {
                var message = new NetEndPointMessage
                {
                    Port = this.Port,
                    IP = this.IP,
                };
                return message;
            }
        }
    }
}