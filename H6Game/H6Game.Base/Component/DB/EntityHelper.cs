using H6Game.Entitys;

namespace H6Game.Base
{
    public static class EntityHelper
    {
        public static string GetElementName<TEntity>(this TEntity doc, string propertyName) where TEntity : BaseEntity
        {
            var epc = Game.Scene.GetComponent<EntityComponent>();
            return epc.GetElementName<TEntity>(propertyName);
        }
    }
}
