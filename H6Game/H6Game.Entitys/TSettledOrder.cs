using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace H6Game.Entitys
{
    public class TSettledOrder : BaseEntity
    {
        /// <summary>
        /// 未结算注单Id
        /// </summary>
        [BsonElement("UOId")]
        public string FUnsettledOrderId { get; set; }

        /// <summary>
        /// 游戏Id
        /// </summary>
        [BsonElement("GId")]
        public string FGameId { get; set; }

        /// <summary>
        /// 下注主单号集合
        /// </summary>
        [BsonElement("BIds")]
        public List<string> FBetIds { get; set; }

        /// <summary>
        /// 下注账号Id集合
        /// </summary>
        [BsonElement("AIds")]
        public List<string> FBetAccountIds { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonElement("CT")]
        public DateTime FCreateTime { get; set; }
    }
}
