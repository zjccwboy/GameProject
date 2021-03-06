﻿using H6Game.Base;
using H6Game.Base.Component;
using H6Game.Hotfix.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace H6Game.Rpository
{
    [SingleCase]
    public class LoginInfoRpository : DBRpository<TLoginInfo>
    {
        public async Task<TLoginInfo> GetById(string objectId)
        {
            var q = await this.DBContext.FindByIdAsync(objectId);

            if (q.Any())
                return q.FirstOrDefault();

            return null;
        }

        public async Task<TLoginInfo> GetByAccountId(string accountId)
        {
            var q = await this.DBContext.WhereAsync(t => t.FAccountId == accountId);

            if (q.Any())
                return q.FirstOrDefault();

            return null;
        }
    }
}
