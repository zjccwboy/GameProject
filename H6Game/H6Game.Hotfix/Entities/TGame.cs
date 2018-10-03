using H6Game.Base;
using H6Game.Hotfix.Entities.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace H6Game.Hotfix.Entities
{
    public class TGame : BaseEntity
    {
        /// <summary>
        /// 游戏类型
        /// </summary>
        [BsonElement("GT")]
        public GameType FGameType { get; set; }

        /// <summary>
        /// 房间Id
        /// </summary>
        [BsonElement("RId")]
        public string FRoomId { get; set; }
    }
}
