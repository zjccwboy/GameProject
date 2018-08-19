using H6Game.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace H6Game.Base.Component.DB
{
    [SingletCase]
    public class AccountRpositoryComponent : BaseRpository<AccountEntity>
    {

        public Task<List<AccountEntity>> GetAccountById(string objectId)
        {
            return this.DBContext.FindByIdAsync(objectId);
        }


    }
}
