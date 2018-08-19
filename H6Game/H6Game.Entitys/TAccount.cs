using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Entitys
{
    [BsonIgnoreExtraElements]
    public class TAccount : BaseEntity
    {
        /// <summary>
        /// 账户类型
        /// </summary>
        [BsonElement("T")]
        public AccountType FType { get; set; }

        /// <summary>
        /// VIP等级
        /// </summary>
        [BsonElement("VL")]
        public VIPLevel FVIPLevel { get; set; }

        /// <summary>
        /// 微信登陆OpenId
        /// </summary>
        [BsonElement("OId")]
        public string FWXOpenId { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [BsonElement("PN")]
        public string FPhoneNumber { get; set; }

        /// <summary>
        /// 账户密码
        /// </summary>
        [BsonElement("PW")]
        public string FPassword { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        [BsonElement("Blc")]
        public decimal FBalance { get; set; }

        /// <summary>
        /// 累积充值
        /// </summary>
        [BsonElement("AR")]
        public decimal FAccumulativeRecharge { get; set; }

        /// <summary>
        /// 累积消费
        /// </summary>
        [BsonElement("CC")]
        public decimal FCumulativeConsumption { get; set; }

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
