using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace H6Game.Base.Component
{
    public abstract class ComponentStorage
    {
        protected internal ConcurrentDictionary<Type, HashSet<BaseComponent>> TypeComponents { get; } = new ConcurrentDictionary<Type, HashSet<BaseComponent>>();
        protected internal ConcurrentDictionary<int, BaseComponent> IdComponents { get; } = new ConcurrentDictionary<int, BaseComponent>();
        protected internal ConcurrentDictionary<Type, BaseComponent> SingleComponents { get; } = new ConcurrentDictionary<Type, BaseComponent>();
    }
}
