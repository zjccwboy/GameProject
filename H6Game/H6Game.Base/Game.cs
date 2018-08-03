using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class Game
    {
        private static Dictionary<int, BaseComponent> CmpDictionary = new Dictionary<int, BaseComponent>();
        private static Dictionary<Type, BaseComponent> typeDictionary = new Dictionary<Type, BaseComponent>();

        public Scene Scene { get; } = Scene.Instance;

        public static void Start()
        {
            foreach (var component in CmpDictionary.Values)
            {
                component.Start();
            }
        }

        public static void Update()
        {
            foreach(var component in CmpDictionary.Values)
            {
                component.Update();
            }
        }

        public static void Add<T>() where T : BaseComponent
        {
            var component = SinglePool.Get<T>();
            CmpDictionary[component.Id] = component;
            typeDictionary[typeof(T)] = component;
            var type = typeof(T);
        }

        public static void Remove<T>() where T : BaseComponent
        {
            var type = typeof(T);
            if (typeDictionary.TryGetValue(type, out BaseComponent component))
            {
                CmpDictionary.Remove(component.Id);
                typeDictionary.Remove(type);
            }
        }

        public static T Get<T>() where T : BaseComponent
        {
            return (T)typeDictionary[typeof(T)];
        }

        public static T Get<T>(int id) where T : BaseComponent
        {
            if(CmpDictionary.TryGetValue(id, out BaseComponent component))
            {
                return (T)component;
            }

            ComponentPool.TryGetComponent(id, out T value);
            return value;
        }
    }
}
