

namespace H6Game.Base
{
    public class TraceSourceLoggerFactory : ITraceSourceLoggerFactory
    {
        public ILogger Create()
        {
            var logger = new TraceSourceLogger();
            logger.Create();
            return logger;
        }
    }

    /// <summary>
    /// 系统日志工厂
    /// </summary>
    public interface ITraceSourceLoggerFactory
    {
        /// <summary>
        /// 创建一个系统日志
        /// </summary>
        ILogger Create();
    }
}
