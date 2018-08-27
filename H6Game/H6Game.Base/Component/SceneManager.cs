
namespace H6Game.Base
{
    public sealed class SceneManager : BaseManager
    {
        public override bool Remove(BaseComponent component)
        {
            var result = base.Remove(component);
            if (result)
            {
                Game.Event.Remove(component);
            }
            return result;
        }
    }
}
