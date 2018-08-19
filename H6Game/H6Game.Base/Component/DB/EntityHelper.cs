using H6Game.Entitys;

namespace H6Game.Base
{
    public static class EntityHelper
    {
        public static string GetElementName<TDoc>(this TDoc doc, string propertyName) where TDoc : BaseEntity
        {
            var epc = Game.Scene.GetComponent<EntityComponent>();
            return epc.GetElementName<TDoc>(propertyName);
        }
    }
}
