using H6Game.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class NetworkHelper
{
    #region RpcCallBack
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
        session.Send(channel, data, messageCmd, rpcId, actorId);
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
        session.Send(channel, data, messageCmd, rpcId, actorId);
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
        session.Send(channel, data, messageCmd, rpcId, actorId);
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
        session.Send(channel, data, messageCmd, rpcId, actorId);
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
        session.Send(channel, data, messageCmd, rpcId, actorId);
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
        session.Send(channel, data, messageCmd, rpcId, actorId);
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
        session.Send(channel, data, messageCmd, rpcId, actorId);
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
        session.Send(channel, data, messageCmd, rpcId, actorId);
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
        session.Send(channel, data, messageCmd, rpcId, actorId);
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
        session.Send(channel, data, messageCmd, rpcId, actorId);
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
        session.Send(channel, data, messageCmd, rpcId, actorId);
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
        session.Send(channel, data, messageCmd, rpcId, actorId);
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
        session.Send(channel, data, messageCmd, rpcId, actorId);
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
        session.Send(channel, data, messageCmd, rpcId, actorId);
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
        session.Send(channel, data, messageCmd, rpcId, actorId);
    }
    #endregion RpcCallBack

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
        session.Send(channel, data, messageCmd, 0);
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
        session.Send(channel, null, messageCmd, 0);
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
        session.Send(channel, data, messageCmd, 0);
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
        session.Send(channel, data, messageCmd, 0);
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
        session.Send(channel, data, messageCmd, 0);
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
        session.Send(channel, data, messageCmd, 0);
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
        session.Send(channel, data, messageCmd, 0);
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
        session.Send(channel, data, messageCmd, 0);
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
        session.Send(channel, data, messageCmd, 0);
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
        session.Send(channel, data, messageCmd, 0);
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
        session.Send(channel, data, messageCmd, 0);
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
        session.Send(channel, data, messageCmd, 0);
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
        session.Send(channel, data, messageCmd, 0);
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
        session.Send(channel, data, messageCmd, 0);
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
        session.Send(channel, data, messageCmd, 0);
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
        session.Send(channel, data, messageCmd, 0);
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
        session.Send(channel, string.Empty, messageCmd, 0, actorId);
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

    #region  CallRpc
    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallRpc<T>(this Network network, T data, Action<Packet> notificationAction, int messageCmd) where T : class

    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallRpc(this Network network, Action<Packet> notificationAction
        , int messageCmd)

    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, null, notificationAction, messageCmd);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallRpc(this Network network, string data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallRpc(this Network network, int data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallRpc(this Network network, uint data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallRpc(this Network network, bool data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallRpc(this Network network, long data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallRpc(this Network network, ulong data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallRpc(this Network network, float data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd);
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
    public static void CallRpc(this Network network, double data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallRpc(this Network network, decimal data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallRpc(this Network network, byte data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallRpc(this Network network, sbyte data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallRpc(this Network network, char data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallRpc(this Network network, short data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd);
    }

    /// <summary>
    /// 发送Rpc请求
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    public static void CallRpc(this Network network, ushort data, Action<Packet> notificationAction, int messageCmd)
    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd);
    }
    #endregion CallRpc

    #region CallMessage
    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Rquest"></typeparam>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallMessage<Rquest,Response>(this Network network, Rquest data, int messageCmd)
        where Rquest : class
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallRpc(data, (p) =>
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
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallMessage<Response>(this Network network, string data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallRpc(data, (p) =>
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
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallMessage<Response>(this Network network, int data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallRpc(data, (p) =>
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
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallMessage<Response>(this Network network, uint data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallRpc(data, (p) =>
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
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallMessage<Response>(this Network network, long data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallRpc(data, (p) =>
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
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallMessage<Response>(this Network network, ulong data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallRpc(data, (p) =>
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
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallMessage<Response>(this Network network, float data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallRpc(data, (p) =>
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
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallMessage<Response>(this Network network, double data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallRpc(data, (p) =>
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
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallMessage<Response>(this Network network, decimal data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallRpc(data, (p) =>
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
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallMessage<Response>(this Network network, short data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallRpc(data, (p) =>
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
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallMessage<Response>(this Network network, ushort data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallRpc(data, (p) =>
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
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallMessage<Response>(this Network network, byte data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallRpc(data, (p) =>
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
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallMessage<Response>(this Network network, sbyte data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallRpc(data, (p) =>
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
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallMessage<Response>(this Network network, char data, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallRpc(data, (p) =>
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
        }, messageCmd);
        return tcs.Task;
    }

    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="messageCmd"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallMessage<Response>(this Network network, int messageCmd)
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallRpc((p) =>
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
        }, messageCmd);
        return tcs.Task;
    }
    #endregion

    #region CallActor
    /// <summary>
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Rquest"></typeparam>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallActorMessage<Rquest, Response>(this Network network, Rquest data, int messageCmd, int actorId)
        where Rquest : class
    {
        var tcs = new TaskCompletionSource<CallResult<Response>>();
        network.CallActor(data,(p) =>
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
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallActorMessage<Response>(this Network network, string data, int messageCmd, int actorId)
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
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallActorMessage<Response>(this Network network, int data, int messageCmd, int actorId)
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
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallActorMessage<Response>(this Network network, uint data, int messageCmd, int actorId)
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
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallActorMessage<Response>(this Network network, long data, int messageCmd, int actorId)
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
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallActorMessage<Response>(this Network network, ulong data, int messageCmd, int actorId)
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
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallActorMessage<Response>(this Network network, float data, int messageCmd, int actorId)
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
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallActorMessage<Response>(this Network network, double data, int messageCmd, int actorId)
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
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallActorMessage<Response>(this Network network, decimal data, int messageCmd, int actorId)
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
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallActorMessage<Response>(this Network network, short data, int messageCmd, int actorId)
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
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallActorMessage<Response>(this Network network, ushort data, int messageCmd, int actorId)
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
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallActorMessage<Response>(this Network network, byte data, int messageCmd, int actorId)
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
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallActorMessage<Response>(this Network network, sbyte data, int messageCmd, int actorId)
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
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="data"></param>
    /// <param name="messageCmd"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallActorMessage<Response>(this Network network, char data, int messageCmd, int actorId)
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
    /// RPC请求，有GC不建议使用
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    /// <param name="messageCmd"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public static Task<CallResult<Response>> CallActorMessage<Response>(this Network network, int messageCmd, int actorId)
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

    /// <summary>
    /// 发送Actor Rpc请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">消息指令</param>
    public static void CallActor<T>(this Network network, T data, Action<Packet> notificationAction
        , int messageCmd, int actorId) where T : class

    {
        var session = network.Session;
        var channel = network.Channel;
        session.Subscribe(channel, data, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 发送Actor Rpc请求
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
        session.Subscribe(channel, string.Empty, notificationAction, messageCmd, actorId);
    }

    /// <summary>
    /// 发送Actor Rpc请求
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
    /// 发送Actor Rpc请求
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
    /// 发送Actor Rpc请求
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
    /// 发送Actor Rpc请求
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
    /// 发送Actor Rpc请求
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
    /// 发送Actor Rpc请求
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
    /// 发送Actor Rpc请求
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
    /// 发送Actor Rpc请求
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
    /// 发送Actor Rpc请求
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
    /// 发送Actor Rpc请求
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
    /// 发送Actor Rpc请求
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
    /// 发送Actor Rpc请求
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
    /// 发送Actor Rpc请求
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
    /// 发送Actor Rpc请求
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
    #endregion CallActor

    #region Broadcast all clients
    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast<T>(this Network network, T data, int messageCmd)
        where T : class
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(string.Empty, messageCmd, 0);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, string data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, int data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, uint data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, bool data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, float data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, double data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, decimal data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, long data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, ulong data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, byte data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, sbyte data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, short data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this Network network, ushort data, int messageCmd)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0);
    }
    #endregion Broadcast

    #region BroadcastActor all clients
    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor<T>(this Network network, T data, int messageCmd, int actorId)
        where T : class
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this Network network, int messageCmd, int actorId)
    {
        var session = network.Session;
        session.Broadcast(string.Empty, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this Network network, string data, int messageCmd, int actorId)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this Network network, int data, int messageCmd, int actorId)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this Network network, uint data, int messageCmd, int actorId)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this Network network, bool data, int messageCmd, int actorId)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this Network network, float data, int messageCmd, int actorId)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this Network network, double data, int messageCmd, int actorId)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this Network network, decimal data, int messageCmd, int actorId)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this Network network, long data, int messageCmd, int actorId)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this Network network, ulong data, int messageCmd, int actorId)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this Network network, byte data, int messageCmd, int actorId)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this Network network, sbyte data, int messageCmd, int actorId)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this Network network, short data, int messageCmd, int actorId)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId);
    }

    /// <summary>
    /// 给所有客户端广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this Network network, ushort data, int messageCmd, int actorId)
    {
        var session = network.Session;
        session.Broadcast(data, messageCmd, 0, actorId);
    }
    #endregion Broadcast

    #region Broadcast list clients
    /// <summary>
    /// 给一组连接网络广播一条消息
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
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="messageCmd">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, int messageCmd)
    {
        foreach (var net in networks)
        {
            net.Send(string.Empty, messageCmd);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
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
    /// 给一组连接网络广播一条消息
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
    /// 给一组连接网络广播一条消息
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
    /// 给一组连接网络广播一条消息
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
    /// 给一组连接网络广播一条消息
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
    /// 给一组连接网络广播一条消息
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
    /// 给一组连接网络广播一条消息
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
    /// 给一组连接网络广播一条消息
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
    /// 给一组连接网络广播一条消息
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
    /// 给一组连接网络广播一条消息
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
    /// 给一组连接网络广播一条消息
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
    /// 给一组连接网络广播一条消息
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
    /// 给一组连接网络广播一条消息
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
    /// 给一组连接网络广播一条消息
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

    #region BroadcastActor list clients
    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor<T>(this IEnumerable<Network> networks, T data, int messageCmd, int actorId)
        where T : class
    {
        foreach (var net in networks)
        {
            net.SendActor(data, messageCmd, actorId);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this IEnumerable<Network> networks, int messageCmd, int actorId)
    {
        foreach (var net in networks)
        {
            net.SendActor(string.Empty, messageCmd, actorId);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this IEnumerable<Network> networks, string data, int messageCmd, int actorId)
    {
        foreach (var net in networks)
        {
            net.SendActor(data, messageCmd, actorId);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this IEnumerable<Network> networks, int data, int messageCmd, int actorId)
    {
        foreach (var net in networks)
        {
            net.SendActor(data, messageCmd, actorId);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this IEnumerable<Network> networks, uint data, int messageCmd, int actorId)
    {
        foreach (var net in networks)
        {
            net.SendActor(data, messageCmd, actorId);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this IEnumerable<Network> networks, bool data, int messageCmd, int actorId)
    {
        foreach (var net in networks)
        {
            net.SendActor(data, messageCmd, actorId);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this IEnumerable<Network> networks, long data, int messageCmd, int actorId)
    {
        foreach (var net in networks)
        {
            net.SendActor(data, messageCmd, actorId);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this IEnumerable<Network> networks, ulong data, int messageCmd, int actorId)
    {
        foreach (var net in networks)
        {
            net.SendActor(data, messageCmd, actorId);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this IEnumerable<Network> networks, float data, int messageCmd, int actorId)
    {
        foreach (var net in networks)
        {
            net.SendActor(data, messageCmd, actorId);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this IEnumerable<Network> networks, double data, int messageCmd, int actorId)
    {
        foreach (var net in networks)
        {
            net.SendActor(data, messageCmd, actorId);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this IEnumerable<Network> networks, decimal data, int messageCmd, int actorId)
    {
        foreach (var net in networks)
        {
            net.SendActor(data, messageCmd, actorId);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this IEnumerable<Network> networks, byte data, int messageCmd, int actorId)
    {
        foreach (var net in networks)
        {
            net.SendActor(data, messageCmd, actorId);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this IEnumerable<Network> networks, sbyte data, int messageCmd, int actorId)
    {
        foreach (var net in networks)
        {
            net.SendActor(data, messageCmd, actorId);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this IEnumerable<Network> networks, char data, int messageCmd, int actorId)
    {
        foreach (var net in networks)
        {
            net.SendActor(data, messageCmd, actorId);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this IEnumerable<Network> networks, short data, int messageCmd, int actorId)
    {
        foreach (var net in networks)
        {
            net.SendActor(data, messageCmd, actorId);
        }
    }

    /// <summary>
    /// 给一组连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="messageCmd">消息指令</param>
    /// <param name="actorId">Actor消息Id</param>
    public static void BroadcastActor(this IEnumerable<Network> networks, ushort data, int messageCmd, int actorId)
    {
        foreach (var net in networks)
        {
            net.SendActor(data, messageCmd, actorId);
        }
    }
    #endregion
}