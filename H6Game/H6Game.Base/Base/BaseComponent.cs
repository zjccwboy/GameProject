
namespace H6Game.Base
{
    public abstract class BaseComponent
    {
        public abstract void Start();

        public int Id { get; set; }
        public void Close()
        {
            this.PutBack();
        }
    }
}
