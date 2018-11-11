﻿using System.Threading.Tasks;

namespace H6Game.Base
{
    [SingleCase]
    public class LoggerRpository : DBRpository<TLogger>
    {
        public override DBType DBType => DBType.LoggerDb;
        public async Task WriteLogger(TLogger loggerEntity)
        {
            this.DBContext.Insert(loggerEntity);
            await Task.CompletedTask;
        }
    }
}
