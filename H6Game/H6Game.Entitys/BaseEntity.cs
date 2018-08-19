using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Entitys
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
        public static Entity Create<Entity>() where Entity : BaseEntity,new()
        {
            var type = typeof(Entity);
            if(!Entitys.TryGetValue(type, out BaseEntity value))
            {
                value = new Entity();
                Entitys[type] = value;
            }
            return (Entity)value;
        }
    }
}
