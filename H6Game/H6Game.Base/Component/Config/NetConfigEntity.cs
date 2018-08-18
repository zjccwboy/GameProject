
using H6Game.Message;
using MongoDB.Bson.Serialization.Attributes;

namespace H6Game.Base
{
    public class SysConfig
    {
        [BsonElement]
        public bool IsCenterServer { get; set; }

        [BsonElement]
        public string OuNetHost { get; set; }

        [BsonElement]
        public InNetConfigEntity InNetConfig { get; set; }

        [BsonElement]
        public EndPointEntity OuNetConfig { get; set; }

        [BsonElement]
        public bool IsCompress { get; set; }

        [BsonElement]
        public bool IsEncrypt { get; set; }
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
        [BsonElement]
        public DbConfigEntity DbConfig { get; set; }
#endif
    }

    public class InNetConfigEntity
    {
        [BsonElement]
        public EndPointEntity CenterEndPoint { get; set; }

        [BsonElement]
        public EndPointEntity LocalEndPoint { get; set; }
    }

    public class DbConfigEntity
    {
        [BsonElement]
        public string ConnectionString { get; set; }

        [BsonElement]
        public string DatabaseName { get; set; }
    }

    public class EndPointEntity
    {
        [BsonElement]
        public int Port { get; set; }

        [BsonElement]
        public string IP { get; set; }

        [BsonElement]
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