using H6Game.Base;
using H6Game.Base.Message;
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
        packet.MsgTypeCode.WriteTo(packet.HeadBytes, 7);

        //写RpcId
        packet.RpcId.WriteTo(packet.HeadBytes, 9);

        return packet.HeadBytes;
    }

    internal static void WriteTo(this ushort value, byte[] bytes, int offset)
    {
        for (var i = 0; i < 2; i++)
        {
            bytes[i + offset] = (byte)(value >> i * 8);
        }
    }

    internal static void WriteTo(this int value, byte[] bytes, int offset)
    {
        for (var i = 0; i < 4; i++)
        {
            bytes[i + offset] = (byte)(value >> i * 8);
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
        if (type.BaseType == typeof(Enum))
            type = typeof(int);
        packet.MsgTypeCode = (ushort)MessageCommandStorage.GetMsgCode(type);
        packet.WriteBuffer();
    }

    internal static void WriteTo(this Packet packet, object obj, Type type)
    {
        if (obj != default)
            Serializer.Serialize(packet.BodyStream, obj);

        if (type.BaseType == typeof(Enum))
            type = typeof(int);
        packet.MsgTypeCode = (ushort)MessageCommandStorage.GetMsgCode(type);
        packet.WriteBuffer();
    }

    internal static T Read<T>(this Packet packet)
    {
        if (packet.BodyStream.Length == 0)
            return default;

        var result = Serializer.Deserialize<T>(packet.BodyStream);
        packet.BodyStream.Seek(0, System.IO.SeekOrigin.Begin);
        return result;
    }
}