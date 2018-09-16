
using H6Game.Message;
using MongoDB.Bson.Serialization.Attributes;

namespace H6Game.Base
{
    public class DistributionsConfig
    {
        [BsonElement]
        public bool IsCenterServer { get; set; }

        [BsonElement]
        public bool IsCompress { get; set; }

        [BsonElement]
        public bool IsEncrypt { get; set; }

        [BsonElement]
        public InnerListenConfig InnerListenConfig { get; set; }

        [BsonElement]
        public EndPointConfig OutListenConfig { get; set; }

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

    public class InnerListenConfig
    {
        [BsonElement]
        public EndPointConfig CenterEndPoint { get; set; }

        [BsonElement]
        public EndPointConfig LocalEndPoint { get; set; }
    }

    public class EndPointConfig
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

    public class OutNetConfig
    {
        [BsonElement]
        public string OutNetHost { get; set; }

        [BsonElement]
        public int Port { get; set; }

        [BsonElement]
        public bool IsCompress { get; set; }

        [BsonElement]
        public bool IsEncrypt { get; set; }
    }
}