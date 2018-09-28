using H6Game.Hotfix.Entities;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace H6Game.Base
{
    [Event(EventType.Awake)]
    [SingletCase]
    public class EntityComponent : BaseComponent
    {
        private Dictionary<Type, Dictionary<string, string>> EntitiesPropertiesElementName { get; } = new Dictionary<Type, Dictionary<string, string>>();
        private Dictionary<Type, PropertyInfo[]> EntitiesProperties { get; } = new Dictionary<Type, PropertyInfo[]>();

        public string this[Type type,string propertyName]
        {
            get { return GetElementName(type, propertyName); }
        }

        private string GetElementName(Type type, string propertyName)
        {
            if (EntitiesPropertiesElementName.TryGetValue(type, out Dictionary<string, string> properties))
            {
                if (properties.TryGetValue(propertyName, out string name))
                    return name;
            }
            return null;
        }

        public string GetElementName<TEntity>(string propertyName) where TEntity : BaseEntity
        {
            var type = typeof(TEntity);
            return GetElementName(type, propertyName);
        }

        public PropertyInfo[] GetPropertys<TEntity>() where TEntity : BaseEntity
        {
            return EntitiesProperties[typeof(TEntity)];
        }

        public override void Awake()
        {
            var types = ObjectPool.GetTypes<BaseEntity>();
            SetPropertiesName(types);
        }

        private void SetPropertiesName(HashSet<Type> types)
        {
            foreach(var type in types)
            {
                if(!EntitiesPropertiesElementName.TryGetValue(type, out Dictionary<string, string> propDic))
                {
                    propDic = new Dictionary<string, string>();
                    EntitiesPropertiesElementName[type] = propDic;
                }

                var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                EntitiesProperties[type] = properties;

                foreach (var prop in properties)
                {
                    var attribute = prop.GetCustomAttribute<BsonElementAttribute>();
                    if (attribute == null)
                        continue;

                    propDic[prop.Name] = attribute.ElementName;
                }
            }
        }
    }
}
