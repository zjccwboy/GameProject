using MongoDB.Bson.Serialization.Attributes;
using H6Game.Base;
using H6Game.Hotfix.Entities.Enums;

namespace H6Game.Rpository
{
    public class TLoginInfo : BaseEntity
    {
        [BsonElement("AId")]
        public string FAccountId { get; set; }

        [BsonElement("LR")]
        public LoginResutlCode FLoginResult { get; set; }
    }
}
