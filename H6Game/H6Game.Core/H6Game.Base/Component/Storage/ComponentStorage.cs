﻿using System;
using System.Collections.Generic;

namespace H6Game.Base
{
    public abstract class ComponentStorage
    {
        protected internal Dictionary<Type, HashSet<BaseComponent>> TypeComponents { get; } = new Dictionary<Type, HashSet<BaseComponent>>();
        protected internal Dictionary<int, BaseComponent> IdComponents { get; } = new Dictionary<int, BaseComponent>();
        protected internal Dictionary<Type, BaseComponent> SingleComponents { get; } = new Dictionary<Type, BaseComponent>();

        /// <summary>
        /// 清空缓冲区
        /// </summary>
        public void Clear()
        {
            TypeComponents.Clear();
            IdComponents.Clear();
            SingleComponents.Clear();
        }
    }
}
