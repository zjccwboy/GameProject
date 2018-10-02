using H6Game.Base;
using System.Linq;
using System.Threading.Tasks;

namespace H6Game.Rpository
{
    [SingleCase]
    public sealed class RoomRpository : ARpository<TRoom>
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
