﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace H6Game.Entitys
{
    public class TUnsettledOrder : BaseEntity
    {
        /// <summary>
        /// 下注主单号
        /// </summary>
        [BsonElement("BIds")]
        public List<string> FBetIds { get; set; }

        /// <summary>
        /// 是否已经结算
        /// </summary>
        [BsonElement("")]
        public bool IsUnsettled { get; set; }

        /// <summary>
        /// 房间Id
        /// </summary>
        [BsonElement("RId")]
        public string FRoomId { get; set; }

        /// <summary>
        /// 游戏Id
        /// </summary>
        [BsonElement("GId")]
        public string FGameId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonElement("CT")]
        public DateTime FCreateTime { get; set; }
    }
}