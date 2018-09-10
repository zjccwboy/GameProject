using H6Game.Entities.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace H6Game.Entities
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
