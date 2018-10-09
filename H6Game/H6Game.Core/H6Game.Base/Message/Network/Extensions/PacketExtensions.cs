using H6Game.Base;
using ProtoBuf;
using System;

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
        var netIntVal = Convert.ToInt32(value);
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
        return MessageCommandStorage.GetMsgCode(type);
    }

    public static T Read<T>(this Packet packet)
    {
        if (packet.BodyStream.Length == 0)
            return default;

        var result = Serializer.Deserialize<T>(packet.BodyStream);
        packet.BodyStream.Seek(0, System.IO.SeekOrigin.Begin);
        return result;
    }

    public static object Read(this Packet packet, Type type, out Type valueType)
    {
        valueType = type;
        if (packet.BodyStream.Length == 0)
            return null;

        object data = null; 
        if (type == typeof(int))
        {
            valueType = typeof(MyInt32);
            data = (MyInt32)Serializer.Deserialize<int>(packet.BodyStream);
        }
        else if (type == typeof(uint))
        {
            valueType = typeof(MyUInt32);
            data = (MyUInt32)Serializer.Deserialize<uint>(packet.BodyStream);
        }
        else if (type == typeof(long))
        {
            valueType = typeof(MyLong);
            data = (MyLong)Serializer.Deserialize<long>(packet.BodyStream);
        }
        else if (type == typeof(ulong))
        {
            valueType = typeof(MyULong);
            data = (MyULong)Serializer.Deserialize<ulong>(packet.BodyStream);
        }
        else if (type == typeof(float))
        {
            valueType = typeof(MyFloat);
            data = (MyFloat)Serializer.Deserialize<float>(packet.BodyStream);
        }
        else if (type == typeof(decimal))
        {
            valueType = typeof(MyDecimal);
            data = (MyDecimal)Serializer.Deserialize<decimal>(packet.BodyStream);
        }
        else if (type == typeof(double))
        {
            valueType = typeof(MyDouble);
            data = (MyDouble)Serializer.Deserialize<double>(packet.BodyStream);
        }
        else if (type == typeof(byte))
        {
            valueType = typeof(MyByte);
            data = (MyByte)Serializer.Deserialize<byte>(packet.BodyStream);
        }
        else if (type == typeof(sbyte))
        {
            valueType = typeof(MySByte);
            data = (MySByte)Serializer.Deserialize<sbyte>(packet.BodyStream);
        }
        else if (type == typeof(bool))
        {
            valueType = typeof(MyBoolean);
            data = (MyBoolean)Serializer.Deserialize<bool>(packet.BodyStream);
        }
        else if (type == typeof(short))
        {
            valueType = typeof(MyShort);
            data = (MyShort)Serializer.Deserialize<short>(packet.BodyStream);
        }
        else if (type == typeof(ushort))
        {
            valueType = typeof(MyUShort);
            data = (MyUShort)Serializer.Deserialize<ushort>(packet.BodyStream);
        }
        else if (type == typeof(char))
        {
            valueType = typeof(MyChar);
            data = (MyChar)Serializer.Deserialize<char>(packet.BodyStream);
        }
        else if (type == typeof(DateTime))
        {
            valueType = typeof(MyDateTime);
            data = (MyDateTime)Serializer.Deserialize<DateTime>(packet.BodyStream);
        }
        else if (type == typeof(Guid))
        {
            valueType = typeof(MyGuid);
            data = (MyGuid)Serializer.Deserialize<Guid>(packet.BodyStream);
        }
        else
        {
            data = Serializer.Deserialize(type, packet.BodyStream);
        }
        packet.BodyStream.Seek(0, System.IO.SeekOrigin.Begin);
        return data;
    }
}