using System.Threading.Tasks;

namespace H6Game.Base
{
    [SingleCase]
    public class LoggerRpository : ARpository<TLogger>
    {
        public override DBType DBType => DBType.LoggerDb;
        public async Task WriteLogger(TLogger loggerEntity)
        {
            await this.DBContext.InsertAsync(loggerEntity);
        }
    }
}
