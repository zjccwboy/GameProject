using System;
using System.Threading.Tasks;

namespace H6Game.Base.Logger
{
    public interface IMyLog
    {
        Task WriteMessage(LogLevel FLogLevel, string stackInfo, string message, string args, LoggerBllType bllType, Exception exception);
    }
}
