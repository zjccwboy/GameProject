using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace H6Game.Entitys
{
    public class TRoom : BaseEntity
    {
        /// <summary>
        /// 房间类型
        /// </summary>
        [BsonElement("RT")]
        public RoomType FRoomType { get; set; }

        /// <summary>
        /// 房间游戏类型
        /// </summary>
        [BsonElement("GT")]
        public GameType FGameType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonElement("CT")]
        public DateTime FCreateTime { get; set; }
    }
}
