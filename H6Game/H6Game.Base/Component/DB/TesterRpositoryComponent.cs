
using System.Linq;
using System.Threading.Tasks;

namespace H6Game.Base
{
    [SingletCase]
    public class TesterRpositoryComponent : BaseRpository<ActorInfoEntity>
    {
        public async Task<ActorInfoEntity> GetInfoById(string objectId)
        {
            var q = await this.DBContext.FindByIdAsync(objectId);

            if (!q.Any())
                return q.FirstOrDefault();

            var upName = this.DefaultEntity.GetElementName(nameof(this.DefaultEntity.ActorId));

            return null;
        }
    }
}
