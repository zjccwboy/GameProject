﻿using System;
using System.Collections.Generic;

namespace H6Game.Base
{
    public class ComponentFactory
    {
        public static HashSet<Type> CmponentTypes { get; }

        static ComponentFactory()
        {
            CmponentTypes = ObjectPool.GetTypes<BaseComponent>();
            var rpositoryTypes = ObjectPool.GetTypes<IRpository>();
            foreach(var type in rpositoryTypes)
            {
                CmponentTypes.Add(type);
            }
        }

        public static BaseComponent CreateComponent(Type type)
        {
            var component = (BaseComponent)Activator.CreateInstance(type);
            return component;
        }

        public static BaseComponent CreateComponent(Type type, params object[] pars)
        {
            var component = (BaseComponent)Activator.CreateInstance(type, pars);
            return component;
        }
    }
}
