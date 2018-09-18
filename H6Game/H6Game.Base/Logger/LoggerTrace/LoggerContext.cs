using System;

namespace H6Game.Base
{

    /// <summary>
    /// 系统日志上下文.
    /// </summary>
    public static class LoggerContext
    {
        static ITraceSourceLoggerFactory _currentLogFactory = null;

        /// <summary>
        /// 设置当然上下文使用的日志工厂
        /// <remarks>
        /// 该上下文始终使用一个日志工厂进行创建日志，建议在系统启动时进行设置，设置后将无法再进行设置
        /// </remarks>
        /// </summary>
        /// <param name="factory">日志工厂</param>
        public static void SetCurrent(ITraceSourceLoggerFactory factory)
        {
            if (_currentLogFactory != null)
                throw new ArgumentException("该日志上下文已经设置使用的日志工厂，无法进行再次设置，如需使用其他日志工厂，请直接实例日志工厂进行操作。", "factory");

            _currentLogFactory = factory;
        }

        /// <summary>
        /// 创建一个日志
        /// </summary>
        public static ILoggerTrace CreateLog()
        {
            if (_currentLogFactory == null)
                throw new NullReferenceException("该日志上下文尚未设置日志工厂，请使用SetCurrent方法进行设置。");

            return _currentLogFactory.Create();
        }
    }

}
