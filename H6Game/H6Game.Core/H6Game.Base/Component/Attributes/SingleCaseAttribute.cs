using System;

namespace H6Game.Base
{
    /// <summary>
    /// 该特性器用来标识单例组件，单例组件表示只能被实例化一次，并且生命周期一直存在直到进程的生命周期走完。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SingleCaseAttribute : Attribute
    {

    }
}
