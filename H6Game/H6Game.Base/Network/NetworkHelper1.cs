using H6Game.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public static class NetworkHelper1
{
    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack<T>(this Network1 network, T data) where T :class
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
    public static void RpcCallBack(this Network1 network, string data)
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
    public static void RpcCallBack(this Network1 network, int data)
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
    public static void RpcCallBack(this Network1 network, uint data)
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
    public static void RpcCallBack(this Network1 network, long data)
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
    public static void RpcCallBack(this Network1 network, ulong data)
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
    public static void RpcCallBack(this Network1 network, float data)
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
    public static void RpcCallBack(this Network1 network, double data)
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
    public static void RpcCallBack(this Network1 network, decimal data)
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
    public static void RpcCallBack(this Network1 network, byte data)
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
    public static void RpcCallBack(this Network1 network, sbyte data)
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
    public static void RpcCallBack(this Network1 network, bool data)
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
    public static void RpcCallBack(this Network1 network, char data)
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
    public static void RpcCallBack(this Network1 network, short data)
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
    public static void RpcCallBack(this Network1 network, ushort data)
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
    public static void Send<T>(this Network1 network, T data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this Network1 network, string data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Send(this Network1 network, int data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Send(this Network1 network, uint data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Send(this Network1 network, long data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Send(this Network1 network, ulong data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Send(this Network1 network, float data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Send(this Network1 network, double data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Send(this Network1 network, decimal data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Send(this Network1 network, byte data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Send(this Network1 network, sbyte data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Send(this Network1 network, bool data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Send(this Network1 network, char data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Send(this Network1 network, short data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Send(this Network1 network, ushort data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void RpcCall<T>(this Network1 network, T data, Action<Packet> notificationAction
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
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this Network1 network, string data, Action<Packet> notificationAction
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
    public static void RpcCall(this Network1 network, int data, Action<Packet> notificationAction
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
    public static void RpcCall(this Network1 network, uint data, Action<Packet> notificationAction
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
    public static void RpcCall(this Network1 network, bool data, Action<Packet> notificationAction
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
    public static void RpcCall(this Network1 network, long data, Action<Packet> notificationAction
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
    public static void RpcCall(this Network1 network, ulong data, Action<Packet> notificationAction
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
    public static void RpcCall(this Network1 network, float data, Action<Packet> notificationAction
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
    public static void RpcCall(this Network1 network, double data, Action<Packet> notificationAction
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
    public static void RpcCall(this Network1 network, decimal data, Action<Packet> notificationAction
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
    public static void RpcCall(this Network1 network, byte data, Action<Packet> notificationAction
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
    public static void RpcCall(this Network1 network, sbyte data, Action<Packet> notificationAction
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
    public static void RpcCall(this Network1 network, char data, Action<Packet> notificationAction
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
    public static void RpcCall(this Network1 network, short data, Action<Packet> notificationAction
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
    public static void RpcCall(this Network1 network, ushort data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    /// <returns></returns>
    public static Task<Tuple<T,bool>> CallMessage<T>(this Network1 network, T data, int messageCmd, bool isCompress = false, bool isEncrypt = false)
        where T : class
    {
        var tcs = new TaskCompletionSource<Tuple<T, bool>>();
        network.RpcCall(data, (p) =>
        {
            Tuple<T, bool> tuple;
            if (p.TryRead(out T response))
            {
                tuple = new Tuple<T, bool>(response, true);
            }
            else
            {
                tuple = new Tuple<T, bool>(response, false);
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
    public static void Broadcast<T>(this Network1 network, T data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
        where T : class
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
    public static void Broadcast(this Network1 network, string data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Broadcast(this Network1 network, int data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Broadcast(this Network1 network, uint data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Broadcast(this Network1 network, bool data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Broadcast(this Network1 network, float data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Broadcast(this Network1 network, double data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Broadcast(this Network1 network, decimal data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Broadcast(this Network1 network, long data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Broadcast(this Network1 network, ulong data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Broadcast(this Network1 network, byte data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Broadcast(this Network1 network, sbyte data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Broadcast(this Network1 network, short data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Broadcast(this Network1 network, ushort data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
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
    public static void Broadcast<T>(this IEnumerable<Network1> networks, T data
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
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this IEnumerable<Network1> networks, string data
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
    public static void Broadcast(this IEnumerable<Network1> networks, int data
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
    public static void Broadcast(this IEnumerable<Network1> networks, uint data
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
    public static void Broadcast(this IEnumerable<Network1> networks, bool data
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
    public static void Broadcast(this IEnumerable<Network1> networks, long data
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
    public static void Broadcast(this IEnumerable<Network1> networks, ulong data
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
    public static void Broadcast(this IEnumerable<Network1> networks, float data
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
    public static void Broadcast(this IEnumerable<Network1> networks, double data
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
    public static void Broadcast(this IEnumerable<Network1> networks, decimal data
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
    public static void Broadcast(this IEnumerable<Network1> networks, byte data
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
    public static void Broadcast(this IEnumerable<Network1> networks, sbyte data
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
    public static void Broadcast(this IEnumerable<Network1> networks, char data
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
    public static void Broadcast(this IEnumerable<Network1> networks, short data
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
    public static void Broadcast(this IEnumerable<Network1> networks, ushort data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }
}