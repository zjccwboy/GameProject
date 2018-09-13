

using MongoDB.Bson.Serialization.Attributes;

namespace H6Game.Base
{
    public class DbConfig
    {
        [BsonElement]
        public string ConnectionString { get; set; }

        [BsonElement]
        public string DatabaseName { get; set; }
    }
}
