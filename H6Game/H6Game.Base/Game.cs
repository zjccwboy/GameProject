using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class Game
    {
        private static Dictionary<int, BaseComponent> idDictionary = new Dictionary<int, BaseComponent>();
        private static Dictionary<Type, BaseComponent> typeDictionary = new Dictionary<Type, BaseComponent>();

        public static void Start()
        {
            foreach (var component in idDictionary.Values)
            {
                component.Start();
            }
        }

        public static void Update()
        {
            foreach(var component in idDictionary.Values)
            {
                component.Update();
            }
        }

        public static void Add<T>() where T : BaseComponent
        {
            var component = SinglePool.Get<T>();
            idDictionary[component.Id] = component;
            typeDictionary[typeof(T)] = component;
            var type = typeof(T);
        }

        public static void Remove<T>() where T : BaseComponent
        {
            var type = typeof(T);
            if (typeDictionary.TryGetValue(type, out BaseComponent component))
            {
                idDictionary.Remove(component.Id);
                typeDictionary.Remove(type);
            }
        }

        public static T Get<T>() where T : BaseComponent
        {
            return (T)typeDictionary[typeof(T)];
        }

        public static T Get<T>(int id) where T : BaseComponent
        {
            if(idDictionary.TryGetValue(id, out BaseComponent component))
            {
                return (T)component;
            }

            ComponentPool.TryGetComponent(id, out T value);
            return value;
        }
    }
}
