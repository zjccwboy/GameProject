
namespace H6Game.Base.Entity
{
    public class SysConfig
    {
        public bool IsCenterServer { get; set; }
        public string OuNetHost { get; set; }
        public InNetConfigEntity InNetConfig { get; set; }
        public EndPointEntity OuNetConfig { get; set; }

        public NetEndPointMessage GetCenterMessage()
        {
            return this.InNetConfig.CenterEndPoint.GetMessage();
        }

        public NetEndPointMessage GetOutMessage()
        {
            return OuNetConfig.GetMessage();
        }

        public NetEndPointMessage GetInMessage()
        {
            return this.InNetConfig.LocalEndPoint.GetMessage();
        }

#if SERVER
        public DbConfigEntity DbConfig { get; set; }
#endif
    }

    public class InNetConfigEntity
    {
        public EndPointEntity CenterEndPoint { get; set; }
        public EndPointEntity LocalEndPoint { get; set; }
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

        private NetEndPointMessage message;
        public NetEndPointMessage GetMessage()
        {
            if(this.message == null)
                this.message = new NetEndPointMessage{ Port = this.Port, IP = this.IP};
            return this.message;
        }
    }
}