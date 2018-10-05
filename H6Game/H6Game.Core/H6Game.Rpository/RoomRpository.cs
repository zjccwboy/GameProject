using H6Game.Base;
using System.Linq;
using System.Threading.Tasks;
using H6Game.Hotfix.Entities;
using H6Game.Hotfix.Entities.Enums;

namespace H6Game.Rpository
{
    [SingleCase]
    public sealed class RoomRpository : DBRpository<TRoom>
    {
        public async Task<TRoom> GetById(string objectId)
        {
            var q = await this.DBContext.FindByIdAsync(objectId);

            if (q.Any())
                return q.FirstOrDefault();

            return null;
        }
    }
}
