using System;
using System.Threading.Tasks;

namespace H6Game.Base
{
    public interface IMyLog
    {
        Task WriteMessage(LogLevel FLogLevel, string message, string args, LoggerBllType bllType, Exception exception);
    }
}
