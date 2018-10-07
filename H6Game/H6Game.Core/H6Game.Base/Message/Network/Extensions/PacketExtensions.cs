using H6Game.Base;
using ProtoBuf;
using System;
using System.Net;

internal static class PacketExtensions
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
        packet.NetCommand.WriteTo(packet.HeadBytes, 5);

        //写MsgTypeCode
        packet.MsgTypeCode.WriteTo(packet.HeadBytes, 9);

        //写RpcId
        packet.RpcId.WriteTo(packet.HeadBytes, 13);

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

    internal static void WriteTo<T>(this Packet packet, T obj)
    {
        if (obj != default)
            Serializer.Serialize(packet.BodyStream, obj);

        var type = obj.GetType();
        packet.MsgTypeCode = GetTypeCode(type);
        packet.WriteBuffer();
    }

    private static int GetTypeCode(Type type)
    {
        return MessageCommandPool.GetMsgCode(type);
    }

    public static T Read<T>(this Packet packet)
    {
        if (packet.BodyStream.Length == 0)
            return default;

        var result = Serializer.Deserialize<T>(packet.BodyStream);
        packet.BodyStream.Seek(0, System.IO.SeekOrigin.Begin);
        return result;
    }

    public static bool TryRead<T>(this Packet packet, out T data)
    {
        if (packet.BodyStream.Length == 0)
        {
            data = default;
            return false;
        }

        data = Serializer.Deserialize<T>(packet.BodyStream);
        packet.BodyStream.Seek(0, System.IO.SeekOrigin.Begin);
        return true;
    }

    public static bool TryRead(this Packet packet, Type type, out object data)
    {
        if (packet.BodyStream.Length == 0)
        {
            data = default;
            return false;
        }

        else if (type == typeof(int))
        {
            data = (MyInt32)Serializer.Deserialize<int>(packet.BodyStream);
        }
        else if (type == typeof(uint))
        {
            data = (MyUInt32)Serializer.Deserialize<uint>(packet.BodyStream);
        }
        else if (type == typeof(long))
        {
            data = (MyLong)Serializer.Deserialize<long>(packet.BodyStream);
        }
        else if (type == typeof(ulong))
        {
            data = (MyULong)Serializer.Deserialize<ulong>(packet.BodyStream);
        }
        else if (type == typeof(float))
        {
            data = (MyFloat)Serializer.Deserialize<float>(packet.BodyStream);
        }
        else if (type == typeof(decimal))
        {
            data = (MyDecimal)Serializer.Deserialize<decimal>(packet.BodyStream);
        }
        else if (type == typeof(double))
        {
            data = (MyDouble)Serializer.Deserialize<double>(packet.BodyStream);
        }
        else if (type == typeof(byte))
        {
            data = (MyByte)Serializer.Deserialize<byte>(packet.BodyStream);
        }
        else if (type == typeof(sbyte))
        {
            data = (MySByte)Serializer.Deserialize<sbyte>(packet.BodyStream);
        }
        else if (type == typeof(bool))
        {
            data = (MyBoolean)Serializer.Deserialize<bool>(packet.BodyStream);
        }
        else if (type == typeof(short))
        {
            data = (MyShort)Serializer.Deserialize<short>(packet.BodyStream);
        }
        else if (type == typeof(ushort))
        {
            data = (MyUShort)Serializer.Deserialize<ushort>(packet.BodyStream);
        }
        else if (type == typeof(char))
        {
            data = (MyChar)Serializer.Deserialize<char>(packet.BodyStream);
        }
        else if(type == typeof(DateTime))
        {
            data = (MyDateTime)Serializer.Deserialize<DateTime>(packet.BodyStream);
        }
        else if(type == typeof(Guid))
        {
            data = (MyGuid)Serializer.Deserialize<Guid>(packet.BodyStream);
        }
        else
        {
            data = Serializer.Deserialize(type, packet.BodyStream);
        }
        packet.BodyStream.Seek(0, System.IO.SeekOrigin.Begin);
        return true;
    }
}