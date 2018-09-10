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

        private static Dictionary<Type, BaseEntity> Entitys { get; } = new Dictionary<Type, BaseEntity>();
        public static TEntity Create<TEntity>() where TEntity : BaseEntity,new()
        {
            var type = typeof(TEntity);
            if(!Entitys.TryGetValue(type, out BaseEntity value))
            {
                value = new TEntity();
                Entitys[type] = value;
            }

            return (TEntity)value;
        }
    }
}
