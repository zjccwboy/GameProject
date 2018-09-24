using System.Collections.Generic;

namespace H6Game.Base
{
    public class EventManager
    {
        private List<BaseComponent> Updates { get; } = new List<BaseComponent>();
        private List<BaseComponent> Starts { get; } = new List<BaseComponent>();

        public void Update()
        {
            foreach (var component in Starts)
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

        public bool Add<T>(T component) where T : BaseComponent
        {
            var type = typeof(T);

            var eventType = TypePool.GetEvent(type);
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
                if (component.IsStart)
                    return;
                Starts.Add(component);
            }
            else if ((eventType & EventType.Update) == EventType.Update)
            {
                Updates.Add(component);
            }
        }
    }
}
