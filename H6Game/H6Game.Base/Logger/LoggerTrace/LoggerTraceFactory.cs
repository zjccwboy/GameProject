

namespace H6Game.Base
{
    public class LoggerTraceFactory : ITraceSourceLoggerFactory
    {
        public ILoggerTrace Create()
        {
            var logger = new LoggerTrace();
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
        ILoggerTrace Create();
    }
}
