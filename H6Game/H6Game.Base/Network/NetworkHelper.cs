using H6Game.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class NetworkHelper
{
    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack<T>(this Network network, T data) where T :class
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageId;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        var isCompress = network.RecvPacket.IsCompress;
        var isEncrypt = network.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this Network network, string data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageId;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        var isCompress = network.RecvPacket.IsCompress;
        var isEncrypt = network.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this Network network, int data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageId;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        var isCompress = network.RecvPacket.IsCompress;
        var isEncrypt = network.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this Network network, uint data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageId;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        var isCompress = network.RecvPacket.IsCompress;
        var isEncrypt = network.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this Network network, long data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageId;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        var isCompress = network.RecvPacket.IsCompress;
        var isEncrypt = network.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this Network network, ulong data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageId;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        var isCompress = network.RecvPacket.IsCompress;
        var isEncrypt = network.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this Network network, float data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageId;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        var isCompress = network.RecvPacket.IsCompress;
        var isEncrypt = network.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this Network network, double data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageId;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        var isCompress = network.RecvPacket.IsCompress;
        var isEncrypt = network.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this Network network, decimal data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageId;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        var isCompress = network.RecvPacket.IsCompress;
        var isEncrypt = network.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this Network network, byte data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageId;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        var isCompress = network.RecvPacket.IsCompress;
        var isEncrypt = network.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this Network network, sbyte data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageId;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        var isCompress = network.RecvPacket.IsCompress;
        var isEncrypt = network.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this Network network, bool data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageId;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        var isCompress = network.RecvPacket.IsCompress;
        var isEncrypt = network.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this Network network, char data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageId;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        var isCompress = network.RecvPacket.IsCompress;
        var isEncrypt = network.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this Network network, short data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageId;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        var isCompress = network.RecvPacket.IsCompress;
        var isEncrypt = network.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this Network network, ushort data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageId;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        var isCompress = network.RecvPacket.IsCompress;
        var isEncrypt = network.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send<T>(this Network network, T data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
        where T : class
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this Network network, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, null, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this Network network, string data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this Network network, int data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this Network network, uint data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this Network network, long data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this Network network, ulong data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this Network network, float data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this Network network, double data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this Network network, decimal data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this Network network, byte data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this Network network, sbyte data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this Network network, bool data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this Network network, char data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this Network network, short data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this Network network, ushort data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall<T>(this Network network, T data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false) where T : class

    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this Network network, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)

    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, null, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this Network network, string data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this Network network, int data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this Network network, uint data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this Network network, bool data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this Network network, long data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this Network network, ulong data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this Network network, float data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this Network network, double data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this Network network, decimal data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this Network network, byte data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this Network network, sbyte data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this Network network, char data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this Network network, short data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this Network network, ushort data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Rquest"></typeparam>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    /// <returns></returns>
    public static Task<Tuple<Response, bool>> CallMessage<Rquest,Response>(this Network network, Rquest data, int messageCmd, bool isCompress = false, bool isEncrypt = false)
        where Rquest : class
    {
        var tcs = new TaskCompletionSource<Tuple<Response, bool>>();
        network.RpcCall(data, (p) =>
        {
            Tuple<Response, bool> tuple;
            if (p.TryRead(out Response response))
            {
                tuple = new Tuple<Response, bool>(response, true);
            }
            else
            {
                tuple = new Tuple<Response, bool>(response, false);
            }
            tcs.TrySetResult(tuple);
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="messageCmd"></param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    /// <returns></returns>
    public static Task<Tuple<Response, bool>> CallMessage<Response>(this Network network, int messageCmd, bool isCompress = false, bool isEncrypt = false)
    {
        var tcs = new TaskCompletionSource<Tuple<Response, bool>>();
        network.RpcCall((p) =>
        {
            Tuple<Response, bool> tuple;
            if (p.TryRead(out Response response))
            {
                tuple = new Tuple<Response, bool>(response, true);
            }
            else
            {
                tuple = new Tuple<Response, bool>(response, false);
            }
            tcs.TrySetResult(tuple);
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast<T>(this Network network, T data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
        where T : class
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this Network network, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        session.Broadcast(null, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this Network network, string data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this Network network, int data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this Network network, uint data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this Network network, bool data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this Network network, float data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this Network network, double data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this Network network, decimal data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this Network network, long data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this Network network, ulong data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this Network network, byte data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this Network network, sbyte data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this Network network, short data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this Network network, ushort data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast<T>(this IEnumerable<Network> networks, T data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
        where T : class
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this IEnumerable<Network> networks
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in networks)
        {
            net.Send(messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this IEnumerable<Network> networks, string data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this IEnumerable<Network> networks, int data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this IEnumerable<Network> networks, uint data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this IEnumerable<Network> networks, bool data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this IEnumerable<Network> networks, long data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this IEnumerable<Network> networks, ulong data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this IEnumerable<Network> networks, float data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this IEnumerable<Network> networks, double data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this IEnumerable<Network> networks, decimal data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this IEnumerable<Network> networks, byte data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this IEnumerable<Network> networks, sbyte data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this IEnumerable<Network> networks, char data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this IEnumerable<Network> networks, short data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this IEnumerable<Network> networks, ushort data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }
}