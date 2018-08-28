using H6Game.Entitys.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace H6Game.Entitys
{
    public class TLoginInfo : BaseEntity
    {
        [BsonElement("AId")]
        public string FAccountId { get; set; }

        [BsonElement("LR")]
        public LoginResutlCode FLoginResult { get; set; }       

        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonElement("CT")]
        public DateTime FCreateTime { get; set; }
    }
}
