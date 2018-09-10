using H6Game.Entities.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace H6Game.Entities
{
    public class TGame : BaseEntity
    {
        /// <summary>
        /// 游戏类型
        /// </summary>
        [BsonElement("GT")]
        public GameType FGameType { get; set; }

        /// <summary>
        /// 房间Id
        /// </summary>
        [BsonElement("RId")]
        public string FRoomId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonElement("CT")]
        public DateTime FCreateTime { get; set; }
    }
}
