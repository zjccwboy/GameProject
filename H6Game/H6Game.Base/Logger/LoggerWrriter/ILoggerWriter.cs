using System;
using System.Threading.Tasks;

namespace H6Game.Base
{
    public interface ILoggerWriter :  IDisposable
    {
        bool CanWrite(LogLevel logLevel);
        Task WriteMessage(LoggerEntity entity);
    }
}
