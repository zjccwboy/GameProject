
namespace H6Game.Base
{
    /// <summary>
    /// 组件事件类型
    /// </summary>
    public enum EventType
    {
        None,
        /// <summary>
        /// 该事件表示组件被第一次实例化之后执行一次，类似无参构造函数，为了方便记忆类被初始化之后执行的代码，所以设计了
        /// 该事件，在使用ComponentEventAttribute特性器标识了组件类并重写了组件类的Awake的方法，该事件才会被执行。
        /// </summary>
        Awake = 1,

        /// <summary>
        /// 该事件在第一次实例化类时与第一次从池中Fetch取出对象时都会执行一次，,在使用ComponentEventAttribute特性器标识了组
        /// 件类并重写了组件类的Start的方法，该事件才会被执行。在设计复用的组件时，该事件可以用于初始化一些类的依赖项，结合
        /// 重写Dispose事件来完成Start事件依赖项的清理，在Dispose被调用以后该类的实例会被放回池中，这样就可很好管理复用的组
        /// 件依赖注入与清理，可以循环的利用复用组件与内存池减少频繁实例化组件带来的开销。
        /// </summary>
        Start = 1 << 1,

        /// <summary>
        /// 更新事件，在使用ComponentEventAttribute特性器标识了组件类并重写了组件类的Awake的方法，该事件才会被执行。该事件主
        /// 要使用在状态机更新，这个框架基本都是基于单线程有限状态机的设计，Update事件包含了几乎所有事件的驱动。
        /// </summary>
        Update = 1 << 2,
    }
}
