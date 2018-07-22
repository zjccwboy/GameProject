using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace H6Game.Component.Base
{
    public class ComponentFactory
    {
        static ComponentFactory()
        {
            var baseType = typeof(BaseComponent);
            var subTypes = Assembly.GetExecutingAssembly().GetTypes().Where((a) => IsSubClassOf(a, baseType));
            foreach (var type in subTypes)
            {
                CmponentTypes.Add(type);
            }
        }

        public static List<Type> CmponentTypes { get; } = new List<Type>();

        public static BaseComponent CreateComponent(Type type)
        {
            var component = (BaseComponent)Activator.CreateInstance(type);
            component.ComponentId = ComponentIdCreator.CreateId();
            return component;
        }

        public static BaseComponent CreateComponent(Type type, params object[] pars)
        {
            var component = (BaseComponent)Activator.CreateInstance(type, pars);
            component.ComponentId = ComponentIdCreator.CreateId();
            return component;
        }

        private static bool IsSubClassOf(Type type, Type baseType)
        {
            var b = type.BaseType;
            while (b != null)
            {
                if (b.Equals(baseType))
                {
                    return true;
                }
                b = b.BaseType;
            }
            return false;
        }
    }
}
