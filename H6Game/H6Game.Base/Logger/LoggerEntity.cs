using H6Game.Hotfix.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace H6Game.Base
{
    public class LoggerEntity : BaseEntity
    {
        [BsonElement("Lv")]
        public LogLevel FLogLevel { get; set; }

        [BsonElement("Msg")]
        public string FMessage { get; set; }

        [BsonElement("EN")]
        public string FExceptionName { get; set; }

        [BsonElement("EI")]
        public string FExceptionInfo { get; set; }

        [BsonElement("A")]
        public string FArgs { get; set; }
    }
}