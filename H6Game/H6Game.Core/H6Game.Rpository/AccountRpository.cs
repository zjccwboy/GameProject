using H6Game.Base;
using H6Game.Hotfix.Entities;
using System.Linq;
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

        public async Task<bool> AddAsync(TAccount account)
        {
            var q = await this.DBContext.WhereAsync(a => a.FAccountName == account.FAccountName, nameof(account.FAccountName));
            if (q.Any())
                return false;

            await this.DBContext.InsertAsync(account);
            return true;
        }
    }
}
