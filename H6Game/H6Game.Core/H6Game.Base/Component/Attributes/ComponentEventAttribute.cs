using System;

namespace H6Game.Base.Component
{
    /// <summary>
    /// 组件事件特性器，在组件重写Awake、Start、Update事件时需要用改特性器标识相应的类事件，否则重写的
    /// 方法事件不能被执行。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ComponentEventAttribute : Attribute
    {
        public EventType EventType { get; }

        public ComponentEventAttribute(EventType eventType)
        {
            this.EventType = eventType;
        }
    }
}
