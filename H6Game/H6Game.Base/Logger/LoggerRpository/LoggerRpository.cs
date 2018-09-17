using System.Threading.Tasks;

namespace H6Game.Base
{
    [SingletCase]
    public class LoggerRpository : BaseRpository<LoggerEntity>
    {
        public async Task WriteLogger(LoggerEntity loggerEntity)
        {
            await this.DBContext.InsertAsync(loggerEntity);
        }
    }
}
