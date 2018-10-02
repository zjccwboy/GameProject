using H6Game.Base;
using System.Linq;
using System.Threading.Tasks;

namespace H6Game.Rpository
{
    [SingleCase]
    public class BetOrderRpository : ARpository<TBetOrder>
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
