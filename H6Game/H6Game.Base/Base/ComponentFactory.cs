using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace H6Game.Base
{
    public class ComponentFactory
    {
        public static HashSet<Type> CmponentTypes { get; }

        static ComponentFactory()
        {
            CmponentTypes = ObjectFactory.GetTypes<BaseComponent>();
        }

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
    }
}
