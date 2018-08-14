using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace H6Game.Base
{
    /// <summary>
    /// 事件驱动组件，如果实现了组件基本的Awake,Start,Update,Dispose，会统一注册到该类中统一处理。
    /// 注册事件使用EventAttirbute特性器注册。
    /// </summary>
    public class EventComponent : BaseComponent
    {
        private ConcurrentDictionary<Type, HashSet<BaseComponent>> UpdateDictionary { get; } = new ConcurrentDictionary<Type, HashSet<BaseComponent>>();
        private ConcurrentDictionary<int, BaseComponent> DisposeDictionary { get; } = new ConcurrentDictionary<int, BaseComponent>();

        public override void Update()
        {
            var keyVals = this.UpdateDictionary;
            foreach(var kv in keyVals)
            {
                var doEvent = TypePool.GetEvent(kv.Key);
                var values = keyVals[kv.Key];
                foreach(var val in values)
                {
                    val.Update();
                }
            }
        }

        public override void AddComponent<T>(T component)
        {
            var type = typeof(T);

            var eventType = TypePool.GetEvent(type);
            if (eventType == EventType.None)
                return;

            IdComponent.AddOrUpdate(component.Id, component, (k, v) => { return component; });

            if (!TypeComponent.TryGetValue(type, out HashSet<BaseComponent> ids))
            {
                ids = new HashSet<BaseComponent>();
                TypeComponent[type] = ids;
            }
            ids.Add(component);

            HandlerEvent(component, eventType & EventType.Awake);
            HandlerEvent(component, eventType & EventType.Start);
            HandlerEvent(component, eventType & EventType.Update);
            HandlerEvent(component, eventType & EventType.Dispose);
        }

        public override void Remove(BaseComponent component)
        {
            if (!IdComponent.TryGetValue(component.Id, out BaseComponent value))
                return;

            var type = value.GetType();
            if (TypeComponent.TryGetValue(type, out HashSet<BaseComponent> hashVal))
                hashVal.Remove(component);

            if (UpdateDictionary.TryGetValue(type, out hashVal))
                hashVal.Remove(component);

            if (DisposeDictionary.TryRemove(component.Id, out value))
                value.Dispose();
        }

        private void HandlerEvent(BaseComponent component, EventType eventType)
        {
            if((eventType & EventType.Awake) == EventType.Awake)
            {
                if (component.IsAwake)
                    return;

                component.Awake();
                component.IsAwake = true;
            }
            else if((eventType & EventType.Start) == EventType.Start)
            {
                if (component.IsStart)
                    return;

                component.Start();
                component.IsStart = true;
            }
            else if((eventType & EventType.Update) == EventType.Update)
            {
                var type = component.GetType();
                if(!UpdateDictionary.TryGetValue(type, out HashSet<BaseComponent> hashVal))
                {
                    hashVal = new HashSet<BaseComponent>();
                    UpdateDictionary[type] = hashVal;
                }
                hashVal.Add(component);
            }
            else if((eventType & EventType.Dispose) == EventType.Dispose)
            {
                DisposeDictionary.AddOrUpdate(component.Id, component, (k, v) => { return component; });
            }
        }

    }
}
