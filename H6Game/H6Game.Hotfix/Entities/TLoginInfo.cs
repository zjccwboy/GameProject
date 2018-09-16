using H6Game.Hotfix.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace H6Game.Hotfix.Entities
{
    public class TLoginInfo : BaseEntity
    {
        [BsonElement("AId")]
        public string FAccountId { get; set; }

        [BsonElement("LR")]
        public LoginResutlCode FLoginResult { get; set; }
    }
}
