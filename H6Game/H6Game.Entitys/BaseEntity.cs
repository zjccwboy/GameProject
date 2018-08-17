using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Entitys
{
    [BsonIgnoreExtraElements]
    public abstract class BaseEntity
    {
        /// <summary>
        /// MongoDB Object _Id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Id")]
        public string Id { get; set; }
    }
}
