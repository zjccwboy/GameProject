using System;
using System.Reflection;
using System.Threading.Tasks;

namespace H6Game.Base
{
    /// <summary>
    /// 方法上下文
    /// </summary>
    public class MetodContext
    {
        /// <summary>
        /// 方法所在类对象
        /// </summary>
        public IComponentSubscriber Owner { get; set; }

        /// <summary>
        /// 方法参数类型
        /// </summary>
        public Type[] ParameterTypes { get; set; }

        /// <summary>
        /// 方法参数
        /// </summary>
        public object[] Parameters { get; set; }

        /// <summary>
        /// 方法放回参数类型
        /// </summary>
        public Type ReturnType { get; set; }

        /// <summary>
        /// 是否是异步方法
        /// </summary>
        public bool IsAsyncMetod { get; set; }

        /// <summary>
        /// 方法属性信息访问类
        /// </summary>
        public MethodInfo MethodInfo { get; set; }

        /// <summary>
        /// 方法类型
        /// </summary>
        public MetodType MetodType { get; set; }


        private static Type VoidType = typeof(void);
        private static Type TaskType = typeof(Task);

        /// <summary>
        /// 是否存在返回参数
        /// </summary>
        /// <returns></returns>
        public bool ExistReturn()
        {
            return ReturnType != VoidType && ReturnType != TaskType;
        }
    }
}
