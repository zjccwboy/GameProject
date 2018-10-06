using System;

namespace H6Game.Base
{
    public sealed class MyInt32 : Value<int>
    {
        private static MyInt32 Instace { get; } = new MyInt32();

        public static explicit operator MyInt32(int data)
        {
            Instace.Data = data;
            return Instace;
        }

        public static explicit operator int(MyInt32 value)
        {
            return value.Data;
        }
    }

    public sealed class MyUInt32 : Value<uint>
    {
        private static MyUInt32 Instace { get; } = new MyUInt32();

        public static explicit operator MyUInt32(uint data)
        {
            Instace.Data = data;
            return Instace;
        }

        public static explicit operator uint(MyUInt32 value)
        {
            return value.Data;
        }
    }

    public sealed class MyLong : Value<long>
    {
        private static MyLong Instace { get; } = new MyLong();

        public static explicit operator MyLong(long data)
        {
            Instace.Data = data;
            return Instace;
        }

        public static explicit operator long(MyLong value)
        {
            return value.Data;
        }
    }

    public sealed class MyULong : Value<ulong>
    {
        private static MyULong Instace { get; } = new MyULong();

        public static explicit operator MyULong(ulong data)
        {
            Instace.Data = data;
            return Instace;
        }

        public static explicit operator ulong(MyULong value)
        {
            return value.Data;
        }
    }

    public sealed class MyFloat : Value<float>
    {
        private static MyFloat Instace { get; } = new MyFloat();

        public static explicit operator MyFloat(float data)
        {
            Instace.Data = data;
            return Instace;
        }

        public static explicit operator float(MyFloat value)
        {
            return value.Data;
        }
    }

    public sealed class MyDouble : Value<double>
    {
        private static MyDouble Instace { get; } = new MyDouble();

        public static explicit operator MyDouble(double data)
        {
            Instace.Data = data;
            return Instace;
        }

        public static explicit operator double(MyDouble value)
        {
            return value.Data;
        }
    }

    public sealed class MyDecimal : Value<decimal>
    {
        private static MyDecimal Instace { get; } = new MyDecimal();

        public static explicit operator MyDecimal(decimal data)
        {
            Instace.Data = data;
            return Instace;
        }

        public static explicit operator decimal(MyDecimal value)
        {
            return value.Data;
        }
    }

    public sealed class MyChar : Value<char>
    {
        private static MyChar Instace { get; } = new MyChar();

        public static explicit operator MyChar(char data)
        {
            Instace.Data = data;
            return Instace;
        }

        public static explicit operator char(MyChar value)
        {
            return value.Data;
        }
    }

    public sealed class MyByte : Value<byte>
    {
        private static MyByte Instace { get; } = new MyByte();

        public static explicit operator MyByte(byte data)
        {
            Instace.Data = data;
            return Instace;
        }

        public static explicit operator byte(MyByte value)
        {
            return value.Data;
        }
    }

    public sealed class MySByte : Value<sbyte>
    {
        private static MySByte Instace { get; } = new MySByte();

        public static explicit operator MySByte(sbyte data)
        {
            Instace.Data = data;
            return Instace;
        }

        public static explicit operator sbyte(MySByte value)
        {
            return value.Data;
        }
    }

    public sealed class MyShort : Value<short>
    {
        private static MyShort Instace { get; } = new MyShort();

        public static explicit operator MyShort(short data)
        {
            Instace.Data = data;
            return Instace;
        }

        public static explicit operator short(MyShort value)
        {
            return value.Data;
        }
    }

    public sealed class MyUShort : Value<ushort>
    {
        private static MyUShort Instace { get; } = new MyUShort();

        public static explicit operator MyUShort(ushort data)
        {
            Instace.Data = data;
            return Instace;
        }

        public static explicit operator ushort(MyUShort value)
        {
            return value.Data;
        }
    }

    public sealed class MyBoolean : Value<bool>
    {
        private static MyBoolean Instace { get; } = new MyBoolean();

        public static explicit operator MyBoolean(bool data)
        {
            Instace.Data = data;
            return Instace;
        }

        public static explicit operator bool(MyBoolean value)
        {
            return value.Data;
        }
    }

    public sealed class MyGuid : Value<Guid>
    {
        private static MyGuid Instace { get; } = new MyGuid();

        public static explicit operator MyGuid(Guid data)
        {
            Instace.Data = data;
            return Instace;
        }

        public static explicit operator Guid(MyGuid value)
        {
            return value.Data;
        }
    }

    public sealed class MyDateTime : Value<DateTime>
    {
        private static MyDateTime Instace { get; } = new MyDateTime();

        public static explicit operator MyDateTime(DateTime data)
        {
            Instace.Data = data;
            return Instace;
        }

        public static explicit operator DateTime(MyDateTime value)
        {
            return value.Data;
        }
    }

    public abstract class Value<T> : IValue
    {
        public T Data { get; set; }

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
        /// <summary>
        /// 调用该方法会产生GC，订阅者控制器值类型可以使用My开头的值类型避免GC开销。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object GetValue(object obj)
        {
            var value = (IValue)obj;
            return value.GetValue();
        }
    }
}
