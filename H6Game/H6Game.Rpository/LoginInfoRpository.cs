﻿using H6Game.Base;
using H6Game.Entitys;
using System.Linq;
using System.Threading.Tasks;

namespace H6Game.Rpository
{
    public class LoginInfoRpository : BaseRpository<TLoginInfo>
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
            var q = await this.DBContext.FindAsync(t => t.FAccountId == accountId);

            if (q.Any())
                return q.FirstOrDefault();

            return null;
        }
    }
}