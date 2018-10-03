using System;

namespace H6Game.Base
{
    /// <summary>
    /// 该特性器用来标识单例组件
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SingleCaseAttribute : Attribute
    {

    }
}
