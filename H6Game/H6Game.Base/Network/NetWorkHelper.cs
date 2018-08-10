using H6Game.Base;
using System;
using System.Collections.Generic;
using System.Text;

public static class NetWorkHelper
{
    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack<T>(this NetWork newWork, T data)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        var messageCmd = newWork.RecvPacket.MessageId;
        var rpcId = newWork.RecvPacket.RpcId;
        var actorId = newWork.RecvPacket.ActorId;
        var isCompress = newWork.RecvPacket.IsCompress;
        var isEncrypt = newWork.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this NetWork newWork, string data)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        var messageCmd = newWork.RecvPacket.MessageId;
        var rpcId = newWork.RecvPacket.RpcId;
        var actorId = newWork.RecvPacket.ActorId;
        var isCompress = newWork.RecvPacket.IsCompress;
        var isEncrypt = newWork.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this NetWork newWork, int data)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        var messageCmd = newWork.RecvPacket.MessageId;
        var rpcId = newWork.RecvPacket.RpcId;
        var actorId = newWork.RecvPacket.ActorId;
        var isCompress = newWork.RecvPacket.IsCompress;
        var isEncrypt = newWork.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this NetWork newWork, uint data)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        var messageCmd = newWork.RecvPacket.MessageId;
        var rpcId = newWork.RecvPacket.RpcId;
        var actorId = newWork.RecvPacket.ActorId;
        var isCompress = newWork.RecvPacket.IsCompress;
        var isEncrypt = newWork.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this NetWork newWork, long data)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        var messageCmd = newWork.RecvPacket.MessageId;
        var rpcId = newWork.RecvPacket.RpcId;
        var actorId = newWork.RecvPacket.ActorId;
        var isCompress = newWork.RecvPacket.IsCompress;
        var isEncrypt = newWork.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this NetWork newWork, ulong data)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        var messageCmd = newWork.RecvPacket.MessageId;
        var rpcId = newWork.RecvPacket.RpcId;
        var actorId = newWork.RecvPacket.ActorId;
        var isCompress = newWork.RecvPacket.IsCompress;
        var isEncrypt = newWork.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this NetWork newWork, float data)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        var messageCmd = newWork.RecvPacket.MessageId;
        var rpcId = newWork.RecvPacket.RpcId;
        var actorId = newWork.RecvPacket.ActorId;
        var isCompress = newWork.RecvPacket.IsCompress;
        var isEncrypt = newWork.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this NetWork newWork, double data)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        var messageCmd = newWork.RecvPacket.MessageId;
        var rpcId = newWork.RecvPacket.RpcId;
        var actorId = newWork.RecvPacket.ActorId;
        var isCompress = newWork.RecvPacket.IsCompress;
        var isEncrypt = newWork.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this NetWork newWork, decimal data)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        var messageCmd = newWork.RecvPacket.MessageId;
        var rpcId = newWork.RecvPacket.RpcId;
        var actorId = newWork.RecvPacket.ActorId;
        var isCompress = newWork.RecvPacket.IsCompress;
        var isEncrypt = newWork.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this NetWork newWork, byte data)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        var messageCmd = newWork.RecvPacket.MessageId;
        var rpcId = newWork.RecvPacket.RpcId;
        var actorId = newWork.RecvPacket.ActorId;
        var isCompress = newWork.RecvPacket.IsCompress;
        var isEncrypt = newWork.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this NetWork newWork, sbyte data)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        var messageCmd = newWork.RecvPacket.MessageId;
        var rpcId = newWork.RecvPacket.RpcId;
        var actorId = newWork.RecvPacket.ActorId;
        var isCompress = newWork.RecvPacket.IsCompress;
        var isEncrypt = newWork.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this NetWork newWork, bool data)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        var messageCmd = newWork.RecvPacket.MessageId;
        var rpcId = newWork.RecvPacket.RpcId;
        var actorId = newWork.RecvPacket.ActorId;
        var isCompress = newWork.RecvPacket.IsCompress;
        var isEncrypt = newWork.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this NetWork newWork, char data)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        var messageCmd = newWork.RecvPacket.MessageId;
        var rpcId = newWork.RecvPacket.RpcId;
        var actorId = newWork.RecvPacket.ActorId;
        var isCompress = newWork.RecvPacket.IsCompress;
        var isEncrypt = newWork.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this NetWork newWork, short data)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        var messageCmd = newWork.RecvPacket.MessageId;
        var rpcId = newWork.RecvPacket.RpcId;
        var actorId = newWork.RecvPacket.ActorId;
        var isCompress = newWork.RecvPacket.IsCompress;
        var isEncrypt = newWork.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">回发数据</param>
    public static void RpcCallBack(this NetWork newWork, ushort data)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        var messageCmd = newWork.RecvPacket.MessageId;
        var rpcId = newWork.RecvPacket.RpcId;
        var actorId = newWork.RecvPacket.ActorId;
        var isCompress = newWork.RecvPacket.IsCompress;
        var isEncrypt = newWork.RecvPacket.IsEncrypt;
        session.Send(channel, data, messageCmd, rpcId, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send<T>(this NetWork newWork, T data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this NetWork newWork, string data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this NetWork newWork, int data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this NetWork newWork, uint data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this NetWork newWork, long data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this NetWork newWork, ulong data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this NetWork newWork, float data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this NetWork newWork, double data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this NetWork newWork, decimal data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this NetWork newWork, byte data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this NetWork newWork, sbyte data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this NetWork newWork, bool data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this NetWork newWork, char data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this NetWork newWork, short data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否对数据包加密</param>
    public static void Send(this NetWork newWork, ushort data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Send(channel, data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall<T>(this NetWork newWork, T data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this NetWork newWork, string data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this NetWork newWork, int data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this NetWork newWork, uint data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this NetWork newWork, bool data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this NetWork newWork, long data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this NetWork newWork, ulong data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this NetWork newWork, float data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this NetWork newWork, double data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this NetWork newWork, decimal data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this NetWork newWork, byte data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this NetWork newWork, sbyte data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this NetWork newWork, char data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this NetWork newWork, short data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress">是否压缩数据包</param>
    /// <param name="isEncrypt">是否加密数据包</param>
    public static void RpcCall(this NetWork newWork, ushort data, Action<Packet> notificationAction
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        var channel = newWork.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast<T>(this NetWork newWork, T data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, string data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, int data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, uint data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, bool data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, float data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, double data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, decimal data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, long data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, ulong data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, byte data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, sbyte data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, short data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWork">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, ushort data, int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        var session = newWork.Session;
        session.Broadcast(data, messageCmd, 0, actorId, isCompress, isEncrypt);
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWorks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast<T>(this IEnumerable<NetWork> newWorks, T data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in newWorks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWorks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, IEnumerable<NetWork> newWorks, string data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in newWorks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWorks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, IEnumerable<NetWork> newWorks, int data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in newWorks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWorks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, IEnumerable<NetWork> newWorks, uint data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in newWorks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWorks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, IEnumerable<NetWork> newWorks, bool data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in newWorks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWorks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, IEnumerable<NetWork> newWorks, long data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in newWorks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWorks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, IEnumerable<NetWork> newWorks, ulong data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in newWorks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWorks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, IEnumerable<NetWork> newWorks, float data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in newWorks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWorks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, IEnumerable<NetWork> newWorks, double data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in newWorks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWorks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, IEnumerable<NetWork> newWorks, decimal data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in newWorks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWorks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, IEnumerable<NetWork> newWorks, byte data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in newWorks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWorks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, IEnumerable<NetWork> newWorks, sbyte data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in newWorks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWorks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, IEnumerable<NetWork> newWorks, char data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in newWorks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWorks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, IEnumerable<NetWork> newWorks, short data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in newWorks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newWorks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    /// <param name="isCompress"></param>
    /// <param name="isEncrypt"></param>
    public static void Broadcast(this NetWork newWork, IEnumerable<NetWork> newWorks, ushort data
        , int messageCmd, int actorId = 0, bool isCompress = false, bool isEncrypt = false)
    {
        foreach (var net in newWorks)
        {
            net.Send(data, messageCmd, actorId, isCompress, isEncrypt);
        }
    }
}