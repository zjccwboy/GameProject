﻿using MongoDB.Bson.Serialization.Attributes;
using System;

namespace H6Game.Entitys
{
    public class TBetInfo : BaseEntity
    {
        /// <summary>
        /// 游戏类型
        /// </summary>
        [BsonElement("GId")]
        public string FGameId { get; set; }

        /// <summary>
        /// 房间类型
        /// </summary>
        [BsonElement("RId")]
        public string FRoomId { get; set; }

        /// <summary>
        /// 账户ID
        /// </summary>
        [BsonElement("AId")]
        public string FAccountId { get; set; }

        /// <summary>
        /// 投注金额
        /// </summary>
        [BsonElement("Amt")]
        public decimal FAmt { get; set; }

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
