using H6Game.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace H6Game.Base
{
    public static class EntityHelper
    {
        public static string BsonElementName<TEntity>(this TEntity doc, string propertyName) where TEntity : BaseEntity
        {
            var epc = Game.Scene.GetComponent<EntityComponent>();
            return epc.GetElementName<TEntity>(propertyName);
        }

        public static PropertyInfo[] GetPropertys<TEntity>(this TEntity entity) where TEntity : BaseEntity
        {
            var epc = Game.Scene.GetComponent<EntityComponent>();
            return epc.GetPropertys<TEntity>();
        }

        private static Dictionary<Type, BaseEntity> Entitys { get; } = new Dictionary<Type, BaseEntity>();
        public static TEntity Create<TEntity>(this IRpository rpository) where TEntity : BaseEntity, new()
        {
            var type = typeof(TEntity);
            if (!Entitys.TryGetValue(type, out BaseEntity value))
            {
                value = new TEntity();
                Entitys[type] = value;
            }

            return (TEntity)value;
        }
    }
}
