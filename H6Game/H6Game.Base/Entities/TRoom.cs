using MongoDB.Bson.Serialization.Attributes;

namespace H6Game.Base
{
    public class TRoom : BaseEntity
    {
        /// <summary>
        /// 房间类型
        /// </summary>
        [BsonElement("RT")]
        public int FRoomType { get; set; }

        /// <summary>
        /// 房间游戏类型
        /// </summary>
        [BsonElement("GT")]
        public int FGameType { get; set; }
    }
}
