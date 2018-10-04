

namespace H6Game.Base
{
    public sealed class Value<T> : IValue
    {
        public T Data;

        private Value(){}
        public static Value<T> Instance { get; } = new Value<T>();

        //免GC装箱
        public Value<T> GetValue(T obj)
        {
            this.Data = obj;
            return this;
        }

        public object GetValue()
        {
            return Data;
        }
    }

    public interface IValue
    {
        object GetValue();
    }

    public static class ValueHelper
    {
        public static object GetValue(object obj)
        {
            var value = (IValue)obj;
            return value.GetValue();
        }
    }
}
