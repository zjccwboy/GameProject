
namespace H6Game.Base
{
    public class CallResult<T>
    {
        public T Content { get; set; }
        public bool Result { get; set; }

        public CallResult(T content, bool result)
        {
            this.Content = content;
            this.Result = result;
        }
    }
}
