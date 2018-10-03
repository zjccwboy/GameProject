using H6Game.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class NetworkHelper
{
    #region Response
    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response<T>(this Network network, T data) where T : class
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageCmd;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        session.Send(channel, data, messageCmd, rpcId, actorId);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response(this Network network, string data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageCmd;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        session.Send(channel, data, messageCmd, rpcId, actorId);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response(this Network network, int data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageCmd;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        session.Send(channel, data, messageCmd, rpcId, actorId);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response(this Network network, uint data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageCmd;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        session.Send(channel, data, messageCmd, rpcId, actorId);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response(this Network network, long data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageCmd;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        session.Send(channel, data, messageCmd, rpcId, actorId);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response(this Network network, ulong data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageCmd;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        session.Send(channel, data, messageCmd, rpcId, actorId);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response(this Network network, float data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageCmd;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        session.Send(channel, data, messageCmd, rpcId, actorId);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response(this Network network, double data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageCmd;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        session.Send(channel, data, messageCmd, rpcId, actorId);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response(this Network network, decimal data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageCmd;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        session.Send(channel, data, messageCmd, rpcId, actorId);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response(this Network network, byte data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageCmd;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        session.Send(channel, data, messageCmd, rpcId, actorId);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response(this Network network, sbyte data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageCmd;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        session.Send(channel, data, messageCmd, rpcId, actorId);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response(this Network network, bool data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageCmd;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        session.Send(channel, data, messageCmd, rpcId, actorId);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response(this Network network, char data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageCmd;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        session.Send(channel, data, messageCmd, rpcId, actorId);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response(this Network network, short data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageCmd;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        session.Send(channel, data, messageCmd, rpcId, actorId);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response(this Network network, ushort data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var messageCmd = network.RecvPacket.MessageCmd;
        var rpcId = network.RecvPacket.RpcId;
        var actorId = network.RecvPacket.ActorId;
        session.Send(channel, data, messageCmd, rpcId, actorId);
    }
    #endregion Response

    #region Send
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    public static void Send<T>(this Network network, T data, int messageCmd)
        where T : class
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    public static void Send(this Network network, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, messageCmd, 0, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    public static void Send(this Network network, string data, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    public static void Send(this Network network, int data, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    public static void Send(this Network network, uint data, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    public static void Send(this Network network, long data, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    public static void Send(this Network network, ulong data, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    public static void Send(this Network network, float data, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    public static void Send(this Network network, double data, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    public static void Send(this Network network, decimal data, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    public static void Send(this Network network, byte data, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    public static void Send(this Network network, sbyte data, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    public static void Send(this Network network, bool data, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    public static void Send(this Network network, char data, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    public static void Send(this Network network, short data, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    public static void Send(this Network network, ushort data, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, 0);
    }
    #endregion Send

    #region SendActor
    /// <summary>
    /// 发送Actor消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void SendActor<T>(this Network network, T data, int messageCmd, int actorId)
        where T : class
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 发送Actor消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void SendActor(this Network network, int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 发送Actor消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void SendActor(this Network network, string data, int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 发送Actor消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void SendActor(this Network network, int data, int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 发送Actor消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void SendActor(this Network network, uint data, int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 发送Actor消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void SendActor(this Network network, long data, int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 发送Actor消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void SendActor(this Network network, ulong data, int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 发送Actor消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void SendActor(this Network network, float data, int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 发送Actor消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void SendActor(this Network network, double data, int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 发送Actor消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void SendActor(this Network network, decimal data, int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 发送Actor消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void SendActor(this Network network, byte data, int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 发送Actor消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void SendActor(this Network network, sbyte data, int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 发送Actor消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void SendActor(this Network network, bool data, int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 发送Actor消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void SendActor(this Network network, char data, int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 发送Actor消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void SendActor(this Network network, short data, int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 发送Actor消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">表示这条消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void SendActor(this Network network, ushort data, int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, messageCmd, 0, actorId);
    }
    #endregion

    #region  CallMessage
    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Request"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage<Request>(this Network network, Request data, Action<Packet> notificationAction, int messageCmd) where Request : class

    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, 0);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage(this Network network, Action<Packet> notificationAction, int messageCmd)

    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, notificationAction, messageCmd, 0);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage(this Network network, string data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, 0);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage(this Network network, int data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, 0);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage(this Network network, uint data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, 0);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage(this Network network, bool data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, 0);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage(this Network network, long data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, 0);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage(this Network network, ulong data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, 0);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage(this Network network, float data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, 0);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage(this Network network, double data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, 0);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage(this Network network, decimal data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, 0);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage(this Network network, byte data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, 0);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage(this Network network, sbyte data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, 0);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage(this Network network, char data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, 0);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage(this Network network, short data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, 0);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage(this Network network, ushort data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, 0);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Request"></typeparam>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage<Request, Response>(this Network network, Request data, Action<Response> notificationAction, int messageCmd) where Request : class
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage<Response>(this Network network, Action<Response> notificationAction, int messageCmd)
    {
        network.CallMessage((p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage<Response>(this Network network, string data, Action<Response> notificationAction, int messageCmd)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage<Response>(this Network network, int data, Action<Response> notificationAction, int messageCmd)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage<Response>(this Network network, uint data, Action<Response> notificationAction, int messageCmd)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage<Response>(this Network network, bool data, Action<Response> notificationAction, int messageCmd)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage<Response>(this Network network, long data, Action<Response> notificationAction, int messageCmd)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage<Response>(this Network network, ulong data, Action<Response> notificationAction, int messageCmd)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage<Response>(this Network network, float data, Action<Response> notificationAction, int messageCmd)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage<Response>(this Network network, double data, Action<Response> notificationAction, int messageCmd)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage<Response>(this Network network, decimal data, Action<Response> notificationAction, int messageCmd)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage<Response>(this Network network, byte data, Action<Response> notificationAction, int messageCmd)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage<Response>(this Network network, sbyte data, Action<Response> notificationAction, int messageCmd)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage<Response>(this Network network, char data, Action<Response> notificationAction, int messageCmd)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage<Response>(this Network network, short data, Action<Response> notificationAction, int messageCmd)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallMessage<Response>(this Network network, ushort data, Action<Response> notificationAction, int messageCmd)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd);
    }

    #endregion CallMessage

    #region CallMessageAsync
    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Rquest">返回消息类型</typeparam>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Rquest, Response>(this Network network, Rquest data, int messageCmd)
        where Rquest : class
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, messageCmd);

        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="messageCmd">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage((p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, string data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, int data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, uint data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, long data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, ulong data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, float data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, double data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, decimal data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, short data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, ushort data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, byte data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, sbyte data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, char data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, bool data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, messageCmd);
        return tcs.Task;
    }
    #endregion

    #region CallActor
    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Request"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<Request>(this Network network, Request data, Action<Packet> notificationAction
        , int messageCmd, int actorId) where Request : class

    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor(this Network network, Action<Packet> notificationAction
        , int messageCmd, int actorId)

    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor(this Network network, string data, Action<Packet> notificationAction
        , int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor(this Network network, int data, Action<Packet> notificationAction
        , int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor(this Network network, uint data, Action<Packet> notificationAction
        , int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor(this Network network, bool data, Action<Packet> notificationAction
        , int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor(this Network network, long data, Action<Packet> notificationAction
        , int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor(this Network network, ulong data, Action<Packet> notificationAction
        , int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor(this Network network, float data, Action<Packet> notificationAction
        , int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor(this Network network, double data, Action<Packet> notificationAction
        , int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor(this Network network, decimal data, Action<Packet> notificationAction
        , int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor(this Network network, byte data, Action<Packet> notificationAction
        , int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor(this Network network, sbyte data, Action<Packet> notificationAction
        , int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor(this Network network, char data, Action<Packet> notificationAction
        , int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor(this Network network, short data, Action<Packet> notificationAction
        , int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor(this Network network, ushort data, Action<Packet> notificationAction
        , int messageCmd, int actorId)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Request"></typeparam>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<Request, Response>(this Network network, Request data, Action<Response> notificationAction
        , int messageCmd, int actorId) where Request : class
    {
        network.CallActor(data, (p) =>
        {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<Response>(this Network network, Action<Response> notificationAction, int messageCmd, int actorId)
    {
        network.CallActor((p) =>
        {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<Response>(this Network network, string data, Action<Response> notificationAction
        , int messageCmd, int actorId)
    {
        network.CallActor(data, (p) =>
        {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<Response>(this Network network, int data, Action<Response> notificationAction
        , int messageCmd, int actorId)
    {
        network.CallActor(data, (p) =>
        {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<Response>(this Network network, uint data, Action<Response> notificationAction
        , int messageCmd, int actorId)
    {
        network.CallActor(data, (p) =>
        {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<Response>(this Network network, bool data, Action<Response> notificationAction
        , int messageCmd, int actorId)
    {
        network.CallActor(data, (p) =>
        {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<Response>(this Network network, long data, Action<Response> notificationAction
        , int messageCmd, int actorId)
    {
        network.CallActor(data, (p) =>
        {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<Response>(this Network network, ulong data, Action<Response> notificationAction
        , int messageCmd, int actorId)
    {
        network.CallActor(data, (p) =>
        {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<Response>(this Network network, float data, Action<Response> notificationAction
        , int messageCmd, int actorId)
    {
        network.CallActor(data, (p) =>
        {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<Response>(this Network network, double data, Action<Response> notificationAction
        , int messageCmd, int actorId)
    {
        network.CallActor(data, (p) =>
        {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<Response>(this Network network, decimal data, Action<Response> notificationAction
        , int messageCmd, int actorId)
    {
        network.CallActor(data, (p) =>
        {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<Response>(this Network network, byte data, Action<Response> notificationAction
        , int messageCmd, int actorId)
    {
        network.CallActor(data, (p) =>
        {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<Response>(this Network network, sbyte data, Action<Response> notificationAction
        , int messageCmd, int actorId)
    {
        network.CallActor(data, (p) =>
        {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<Response>(this Network network, char data, Action<Response> notificationAction
        , int messageCmd, int actorId)
    {
        network.CallActor(data, (p) =>
        {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<Response>(this Network network, short data, Action<Response> notificationAction
        , int messageCmd, int actorId)
    {
        network.CallActor(data, (p) =>
        {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd, actorId);
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<Response>(this Network network, ushort data, Action<Response> notificationAction
        , int messageCmd, int actorId)
    {
        network.CallActor(data, (p) =>
        {
            var response = p.Read<Response>();
            notificationAction(response);
        }, messageCmd, actorId);
    }
    #endregion CallActor

    #region CallActorAsync
    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Rquest">发送数据类型</typeparam>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor Id</param>
    /// <returns>返回消息数据</returns>
    public static Task<CallResult<Response>> CallActorAsync<Rquest, Response>(this Network network, Rquest data, int messageCmd, int actorId)
        where Rquest : class
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallActor(data, (p) =>
        {
            CallResult<Response> result;
            if (p.TryRead(out Response response))
            {
                result = new CallResult<Response>(response, true);
            }
            else
            {
                result = new CallResult<Response>(response, false);
            }
            tcs.TrySetResult(result);
        }, messageCmd, actorId);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor Id</param>
    /// <returns>返回消息数据</returns>
    public static Task<CallResult<Response>> CallActorAsync<Response>(this Network network, string data, int messageCmd, int actorId)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallActor(data, (p) =>
        {
            CallResult<Response> result;
            if (p.TryRead(out Response response))
            {
                result = new CallResult<Response>(response, true);
            }
            else
            {
                result = new CallResult<Response>(response, false);
            }
            tcs.TrySetResult(result);
        }, messageCmd, actorId);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor Id</param>
    /// <returns>返回消息数据</returns>
    public static Task<CallResult<Response>> CallActorAsync<Response>(this Network network, int data, int messageCmd, int actorId)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallActor(data, (p) =>
        {
            CallResult<Response> result;
            if (p.TryRead(out Response response))
            {
                result = new CallResult<Response>(response, true);
            }
            else
            {
                result = new CallResult<Response>(response, false);
            }
            tcs.TrySetResult(result);
        }, messageCmd, actorId);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor Id</param>
    /// <returns>返回消息数据</returns>
    public static Task<CallResult<Response>> CallActorAsync<Response>(this Network network, uint data, int messageCmd, int actorId)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallActor(data, (p) =>
        {
            CallResult<Response> result;
            if (p.TryRead(out Response response))
            {
                result = new CallResult<Response>(response, true);
            }
            else
            {
                result = new CallResult<Response>(response, false);
            }
            tcs.TrySetResult(result);
        }, messageCmd, actorId);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor Id</param>
    /// <returns>返回消息数据</returns>
    public static Task<CallResult<Response>> CallActorAsync<Response>(this Network network, long data, int messageCmd, int actorId)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallActor(data, (p) =>
        {
            CallResult<Response> result;
            if (p.TryRead(out Response response))
            {
                result = new CallResult<Response>(response, true);
            }
            else
            {
                result = new CallResult<Response>(response, false);
            }
            tcs.TrySetResult(result);
        }, messageCmd, actorId);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor Id</param>
    /// <returns>返回消息数据</returns>
    public static Task<CallResult<Response>> CallActorAsync<Response>(this Network network, ulong data, int messageCmd, int actorId)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallActor(data, (p) =>
        {
            CallResult<Response> result;
            if (p.TryRead(out Response response))
            {
                result = new CallResult<Response>(response, true);
            }
            else
            {
                result = new CallResult<Response>(response, false);
            }
            tcs.TrySetResult(result);
        }, messageCmd, actorId);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor Id</param>
    /// <returns>返回消息数据</returns>
    public static Task<CallResult<Response>> CallActorAsync<Response>(this Network network, float data, int messageCmd, int actorId)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallActor(data, (p) =>
        {
            CallResult<Response> result;
            if (p.TryRead(out Response response))
            {
                result = new CallResult<Response>(response, true);
            }
            else
            {
                result = new CallResult<Response>(response, false);
            }
            tcs.TrySetResult(result);
        }, messageCmd, actorId);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor Id</param>
    /// <returns>返回消息数据</returns>
    public static Task<CallResult<Response>> CallActorAsync<Response>(this Network network, double data, int messageCmd, int actorId)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallActor(data, (p) =>
        {
            CallResult<Response> result;
            if (p.TryRead(out Response response))
            {
                result = new CallResult<Response>(response, true);
            }
            else
            {
                result = new CallResult<Response>(response, false);
            }
            tcs.TrySetResult(result);
        }, messageCmd, actorId);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor Id</param>
    /// <returns>返回消息数据</returns>
    public static Task<CallResult<Response>> CallActorAsync<Response>(this Network network, decimal data, int messageCmd, int actorId)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallActor(data, (p) =>
        {
            CallResult<Response> result;
            if (p.TryRead(out Response response))
            {
                result = new CallResult<Response>(response, true);
            }
            else
            {
                result = new CallResult<Response>(response, false);
            }
            tcs.TrySetResult(result);
        }, messageCmd, actorId);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor Id</param>
    /// <returns>返回消息数据</returns>
    public static Task<CallResult<Response>> CallActorAsync<Response>(this Network network, short data, int messageCmd, int actorId)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallActor(data, (p) =>
        {
            CallResult<Response> result;
            if (p.TryRead(out Response response))
            {
                result = new CallResult<Response>(response, true);
            }
            else
            {
                result = new CallResult<Response>(response, false);
            }
            tcs.TrySetResult(result);
        }, messageCmd, actorId);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor Id</param>
    /// <returns>返回消息数据</returns>
    public static Task<CallResult<Response>> CallActorAsync<Response>(this Network network, ushort data, int messageCmd, int actorId)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallActor(data, (p) =>
        {
            CallResult<Response> result;
            if (p.TryRead(out Response response))
            {
                result = new CallResult<Response>(response, true);
            }
            else
            {
                result = new CallResult<Response>(response, false);
            }
            tcs.TrySetResult(result);
        }, messageCmd, actorId);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor Id</param>
    /// <returns>返回消息数据</returns>
    public static Task<CallResult<Response>> CallActorAsync<Response>(this Network network, byte data, int messageCmd, int actorId)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallActor(data, (p) =>
        {
            CallResult<Response> result;
            if (p.TryRead(out Response response))
            {
                result = new CallResult<Response>(response, true);
            }
            else
            {
                result = new CallResult<Response>(response, false);
            }
            tcs.TrySetResult(result);
        }, messageCmd, actorId);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor Id</param>
    /// <returns>返回消息数据</returns>
    public static Task<CallResult<Response>> CallActorAsync<Response>(this Network network, sbyte data, int messageCmd, int actorId)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallActor(data, (p) =>
        {
            CallResult<Response> result;
            if (p.TryRead(out Response response))
            {
                result = new CallResult<Response>(response, true);
            }
            else
            {
                result = new CallResult<Response>(response, false);
            }
            tcs.TrySetResult(result);
        }, messageCmd, actorId);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor Id</param>
    /// <returns>返回消息数据</returns>
    public static Task<CallResult<Response>> CallActorAsync<Response>(this Network network, char data, int messageCmd, int actorId)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallActor(data, (p) =>
        {
            CallResult<Response> result;
            if (p.TryRead(out Response response))
            {
                result = new CallResult<Response>(response, true);
            }
            else
            {
                result = new CallResult<Response>(response, false);
            }
            tcs.TrySetResult(result);
        }, messageCmd, actorId);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor Id</param>
    /// <returns>返回消息数据</returns>
    public static Task<CallResult<Response>> CallActorAsync<Response>(this Network network, bool data, int messageCmd, int actorId)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallActor(data, (p) =>
        {
            CallResult<Response> result;
            if (p.TryRead(out Response response))
            {
                result = new CallResult<Response>(response, true);
            }
            else
            {
                result = new CallResult<Response>(response, false);
            }
            tcs.TrySetResult(result);
        }, messageCmd, actorId);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条Actor Rpc消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor Id</param>
    /// <returns>返回消息数据</returns>
    public static Task<CallResult<Response>> CallActorAsync<Response>(this Network network, int messageCmd, int actorId)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallActor((p) =>
        {
            CallResult<Response> result;
            if (p.TryRead(out Response response))
            {
                result = new CallResult<Response>(response, true);
            }
            else
            {
                result = new CallResult<Response>(response, false);
            }
            tcs.TrySetResult(result);
        }, messageCmd, actorId);
        return tcs.Task;
    }
    #endregion

    #region Broadcast all connections
    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast<T>(this Network network, T data, int messageCmd)
        where T : class
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(messageCmd, 0, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, string data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, int data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, uint data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, bool data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, float data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, double data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, decimal data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, long data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, ulong data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, byte data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, sbyte data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, short data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, ushort data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, 0);
    }
    #endregion Broadcast

    #region Broadcast list connections
    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast<T>(this IEnumerable<Network> networks, T data, int messageCmd)
        where T : class
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, int messageCmd)
    {
        foreach (var net in networks)
        {
            net.Send(messageCmd);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, string data, int messageCmd)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, int data, int messageCmd)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, uint data, int messageCmd)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, bool data, int messageCmd)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, long data, int messageCmd)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, ulong data, int messageCmd)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, float data, int messageCmd)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, double data, int messageCmd)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, decimal data, int messageCmd)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, byte data, int messageCmd)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, sbyte data, int messageCmd)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, char data, int messageCmd)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, short data, int messageCmd)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, ushort data, int messageCmd)
    {
        foreach (var net in networks)
        {
            net.Send(data, messageCmd);
        }
    }
    #endregion
}