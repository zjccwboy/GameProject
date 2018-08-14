using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    /// <summary>
    /// 多例组件池，一个类型可以被多次实例化使用该池来保存与创建，类型对象的生命周期应该在组件Add->Remove之间，
    /// 当调用Remove实际类型对象并不会被GC回收，该对象会一直存在ComponentPool池中，可以被第二次使用。
    /// </summary>
    public static class ManyPool
    {
        private static ConcurrentDictionary<int, BaseComponent> IdDictionary { get; } = new ConcurrentDictionary<int, BaseComponent>();
        public static EventComponent EventComponent { get; }

        static ManyPool()
        {
            EventComponent = ComponentPool.Fetch<EventComponent>();
            IdDictionary[EventComponent.Id] = EventComponent;
        }

        public static T Add<T>() where T : BaseComponent
        {
            var value = ComponentPool.Fetch<T>();
            IdDictionary.AddOrUpdate(value.Id, value, (k, v) => { return value; });
            EventComponent.AddComponent(value);
            return value;
        }

        public static T Add<T, K1>(K1 k1) where T : BaseComponent
        {
            var value = ComponentPool.Fetch<T, K1>(k1);
            IdDictionary.AddOrUpdate(value.Id, value, (k, v) => { return value; });
            EventComponent.AddComponent(value);
            return value;
        }

        public static T Add<T, K1, K2>(K1 k1, K2 k2) where T : BaseComponent
        {
            var value = ComponentPool.Fetch<T, K1, K2>(k1, k2);
            IdDictionary.AddOrUpdate(value.Id, value, (k, v) => { return value; });
            EventComponent.AddComponent(value);
            return value;
        }

        public static T Add<T, K1, K2, K3>(K1 k1, K2 k2, K3 k3) where T : BaseComponent
        {
            var value = ComponentPool.Fetch<T, K1, K2, K3>(k1, k2, k3);
            IdDictionary.AddOrUpdate(value.Id, value, (k, v) => { return value; });
            EventComponent.AddComponent(value);
            return value;
        }

        public static T Add<T, K1, K2, K3, K4>(K1 k1, K2 k2, K3 k3, K4 k4) where T : BaseComponent
        {
            var value = ComponentPool.Fetch<T, K1, K2, K3, K4>(k1, k2, k3, k4);
            IdDictionary.AddOrUpdate(value.Id, value, (k, v) => { return value; });
            EventComponent.AddComponent(value);
            return value;
        }

        public static T Add<T, K1, K2, K3, K4, K5>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5) where T : BaseComponent
        {
            var value = ComponentPool.Fetch<T, K1, K2, K3, K4, K5>(k1, k2, k3, k4, k5);
            IdDictionary.AddOrUpdate(value.Id, value, (k, v) => { return value; });
            EventComponent.AddComponent(value);
            return value;
        }

        public static T Add<T, K1, K2, K3, K4, K5, K6>(K1 k1, K2 k2, K3 k3, K4 k4, K5 k5, K6 k6) where T : BaseComponent
        {
            var value = ComponentPool.Fetch<T, K1, K2, K3, K4, K5, K6>(k1, k2, k3, k4, k5, k6);
            IdDictionary.AddOrUpdate(value.Id, value, (k, v) => { return value; });
            EventComponent.AddComponent(value);
            return value;
        }

        public static void Remove<T>(this T component) where T : BaseComponent
        {
            IdDictionary.TryRemove(component.Id, out BaseComponent value);
        }
    }
}
