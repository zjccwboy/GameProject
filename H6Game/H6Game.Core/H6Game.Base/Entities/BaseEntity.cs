using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace H6Game.Base
{
    [BsonIgnoreExtraElements]
    public abstract class BaseEntity
    {
        /// <summary>
        /// 获取属性的BsonElement名
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        [BsonIgnore]
        public string this[string propertyName]
        {
            get
            {
                return this.BsonElementName(propertyName);
            }
        }

        /// <summary>
        /// MongoDB Object _Id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement]
        public string Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonElement("CT")]
        public DateTime? FCreateTime { get; set; }

        /// <summary>
        /// 由谁创建的
        /// </summary>
        [BsonElement("C")]
        public string FCreator { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [BsonElement("UT")]
        public DateTime? FUpdateTime { get; set; }

        /// <summary>
        /// 由谁更新的
        /// </summary>
        [BsonElement("U")]
        public string FUpdater { get; set; }


        public void SetCreator(string creator)
        {
            this.FCreator = creator;
            this.FCreateTime = DateTime.UtcNow;
        }

        public void SetUpdater(string updater)
        {
            this.FUpdater = updater;
            this.FUpdateTime = DateTime.UtcNow;
        }
    }
}
