using H6Game.Entitys;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H6Game.Base
{
    [SingletCase]
    public class AccountRpositoryComponent : BaseRpository<TAccount>
    {

        public async Task<TAccount> GetAccountById(string objectId)
        {
            var q = await this.DBContext.FindByIdAsync(objectId);

            if (!q.Any())
                return q.FirstOrDefault();

            var upName = this.DefaultEntity.GetElementName(nameof(this.DefaultEntity.FType));

            return null;
        }
    }
}
