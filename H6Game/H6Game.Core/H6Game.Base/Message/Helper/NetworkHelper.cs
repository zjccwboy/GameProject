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
    public static void Response<T>(this Network network, T data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var netCommand = network.RecvPacket.NetCommand;
        var rpcId = network.RecvPacket.RpcId;
        session.Send(channel, data, netCommand, rpcId);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response(this Network network, object data)
    {
        var session = network.Session;
        var channel = network.Channel;
        var netCommand = network.RecvPacket.NetCommand;
        var rpcId = network.RecvPacket.RpcId;
        session.Send(channel, data, netCommand, rpcId);
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
        var netCommand = network.RecvPacket.NetCommand;
        var rpcId = network.RecvPacket.RpcId;
        session.Send(channel, data, netCommand, rpcId);
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
        var netCommand = network.RecvPacket.NetCommand;
        var rpcId = network.RecvPacket.RpcId;
        session.Send(channel, data, netCommand, rpcId);
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
        var netCommand = network.RecvPacket.NetCommand;
        var rpcId = network.RecvPacket.RpcId;
        session.Send(channel, data, netCommand, rpcId);
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
        var netCommand = network.RecvPacket.NetCommand;
        var rpcId = network.RecvPacket.RpcId;
        session.Send(channel, data, netCommand, rpcId);
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
        var netCommand = network.RecvPacket.NetCommand;
        var rpcId = network.RecvPacket.RpcId;
        session.Send(channel, data, netCommand, rpcId);
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
        var netCommand = network.RecvPacket.NetCommand;
        var rpcId = network.RecvPacket.RpcId;
        session.Send(channel, data, netCommand, rpcId);
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
        var netCommand = network.RecvPacket.NetCommand;
        var rpcId = network.RecvPacket.RpcId;
        session.Send(channel, data, netCommand, rpcId);
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
        var netCommand = network.RecvPacket.NetCommand;
        var rpcId = network.RecvPacket.RpcId;
        session.Send(channel, data, netCommand, rpcId);
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
        var netCommand = network.RecvPacket.NetCommand;
        var rpcId = network.RecvPacket.RpcId;
        session.Send(channel, data, netCommand, rpcId);
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
        var netCommand = network.RecvPacket.NetCommand;
        var rpcId = network.RecvPacket.RpcId;
        session.Send(channel, data, netCommand, rpcId);
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
        var netCommand = network.RecvPacket.NetCommand;
        var rpcId = network.RecvPacket.RpcId;
        session.Send(channel, data, netCommand, rpcId);
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
        var netCommand = network.RecvPacket.NetCommand;
        var rpcId = network.RecvPacket.RpcId;
        session.Send(channel, data, netCommand, rpcId);
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
        var netCommand = network.RecvPacket.NetCommand;
        var rpcId = network.RecvPacket.RpcId;
        session.Send(channel, data, netCommand, rpcId);
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
        var netCommand = network.RecvPacket.NetCommand;
        var rpcId = network.RecvPacket.RpcId;
        session.Send(channel, data, netCommand, rpcId);
    }
    #endregion Response

    #region Send
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send<T>(this Network network, T data, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, netCommand, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send(this Network network, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, netCommand, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">表示这条消息指令</param>
    /// <param name="actorId">Actor消息指令</param>
    public static void Send(this Network network, string data, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, netCommand, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send(this Network network, int data, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, netCommand, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send(this Network network, uint data, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, netCommand, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send(this Network network, long data, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, netCommand, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send(this Network network, ulong data, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, netCommand, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send(this Network network, float data, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, netCommand, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send(this Network network, double data, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, netCommand, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send(this Network network, decimal data, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, netCommand, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send(this Network network, byte data, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, netCommand, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send(this Network network, sbyte data, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, netCommand, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send(this Network network, bool data, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, netCommand, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send(this Network network, char data, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, netCommand, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send(this Network network, short data, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, netCommand, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send(this Network network, ushort data, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Send(channel, data, netCommand, 0);
    }
    #endregion Send

    #region  CallMessage
    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Request"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<Request>(this Network network, Request data, Action<Packet> notificationAction, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage(this Network network, Action<Packet> notificationAction, int netCommand)

    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, notificationAction, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage(this Network network, string data, Action<Packet> notificationAction, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage(this Network network, int data, Action<Packet> notificationAction, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage(this Network network, uint data, Action<Packet> notificationAction, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage(this Network network, bool data, Action<Packet> notificationAction, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage(this Network network, long data, Action<Packet> notificationAction, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage(this Network network, ulong data, Action<Packet> notificationAction, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage(this Network network, float data, Action<Packet> notificationAction, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage(this Network network, double data, Action<Packet> notificationAction, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage(this Network network, decimal data, Action<Packet> notificationAction, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage(this Network network, byte data, Action<Packet> notificationAction, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage(this Network network, sbyte data, Action<Packet> notificationAction, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage(this Network network, char data, Action<Packet> notificationAction, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage(this Network network, short data, Action<Packet> notificationAction, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage(this Network network, ushort data, Action<Packet> notificationAction, int netCommand)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Request"></typeparam>
    /// <typeparam name="Response"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<Request, Response>(this Network network, Request data, Action<Response> notificationAction, int netCommand) where Request : class
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<Response>(this Network network, Action<Response> notificationAction, int netCommand)
    {
        network.CallMessage((p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<Response>(this Network network, string data, Action<Response> notificationAction, int netCommand)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<Response>(this Network network, int data, Action<Response> notificationAction, int netCommand)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<Response>(this Network network, uint data, Action<Response> notificationAction, int netCommand)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<Response>(this Network network, bool data, Action<Response> notificationAction, int netCommand)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<Response>(this Network network, long data, Action<Response> notificationAction, int netCommand)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<Response>(this Network network, ulong data, Action<Response> notificationAction, int netCommand)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<Response>(this Network network, float data, Action<Response> notificationAction, int netCommand)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<Response>(this Network network, double data, Action<Response> notificationAction, int netCommand)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<Response>(this Network network, decimal data, Action<Response> notificationAction, int netCommand)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<Response>(this Network network, byte data, Action<Response> notificationAction, int netCommand)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<Response>(this Network network, sbyte data, Action<Response> notificationAction, int netCommand)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<Response>(this Network network, char data, Action<Response> notificationAction, int netCommand)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<Response>(this Network network, short data, Action<Response> notificationAction, int netCommand)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<Response>(this Network network, ushort data, Action<Response> notificationAction, int netCommand)
    {
        network.CallMessage(data, (p) => {
            var response = p.Read<Response>();
            notificationAction(response);
        }, netCommand);
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
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Rquest, Response>(this Network network, Rquest data, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);

        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage((p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, string data, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, int data, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, uint data, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, long data, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, ulong data, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, float data, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, double data, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, decimal data, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, short data, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, ushort data, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, byte data, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, sbyte data, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, char data, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);
        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="Response">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<Response>> CallMessageAsync<Response>(this Network network, bool data, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallMessage(data, (p) =>
        {
            var state = p.TryRead(out Response response);
            var result = new CallResult<Response>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);
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
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast<T>(this Network network, T data, int netCommand)
    {
        var session = network.Session;
        session.Broadcast(data, netCommand, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this Network network, int netCommand)
    {
        var session = network.Session;
        session.Broadcast(netCommand, 0, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this Network network, string data, int netCommand)
    {
        var session = network.Session;
        session.Broadcast(data, netCommand, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this Network network, int data, int netCommand)
    {
        var session = network.Session;
        session.Broadcast(data, netCommand, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this Network network, uint data, int netCommand)
    {
        var session = network.Session;
        session.Broadcast(data, netCommand, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this Network network, bool data, int netCommand)
    {
        var session = network.Session;
        session.Broadcast(data, netCommand, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this Network network, float data, int netCommand)
    {
        var session = network.Session;
        session.Broadcast(data, netCommand, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this Network network, double data, int netCommand)
    {
        var session = network.Session;
        session.Broadcast(data, netCommand, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this Network network, decimal data, int netCommand)
    {
        var session = network.Session;
        session.Broadcast(data, netCommand, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this Network network, long data, int netCommand)
    {
        var session = network.Session;
        session.Broadcast(data, netCommand, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this Network network, ulong data, int netCommand)
    {
        var session = network.Session;
        session.Broadcast(data, netCommand, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this Network network, byte data, int netCommand)
    {
        var session = network.Session;
        session.Broadcast(data, netCommand, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this Network network, sbyte data, int netCommand)
    {
        var session = network.Session;
        session.Broadcast(data, netCommand, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this Network network, short data, int netCommand)
    {
        var session = network.Session;
        session.Broadcast(data, netCommand, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this Network network, ushort data, int netCommand)
    {
        var session = network.Session;
        session.Broadcast(data, netCommand, 0);
    }
    #endregion Broadcast

    #region Broadcast list connections
    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast<T>(this IEnumerable<Network> networks, T data, int netCommand)
    {
        foreach (var net in networks)
        {
            net.Send(data, netCommand);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, int netCommand)
    {
        foreach (var net in networks)
        {
            net.Send(netCommand);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, string data, int netCommand)
    {
        foreach (var net in networks)
        {
            net.Send(data, netCommand);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, int data, int netCommand)
    {
        foreach (var net in networks)
        {
            net.Send(data, netCommand);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, uint data, int netCommand)
    {
        foreach (var net in networks)
        {
            net.Send(data, netCommand);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, bool data, int netCommand)
    {
        foreach (var net in networks)
        {
            net.Send(data, netCommand);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, long data, int netCommand)
    {
        foreach (var net in networks)
        {
            net.Send(data, netCommand);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, ulong data, int netCommand)
    {
        foreach (var net in networks)
        {
            net.Send(data, netCommand);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, float data, int netCommand)
    {
        foreach (var net in networks)
        {
            net.Send(data, netCommand);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, double data, int netCommand)
    {
        foreach (var net in networks)
        {
            net.Send(data, netCommand);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, decimal data, int netCommand)
    {
        foreach (var net in networks)
        {
            net.Send(data, netCommand);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, byte data, int netCommand)
    {
        foreach (var net in networks)
        {
            net.Send(data, netCommand);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, sbyte data, int netCommand)
    {
        foreach (var net in networks)
        {
            net.Send(data, netCommand);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, char data, int netCommand)
    {
        foreach (var net in networks)
        {
            net.Send(data, netCommand);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, short data, int netCommand)
    {
        foreach (var net in networks)
        {
            net.Send(data, netCommand);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, ushort data, int netCommand)
    {
        foreach (var net in networks)
        {
            net.Send(data, netCommand);
        }
    }
    #endregion
}