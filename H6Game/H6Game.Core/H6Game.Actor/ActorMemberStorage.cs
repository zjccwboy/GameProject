using H6Game.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H6Game.Actor
{
    public sealed class ActorMemberStorage : ComponentStorage
    {
        private Dictionary<Type, HashSet<BaseActorComponent>> ActorTypeComponents { get; } = new Dictionary<Type, HashSet<BaseActorComponent>>();

        /// <summary>
        /// 添加一个组件
        /// </summary>
        /// <param name="component"></param>
        internal void AddComponent(BaseActorComponent component)
        {
            var type = component.GetType();
            IdComponents[component.Id] = component;
            if (!ActorTypeComponents.TryGetValue(component.GetType(), out HashSet<BaseActorComponent> components))
            {
                components = new HashSet<BaseActorComponent>();
                ActorTypeComponents[type] = components;
            }
            components.Add(component);
        }


        /// <summary>
        /// 获取一个组件集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal HashSet<BaseActorComponent> GetComponents<T>() where T : BaseActorComponent
        {
            var type = typeof(T);
            if (!ActorTypeComponents.TryGetValue(type, out HashSet<BaseActorComponent> components))
                return null;

            return components;
        }

        /// <summary>
        /// 获取一个组件集合
        /// </summary>
        /// <returns></returns>
        internal HashSet<BaseActorComponent> GetComponents(Type type)
        {
            if (!ActorTypeComponents.TryGetValue(type, out HashSet<BaseActorComponent> components))
                return null;

            return components;
        }

        /// <summary>
        /// 获取一个组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        internal T GetComponent<T>(int id) where T : BaseActorComponent
        {
            if (!IdComponents.TryGetValue(id, out BaseComponent component))
                return null;

            return (T)component;
        }

        /// <summary>
        /// 删除一个组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        internal bool Remove<T>(T component) where T : BaseActorComponent
        {
            if (!IdComponents.Remove(component.Id))
                return false;

            var type = typeof(T);
            if (ActorTypeComponents.TryGetValue(type, out HashSet<BaseActorComponent> hashVal))
                hashVal.Remove(component);

            return false;
        }

        /// <summary>
        /// 删除一个组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        internal bool Remove(int id)
        {
            if (!IdComponents.TryGetValue(id, out BaseComponent value))
                return false;

            if (!IdComponents.Remove(id))
                return false;

            var type = value.GetType();
            if (ActorTypeComponents.TryGetValue(type, out HashSet<BaseActorComponent> hashVal))
                hashVal.Remove(value as BaseActorComponent);

            return false;
        }

        /// <summary>
        /// 清空缓冲区
        /// </summary>
        public void Clear()
        {
            ActorTypeComponents.Clear();
            TypeComponents.Clear();
            IdComponents.Clear();
            SingleComponents.Clear();
        }
    }
}
