using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public sealed class ValueObject<T>
    {
        public T Value;

        private ValueObject(){}
        public static ValueObject<T> Instance { get; } = new ValueObject<T>();

        //免GC装箱
        public ValueObject<T> GetValue(T value)
        {
            this.Value = value;
            return this;
        }
    }
}
