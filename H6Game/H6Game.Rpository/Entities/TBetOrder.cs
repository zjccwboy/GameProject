using H6Game.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace H6Game.Rpository
{
    public class TBetOrder : BaseEntity
    {
        /// <summary>
        /// 未结算注单Id
        /// </summary>
        [BsonElement("UOId")]
        public string FUnsettledOrderId { get; set; }

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
        /// 输赢情况
        /// </summary>
        [BsonElement("WIN")]
        public decimal FWins { get; set; }
    }
}
