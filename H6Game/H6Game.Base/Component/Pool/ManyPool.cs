using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    /// <summary>
    /// 多例组件池，一个类型可以被多次实例化使用该池来保存与创建，类型对象的生命周期应该在组件Add->Remove之间，
    /// 当调用Remove实际类型对象并不会被GC回收，该对象会一直存在ComponentPool池中，直到被第二次使用。
    /// </summary>
    public static class ManyPool
    {
        private static ConcurrentDictionary<int, BaseComponent> IdDictionary { get; } = new ConcurrentDictionary<int, BaseComponent>();


    }
}
