using H6Game.Entitys;
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
        private Dictionary<Type, Dictionary<string, string>> ClassPropertiesElementName { get; } = new Dictionary<Type, Dictionary<string, string>>();
        private Dictionary<Type, PropertyInfo[]> ClassPropertyInfos { get; } = new Dictionary<Type, PropertyInfo[]>();

        public string GetPropertyElementName<TDoc>(TDoc doc, string propertyName) where TDoc : BaseEntity
        {
            var type = typeof(TDoc);
            if (ClassPropertiesElementName.TryGetValue(type, out Dictionary<string, string> properties))
            {
                if (properties.TryGetValue(propertyName, out string name))
                    return name;
            }
            return null;
        }

        public PropertyInfo[] GetPropertys<TDoc>() where TDoc : BaseEntity
        {
            return ClassPropertyInfos[typeof(TDoc)];
        }

        public override void Awake()
        {
            var types = TypePool.GetTypes<BaseEntity>();
            SetPropertiesName(types);
        }

        private void SetPropertiesName(HashSet<Type> types)
        {
            foreach(var type in types)
            {
                if(!ClassPropertiesElementName.TryGetValue(type, out Dictionary<string, string> propDic))
                {
                    propDic = new Dictionary<string, string>();
                    ClassPropertiesElementName[type] = propDic;
                }

                var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                ClassPropertyInfos[type] = properties;

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
