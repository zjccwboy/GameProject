using H6Game.Base.Component;
using System.Collections.Generic;

namespace H6Game.Base.Logger
{
    public class EventManager
    {
        private HashSet<BaseComponent> Updates { get; } = new HashSet<BaseComponent>();
        private List<BaseComponent> Starts { get; } = new List<BaseComponent>();
        private List<BaseComponent> Disposers { get; } = new List<BaseComponent>();

        public void Update()
        {

            foreach (var component in Starts)
            {
                component.Start();
            }
            Starts.Clear();

            foreach (var dispose in Disposers)
            {
                Updates.Remove(dispose);
            }
            Disposers.Clear();

            foreach (var component in Updates)
            {
                component.Update();
            }
        }

        public bool Add(BaseComponent component)
        {
            var type = component.GetType();
            var eventType = ObjectTypeStorage.GetEvent(type);
            if (eventType == EventType.None)
                return false;

            if (Updates.Contains(component))
            {
                return false;
            }

            HandlerEvent(component, eventType & EventType.Awake);
            HandlerEvent(component, eventType & EventType.Start);
            HandlerEvent(component, eventType & EventType.Update);
            return true;
        }

        public void Remove(BaseComponent component)
        {
            Disposers.Add(component);
        }

        private void HandlerEvent(BaseComponent component, EventType eventType)
        {
            if ((eventType & EventType.Awake) == EventType.Awake)
            {
                if (component.IsAwake)
                    return;

                component.Awake();
                component.IsAwake = true;
            }
            else if ((eventType & EventType.Start) == EventType.Start)
            {
                Starts.Add(component);
            }
            else if ((eventType & EventType.Update) == EventType.Update)
            {
                Updates.Add(component);
            }
        }
    }
}
