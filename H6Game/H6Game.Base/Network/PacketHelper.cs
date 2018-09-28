using H6Game.Base;
using ProtoBuf;
using System;
using System.Net;
using System.Text;

public static class PacketHelper
{

    /// <summary>
    /// 获取包头的字节数组
    /// </summary>
    /// <returns></returns>
    internal static byte[] GetHeadBytes(this Packet packet, int bodySize)
    {
        //写包大小
        var packetSize = PacketParser.HeadSize + bodySize;
        packetSize.WriteTo(packet.HeadBytes, 0);

        packet.HeadBytes[4] = 0;
        //写标志位
        if (packet.IsHeartbeat)
            packet.HeadBytes[4] |= 1;

        if (packet.IsCompress)
            packet.HeadBytes[4] |= 1 << 1;

        if (packet.IsEncrypt)
            packet.HeadBytes[4] |= 1 << 2;

        if (packet.KcpProtocal > 0)
            packet.HeadBytes[4] |= (byte)(packet.KcpProtocal << 4);

        //写MessageId
        packet.MessageCmd.WriteTo(packet.HeadBytes, 5);

        //写MsgTypeCode
        packet.MsgTypeCode.WriteTo(packet.HeadBytes, 9);

        //写RpcId
        packet.RpcId.WriteTo(packet.HeadBytes, 13);

        //写ActorId
        packet.ActorId.WriteTo(packet.HeadBytes, 17);

        return packet.HeadBytes;
    }

    internal static void WriteTo(this int value, byte[] bytes, int offset)
    {
        var netIntVal = IPAddress.HostToNetworkOrder(Convert.ToInt32(value));
        for (var i = 0; i < 4; i++)
        {
            bytes[i + offset] = (byte)(netIntVal >> i * 8);
        }
    }

    internal static void WriteTo(this Packet packet)
    {
        packet.WriteBuffer();
    }

    internal static void WriteTo<T>(this Packet packet, T obj) where T : class
    {
        if (obj != default)
            Serializer.Serialize(packet.BodyStream, obj);

        packet.MsgTypeCode = GetTypeCode(typeof(T));
        packet.WriteBuffer();
    }

    internal static void WriteTo(this Packet packet, string data)
    {
        if (!string.IsNullOrEmpty(data))
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            packet.BodyStream.Write(bytes, 0, bytes.Length);
        }
        packet.MsgTypeCode = GetTypeCode(typeof(string));
        packet.WriteBuffer();
    }

    internal static void WriteTo(this Packet packet, int data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.MsgTypeCode = GetTypeCode(typeof(int));
        packet.WriteBuffer();
    }

    internal static void WriteTo(this Packet packet, uint data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.MsgTypeCode = GetTypeCode(typeof(uint));
        packet.WriteBuffer();
    }

    internal static void WriteTo(this Packet packet, bool data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.MsgTypeCode = GetTypeCode(typeof(bool));
        packet.WriteBuffer();
    }

    internal static void WriteTo(this Packet packet, long data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.MsgTypeCode = GetTypeCode(typeof(long));
        packet.WriteBuffer();
    }

    internal static void WriteTo(this Packet packet, ulong data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.MsgTypeCode = GetTypeCode(typeof(ulong));
        packet.WriteBuffer();
    }

    internal static void WriteTo(this Packet packet, float data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.MsgTypeCode = GetTypeCode(typeof(float));
        packet.WriteBuffer();
    }

    internal static void WriteTo(this Packet packet, double data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.MsgTypeCode = GetTypeCode(typeof(double));
        packet.WriteBuffer();
    }

    internal static void WriteTo(this Packet packet, decimal data)
    {
        var bytes = BitConverter.GetBytes((double)data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.MsgTypeCode = GetTypeCode(typeof(decimal));
        packet.WriteBuffer();
    }

    internal static void WriteTo(this Packet packet, byte data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.MsgTypeCode = GetTypeCode(typeof(byte));
        packet.WriteBuffer();
    }

    internal static void WriteTo(this Packet packet, sbyte data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.MsgTypeCode = GetTypeCode(typeof(sbyte));
        packet.WriteBuffer();
    }

    internal static void WriteTo(this Packet packet, char data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.MsgTypeCode = GetTypeCode(typeof(char));
        packet.WriteBuffer();
    }

    internal static void WriteTo(this Packet packet, short data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.MsgTypeCode = GetTypeCode(typeof(short));
        packet.WriteBuffer();
    }

    internal static void WriteTo(this Packet packet, ushort data)
    {
        var bytes = BitConverter.GetBytes(data);
        packet.BodyStream.Write(bytes, 0, bytes.Length);
        packet.MsgTypeCode = GetTypeCode(typeof(ushort));
        packet.WriteBuffer();
    }

    private static int GetTypeCode(Type type)
    {
        return SubscriberMsgPool.GetMsgCode(type);
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

        var result = Serializer.Deserialize<T>(packet.BodyStream);
        packet.BodyStream.Seek(0, System.IO.SeekOrigin.Begin);
        return result;
    }

    public static bool TryRead<T>(this Packet packet, out T data)
    {
        if (packet == null)
        {
            data = default;
            return false;
        }

        if (packet.BodyStream.Length == 0)
        {
            data = default;
            return false;
        }

        var type = typeof(T);
        if (TryGetValueType(packet, type, out object obj))
        {
            var objVal = obj as ValueObject<T>;
            data = objVal.Value;
            return true;
        }

        try
        {
            data = Serializer.Deserialize<T>(packet.BodyStream);
            return true;
        }
        catch
        {
            data = default;
            return false;
        }
    }

    private static readonly Type SType = typeof(string);
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