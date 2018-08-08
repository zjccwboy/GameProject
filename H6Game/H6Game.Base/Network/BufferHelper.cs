using H6Game.Base;
using ProtoBuf;
using System;
using System.Text;

public static class BufferHelper
{
    public static void WriteTo<T>(this Packet packet, T obj)
    {
        if (obj != default)
            Serializer.Serialize(packet.BodyStream, obj);

        packet.WriteBuffer();
    }

    public static void WriteTo(this Packet packet, string data)
    {
        if (data == null)
            return;

        var bytes = Encoding.UTF8.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.WriteBuffer();
    }

    public static void WriteTo(this Packet packet, int data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.WriteBuffer();
    }

    public static void WriteTo(this Packet packet, uint data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.WriteBuffer();
    }

    public static void WriteTo(this Packet packet, bool data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.WriteBuffer();
    }

    public static void WriteTo(this Packet packet, long data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.WriteBuffer();
    }

    public static void WriteTo(this Packet packet, ulong data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.WriteBuffer();
    }

    public static void WriteTo(this Packet packet, float data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.WriteBuffer();
    }

    public static void WriteTo(this Packet packet, double data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.WriteBuffer();
    }

    public static void WriteTo(this Packet packet, decimal data)
    {
        var bytes = BitConverter.GetBytes((double)data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.WriteBuffer();
    }

    public static void WriteTo(this Packet packet, byte data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.WriteBuffer();
    }

    public static void WriteTo(this Packet packet, sbyte data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.WriteBuffer();
    }

    public static void WriteTo(this Packet packet, char data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.WriteBuffer();
    }

    public static void WriteTo(this Packet packet, short data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.WriteBuffer();
    }

    public static void WriteTo(this Packet packet, ushort data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.WriteBuffer();
    }

    public static T Read<T>(this Packet packet)
    {
        if (packet == null)
            return default;

        if (packet.BodyStream.Length == 0)
            return default;

        var type = typeof(T);
        if (TryGetValueType(packet, type, out object obj))
        {
            var objVal = obj as ValueObject<T>;
            return objVal.Value;
        }
        return Serializer.Deserialize<T>(packet.BodyStream);
    }

    private static Type SType = typeof(string);
    private static bool TryGetValueType(Packet packet, Type type, out object value)
    {
        var bytes = packet.BodyStream.GetBuffer();
        if (type == SType)
        {
            var data = Encoding.UTF8.GetString(bytes, 0, (int)packet.BodyStream.Length);
            value = ValueObject<string>.Instance.GetValue(data);
            return true;
        }

        TypeCode code = Type.GetTypeCode(type);
        switch (code)
        {
            case TypeCode.Int32:
                {
                    var data = BitConverter.ToInt32(bytes, 0);
                    value = ValueObject<int>.Instance.GetValue(data);
                    return true;
                }
            case TypeCode.UInt32:
                {
                    var data = BitConverter.ToUInt32(bytes, 0);
                    value = ValueObject<uint>.Instance.GetValue(data);
                    return true;
                }
            case TypeCode.Boolean:
                {
                    var data = BitConverter.ToBoolean(bytes, 0);
                    value = ValueObject<bool>.Instance.GetValue(data);
                    return true;
                }
            case TypeCode.Int64:
                {
                    var data = BitConverter.ToInt64(bytes, 0);
                    value = ValueObject<long>.Instance.GetValue(data);
                    return true;
                }
            case TypeCode.UInt64:
                {
                    var data = BitConverter.ToUInt64(bytes, 0);
                    value = ValueObject<ulong>.Instance.GetValue(data);
                    return true;
                }
            case TypeCode.Single:
                {
                    var data = BitConverter.ToSingle(bytes, 0);
                    value = ValueObject<float>.Instance.GetValue(data);
                    return true;
                }
            case TypeCode.Double:
            case TypeCode.Decimal:
                {
                    var data = BitConverter.ToDouble(bytes, 0);
                    value = ValueObject<double>.Instance.GetValue(data);
                    return true;
                }
            case TypeCode.Byte:
            case TypeCode.SByte:
                value = bytes[0];
                return true;
            case TypeCode.Char:
                {
                    var data = BitConverter.ToChar(bytes, 0);
                    value = ValueObject<char>.Instance.GetValue(data);
                    return true;
                }
            case TypeCode.Int16:
                {
                    var data = BitConverter.ToInt16(bytes, 0);
                    value = ValueObject<short>.Instance.GetValue(data);
                    return true;
                }
            case TypeCode.UInt16:
                {
                    var data = BitConverter.ToUInt16(bytes, 0);
                    value = ValueObject<ushort>.Instance.GetValue(data);
                    return true;
                }
        }
        value = default;
        return false;
    }
}