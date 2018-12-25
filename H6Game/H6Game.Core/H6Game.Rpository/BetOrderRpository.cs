using H6Game.Base;
using H6Game.Base.Component;
using H6Game.Hotfix.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace H6Game.Rpository
{
    [SingleCase]
    public class BetOrderRpository : DBRpository<TBetOrder>
    {
        public async Task<TBetOrder> GetById(string objectId)
        {
            var q = await this.DBContext.FindByIdAsync(objectId);

            if (q.Any())
                return q.FirstOrDefault();

            return null;
        }
    }
}
