using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class ValueObject<T>
    {
        public T Value;
        public static ValueObject<T> Instance { get; } = new ValueObject<T>();

        //免GC装箱
        public ValueObject<T> GetValue(T value)
        {
            this.Value = value;
            return this;
        }
    }
}
