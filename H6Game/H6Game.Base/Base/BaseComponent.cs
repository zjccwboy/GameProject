
namespace H6Game.Base
{
    public abstract class BaseComponent
    {
        public uint ComponentId { get; set; }

        public void Close()
        {
            this.PutBack();
        }
    }
}
