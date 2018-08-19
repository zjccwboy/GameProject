using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Entitys
{
    [BsonIgnoreExtraElements]
    public class TAccount : BaseEntity
    {
        [BsonElement("FT")]
        public AccountType FType { get; set; }

        [BsonElement("FB")]
        public decimal FBalance { get; set; }


    }
}
