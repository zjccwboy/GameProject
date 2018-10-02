using MongoDB.Bson.Serialization.Attributes;

namespace H6Game.Base
{
    public class TGame : BaseEntity
    {
        /// <summary>
        /// 游戏类型
        /// </summary>
        [BsonElement("GT")]
        public int FGameType { get; set; }

        /// <summary>
        /// 房间Id
        /// </summary>
        [BsonElement("RId")]
        public string FRoomId { get; set; }
    }
}
