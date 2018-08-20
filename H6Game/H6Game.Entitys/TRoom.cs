using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

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
        /// 游戏Id
        /// </summary>
        [BsonElement("GId")]
        public string FGameId { get; set; }

        /// <summary>
        /// 最多游戏圈数
        /// </summary>
        public int FMaxGame { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonElement("CT")]
        public DateTime FCreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [BsonElement("UT")]
        public DateTime FUpdateTime { get; set; }
    }
}
