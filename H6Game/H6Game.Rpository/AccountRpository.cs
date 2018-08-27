using H6Game.Base;
using H6Game.Entitys;
using System.Linq;
using System.Threading.Tasks;

namespace H6Game.Rpository
{
    [SingletCase]
    public class AccountRpository : BaseRpository<TAccount>
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
            var q = await this.DBContext.FindAsync(t => t.FWXOpenId == openId);

            if (q.Any())
                return q.FirstOrDefault();

            return null;
        }

        public async Task<TAccount> GetByPhoneNumber(string phoneNumber)
        {
            var q = await this.DBContext.FindAsync(t => t.FPhoneNumber == phoneNumber);

            if (q.Any())
                return q.FirstOrDefault();

            return null;
        }
    }
}
