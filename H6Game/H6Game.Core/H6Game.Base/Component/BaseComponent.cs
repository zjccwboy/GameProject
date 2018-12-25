using System;

namespace H6Game.Base.Component
{
    public abstract class BaseComponent : IDisposable
    {        
        /// <summary>
        /// 该事件只有第一次创建出来的时候执行一次，应该把资源初始化之类的逻辑放到该事件重写方法中处理。
        /// </summary>
        public virtual void Awake() { }

        /// <summary>
        /// 每一次从池中拿出来的时候会被执行一次，在重写方法中可以把一些入口逻辑放到其中。
        /// </summary>
        public virtual void Start() { }

        /// <summary>
        /// 每一帧都会被执行
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// 调用该事件会把当前的组件放回池中
        /// </summary>
        public virtual void Dispose()
        {
            Game.Scene.Remove(this);
            Game.Event.Remove(this);
            this.PutBack();            
        }

        /// <summary>
        /// 组件唯一Id标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Awake事件状态
        /// </summary>
        public bool IsAwake { get; set; }

    }
}
