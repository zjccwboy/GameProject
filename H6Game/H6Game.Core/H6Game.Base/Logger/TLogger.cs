﻿using MongoDB.Bson.Serialization.Attributes;

namespace H6Game.Base.Logger
{
    public class TLogger : BaseEntity
    {
        [BsonElement("LL")]
        public LogLevel FLogLevel { get; set; }

        [BsonElement("BT")]
        public LoggerBllType FBllType { get; set; }

        [BsonElement("SI")]
        public string FStackInfo { get; set; }

        [BsonElement("MSG")]
        public string FMessage { get; set; }

        [BsonElement("EN")]
        public string FExceptionName { get; set; }

        [BsonElement("EM")]
        public string FExceptionMessage { get; set; }

        [BsonElement("EI")]
        public string FExceptionInfo { get; set; }

        [BsonElement("AG")]
        public string FArgs { get; set; }
    }
}