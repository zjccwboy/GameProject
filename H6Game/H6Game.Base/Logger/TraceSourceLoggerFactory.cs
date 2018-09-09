

namespace H6Game.Base
{
    public class TraceSourceLoggerFactory : ILoggerFactory
    {
        public ILogger Create()
        {
            var logger = new TraceSourceLogger();
            return logger;
        }
    }

    /// <summary>
    /// 系统日志工厂
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// 创建一个系统日志
        /// </summary>
        ILogger Create();
    }
}
