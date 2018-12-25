using H6Game.Base;
using H6Game.Base.Component;
using H6Game.Hotfix.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace H6Game.Rpository
{
    [SingleCase]
    public class AccountRpository : DBRpository<TAccount>
    {
        public async Task<TAccount> GetById(string objectId)
        {
            var q = await this.DBContext.FindByIdAsync(objectId);

            if (q.Any())
                return q.FirstOrDefault();

            return null;
        }

        public async Task<TAccount> GetByOpenId(string openId)
        {
            var q = await this.DBContext.WhereAsync(t => t.FWXOpenId == openId);

            if (q.Any())
                return q.FirstOrDefault();

            return null;
        }

        public async Task<TAccount> GetByPhoneNumber(string phoneNumber)
        {
            var q = await this.DBContext.WhereAsync(t => t.FPhoneNumber == phoneNumber);

            if (q.Any())
                return q.FirstOrDefault();

            return null;
        }

        public async Task<TAccount> GetByName(string name)
        {
            var q = await this.DBContext.WhereAsync(t => t.FAccountName == name);

            if (q.Any())
                return q.FirstOrDefault();

            return null;
        }
    }
}
