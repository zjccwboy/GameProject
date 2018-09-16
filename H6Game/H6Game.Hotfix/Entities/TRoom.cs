using H6Game.Hotfix.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace H6Game.Hotfix.Entities
{
    public class TRoom : BaseEntity
    {
        /// <summary>
        /// 房间类型
        /// </summary>
        [BsonElement("RT")]
        public RoomType FRoomType { get; set; }

        /// <summary>
        /// 房间游戏类型
        /// </summary>
        [BsonElement("GT")]
        public GameType FGameType { get; set; }
    }
}
