using H6Game.Entities;

namespace H6Game.Base
{
    public abstract class BaseComponent<TEntity> : BaseComponent where TEntity : BaseEntity
    {
        public TEntity EntityInfo { get; protected set; }

        public override void Dispose()
        {
            this.EntityInfo = null;
            base.Dispose();
        }
    }
}
