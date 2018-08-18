
namespace H6Game.Base
{
    [SingletCase]
    public sealed class SceneComponent : BaseComponent
    {
        /// <summary>
        /// 新建并添加一个组件到该组件中.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        public override bool AddComponent<T>(T component)
        {
            if (base.AddComponent(component))
            {
                Game.Event.AddComponent(component);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 新建或者从组件池中获取一个单例组件添加到该组件中.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public override T AddComponent<T>()
        {
            var component = base.AddComponent<T>();
            Game.Event.AddComponent(component);
            return component;
        }
    }
}
