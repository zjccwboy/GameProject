using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Entitys
{
    public class TRoom : BaseEntity
    {
        [BsonElement("RT")]
        public RoomType FRoomType { get; set; }

        [BsonElement("GId")]
        public string FGameId { get; set; }



        [BsonElement("CT")]
        public DateTime FCreateTime { get; set; }

        [BsonElement("UT")]
        public DateTime FUpdateTime { get; set; }
    }
}
