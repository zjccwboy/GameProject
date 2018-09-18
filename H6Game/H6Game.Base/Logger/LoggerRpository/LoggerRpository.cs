using System.Threading.Tasks;

namespace H6Game.Base
{
    [SingletCase]
    public class LoggerRpository : BaseRpository<LoggerEntity>
    {
        public override DBType DBType => DBType.LoggerDb;
        public async Task WriteLogger(LoggerEntity loggerEntity)
        {
            await this.DBContext.InsertAsync(loggerEntity);
        }
    }
}
