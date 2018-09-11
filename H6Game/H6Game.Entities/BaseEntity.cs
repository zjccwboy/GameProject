using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Entities
{
    [BsonIgnoreExtraElements]
    public abstract class BaseEntity
    {
        /// <summary>
        /// MongoDB Object _Id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement]
        [ProtoMember(1)]
        public string Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonElement("CT")]
        public DateTime? FCreateTime { get; set; }

        /// <summary>
        /// 由谁创建的
        /// </summary>
        [BsonElement("CRT")]
        public string FCreator { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [BsonElement("UT")]
        public DateTime? FUpdateTime { get; set; }

        /// <summary>
        /// 由谁更新的
        /// </summary>
        [BsonElement("UDT")]
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
