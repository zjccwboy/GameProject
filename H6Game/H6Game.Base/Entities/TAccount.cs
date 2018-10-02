using MongoDB.Bson.Serialization.Attributes;

namespace H6Game.Base
{
    [BsonIgnoreExtraElements]
    public class TAccount : BaseEntity
    {
        /// <summary>
        /// 账户类型
        /// </summary>
        [BsonElement("AT")]
        public AccountType FType { get; set; }

        /// <summary>
        /// 账号名
        /// </summary>
        [BsonElement("FA")]
        public string FAccountName { get; set; }

        /// <summary>
        /// VIP等级
        /// </summary>
        [BsonElement("VL")]
        public VIPLevel FVIPLevel { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [BsonElement("Sex")]
        public UserSex FSex { get; set; }

        /// <summary>
        /// 微信登陆OpenId
        /// </summary>
        [BsonElement("OId")]
        public string FWXOpenId { get; set; }

        /// <summary>
        /// 微信头像地址
        /// </summary>
        [BsonElement("WI")]
        public string FWXHeadImgurl { get; set; }

        /// <summary>
        /// 支付宝登陆userId
        /// </summary>
        [BsonElement("AUId")]
        public string FAlipayUserId { get; set; }

        /// <summary>
        /// 支付宝头像地址
        /// </summary>
        [BsonElement("AI")]
        public string FAlipayHeadImgUrl { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [BsonElement("NN")]
        public string FNikeNmae { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [BsonElement("EM")]
        public string FEmail { get; set; }

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
        /// 一周投注金额
        /// </summary>
        [BsonElement("BOW")]
        public decimal FBetOneWeek { get; set; }

        /// <summary>
        /// 投注总金额
        /// </summary>
        [BsonElement("BT")]
        public decimal FBetTotal { get; set; }
    }
}
