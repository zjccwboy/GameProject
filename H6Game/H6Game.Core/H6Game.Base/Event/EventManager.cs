using System.Collections.Generic;

namespace H6Game.Base
{
    public class EventManager
    {
        private HashSet<BaseComponent> Updates { get; } = new HashSet<BaseComponent>();
        private List<BaseComponent> TempUpdates { get; } = new List<BaseComponent>();
        private List<BaseComponent> Starts { get; } = new List<BaseComponent>();
        private List<BaseComponent> TempStarts { get; } = new List<BaseComponent>();

        public void Update()
        {
            TempStarts.AddRange(Starts);
            foreach (var component in TempStarts)
            {
                component.Start();
            }
            Starts.Clear();
            TempStarts.Clear();

            TempUpdates.AddRange(Updates);
            foreach (var component in TempUpdates)
            {
                component.Update();
            }
            TempUpdates.Clear();
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

        public bool Remove(BaseComponent component)
        {
            return Updates.Remove(component);
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
