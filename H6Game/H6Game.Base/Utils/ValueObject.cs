﻿using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class ValueObject<T>
    {
        private Dictionary<TypeCode, ValueObject<T>> ValueTypes = new Dictionary<TypeCode, ValueObject<T>>();

        public T Value;

        public static ValueObject<T> Instance { get; } = new ValueObject<T>();

        //免GC装箱
        public ValueObject<T> GetValue(T value)
        {
            lock (this)
            {
                var code = Type.GetTypeCode(value.GetType());
                if (!ValueTypes.TryGetValue(code, out ValueObject<T> valueObj))
                {
                    valueObj = new ValueObject<T>();
                    ValueTypes[code] = valueObj;
                }
                valueObj.Value = value;
                return valueObj;
            }
        }
    }
}
