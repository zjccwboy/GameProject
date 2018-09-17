using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace H6Game.Base
{
    public interface IMyLog
    {
        Task WriteMessage(LogLevel FLogLevel, string message, EventId eventId, string args = null, Exception exception = null);
    }
}
