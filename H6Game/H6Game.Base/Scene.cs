using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class Scene : BaseComponent
    {
        private Dictionary<int, BaseComponent> idDictionary = new Dictionary<int, BaseComponent>();
        private Dictionary<Type, LinkedList<BaseComponent>> typeDictionary = new Dictionary<Type, LinkedList<BaseComponent>>();

        public void Add<T>() where T : BaseComponent
        {
            var component = ComponentPool.Fetch<T>();
            idDictionary[component.Id] = component;

            var type = typeof(T);
            if(!typeDictionary.TryGetValue(type, out LinkedList<BaseComponent> list))
            {
                list = new LinkedList<BaseComponent>();
                typeDictionary[typeof(T)] = list;
            }

            list.AddLast(component);
        }

        public void Remove<T>(BaseComponent component) where T : BaseComponent
        {
            idDictionary.Remove(component.Id);

            var type = typeof(T);
            if (!typeDictionary.TryGetValue(type, out LinkedList<BaseComponent> list))
            {
                return;
            }

            foreach(var cmp in list)
            {
                if(cmp.Id == component.Id)
                {
                    list.Remove(cmp);
                    break;
                }
            }
        }

        public IEnumerable<T> Get<T>() where T : BaseComponent
        {
            var result = typeDictionary[typeof(T)];
            return (IEnumerable<T>)result;
        }

        public override void Update()
        {
            var components = idDictionary.Values;
            foreach(var component in components)
            {
                component.Update();
            }
        }

        public T Get<T>(int id) where T : BaseComponent
        {
            if (idDictionary.TryGetValue(id, out BaseComponent component))
            {
                return (T)component;
            }

            ComponentPool.TryGetComponent(id, out T value);
            return value;
        }
    }
}
