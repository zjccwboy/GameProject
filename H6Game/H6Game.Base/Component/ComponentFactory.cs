using System;
using System.Collections.Generic;

namespace H6Game.Base
{
    public class ComponentFactory
    {
        public static HashSet<Type> CmponentTypes { get; }

        static ComponentFactory()
        {
            CmponentTypes = TypePool.GetTypes<BaseComponent>();
        }

        public static BaseComponent CreateComponent(Type type)
        {
            var component = (BaseComponent)Activator.CreateInstance(type);
            component.Id = ComponentIdCreator.CreateId();
            return component;
        }

        public static BaseComponent CreateComponent(Type type, params object[] pars)
        {
            var component = (BaseComponent)Activator.CreateInstance(type, pars);
            component.Id = ComponentIdCreator.CreateId();
            return component;
        }
    }
}
