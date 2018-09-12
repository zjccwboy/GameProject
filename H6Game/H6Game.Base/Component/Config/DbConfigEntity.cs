

using MongoDB.Bson.Serialization.Attributes;

namespace H6Game.Base
{
    public class DbConfigEntity
    {
        [BsonElement]
        public string ConnectionString { get; set; }

        [BsonElement]
        public string DatabaseName { get; set; }
    }
}
