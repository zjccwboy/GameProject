
namespace H6Game.Base
{
    public enum MethodType
    {
        /// <summary>
        /// 无返回参数同步方法
        /// </summary>
        Invoke,

        /// <summary>
        /// 有返回参数同步方法
        /// </summary>
        ExistReturnInvoke,

        /// <summary>
        /// 无返回参数异步方法，例如返回Task的方法。
        /// </summary>
        InvokeAsync,

        /// <summary>
        /// 有返回参数异步方法，例如返回Task<Result>方法
        /// </summary>
        ExistReturnInvokeAsync,
    }
}
