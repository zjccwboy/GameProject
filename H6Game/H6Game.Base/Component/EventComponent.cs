using System;
using System.Collections.Generic;

namespace H6Game.Base
{
    public class EventComponent : BaseComponent
    {
        public override void Update()
        {
            var keyVals = this.TypeComponent;
            foreach(var kv in keyVals)
            {
                var doEvent = TypePool.GetEvent(kv.Key);
                var values = keyVals[kv.Key];
                foreach(var val in values)
                {
                    HandlerEvent(val, doEvent & EventType.Update);
                }
            }
        }

        public override void Add<T>(T component)
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

            if (component.IsAwake)
                return;

            HandlerEvent(component, eventType & EventType.Awake);
            HandlerEvent(component, eventType & EventType.Start);
        }

        private void HandlerEvent(BaseComponent component, EventType eventType)
        {
            if((eventType & EventType.Awake) == EventType.Awake)
            {
                component.Awake();
                component.IsAwake = true;
            }
            else if((eventType & EventType.Start) == EventType.Start)
            {
                component.Start();
                component.IsStart = true;
            }
            else if((eventType & EventType.Update) == EventType.Update)
            {
                component.Update();
            }
            else if((eventType & EventType.Close) == EventType.Close)
            {
                component.Close();
            }
        }

    }
}
