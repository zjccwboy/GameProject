using H6Game.Base;
using H6Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H6Game.Rpository
{
    [SingletCase]
    public class SettledOrderRpositoryt : BaseRpository<TSettledOrder>
    {
        public async Task<TSettledOrder> GetById(string objectId)
        {
            var q = await this.DBContext.FindByIdAsync(objectId);

            if (q.Any())
                return q.FirstOrDefault();

            return null;
        }
    }
}
