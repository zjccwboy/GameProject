using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace H6Game.Base
{
    /// <summary>
    /// 事件驱动组件，如果实现了组件基本的Awake,Start,Update,Dispose，会统一注册到该类中统一处理。
    /// 注册事件使用EventAttirbute特性器注册。
    /// </summary>
    [SingletCase]
    public sealed class EventComponent : BaseComponent
    {
        private HashSet<BaseComponent> Disposes { get; } = new HashSet<BaseComponent>();
        private HashSet<BaseComponent> Updates { get; } = new HashSet<BaseComponent>();
        private HashSet<BaseComponent> Starts { get; } = new HashSet<BaseComponent>();

        public override void Update()
        {
            foreach(var component in Starts)
            {
                component.Start();
                component.IsStart = true;
            }

            Starts.Clear();

            foreach (var component in Updates)
            {
                component.Update();
            }
        }

        public override bool AddComponent<T>(T component)
        {
            var type = typeof(T);

            var eventType = TypePool.GetEvent(type);
            if (eventType == EventType.None)
                return false;

            if (!base.AddComponent(component))
                return false;

            HandlerEvent(component, eventType & EventType.Awake);
            HandlerEvent(component, eventType & EventType.Start);
            HandlerEvent(component, eventType & EventType.Update);
            HandlerEvent(component, eventType & EventType.Dispose);
            return true;
        }

        public override bool Remove(BaseComponent component)
        {
            var result = base.Remove(component);
            if (result)
            {
                var type = component.GetType();
                Updates.Remove(component);
                result &= Disposes.Remove(component);
                if (result)
                {
                    if (!ComponentPool.IsSingleType(type))
                        component.Dispose();
                }
            }
            return result;
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
                Starts.Add(component);
            }
            else if((eventType & EventType.Update) == EventType.Update)
            {
                Updates.Add(component);
            }
            else if((eventType & EventType.Dispose) == EventType.Dispose)
            {
                Disposes.Add(component);
            }
        }

    }
}
