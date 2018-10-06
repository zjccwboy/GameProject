using H6Game.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class NetworkHelper
{
    #region TResponse
    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response<TRequest>(this Network network, TRequest data)
    {
        network.Session.Send(network.Channel, data, network.RecvPacket.NetCommand, network.RecvPacket.RpcId);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response(this Network network, object data)
    {
        network.Session.Send(network.Channel, data, network.RecvPacket.NetCommand, network.RecvPacket.RpcId);
    }
    #endregion TResponse

    #region Send
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send<TRequest>(this Network network, TRequest data, int netCommand)
    {
        network.Session.Send(network.Channel, data, netCommand, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send(this Network network, int netCommand)
    {
        network.Session.Send(network.Channel, netCommand, 0);
    }
    #endregion Send

    #region  CallMessage
    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<TRequest, TResponse>(this Network network, TRequest data, Action<TResponse> notificationAction, int netCommand)
    {
        network.Session.Subscribe(network.Channel,data, (p) => {
            var response = p.Read<TResponse>();
            notificationAction(response);
        }, netCommand);
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="TResponse">返回数据类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="notificationAction">订阅回调</param>
    /// <param name="netCommand">消息指令</param>
    public static void CallMessage<TResponse>(this Network network, Action<TResponse> notificationAction, int netCommand)
    {
        network.Session.Subscribe(network.Channel, (p) => {
            var response = p.Read<TResponse>();
            notificationAction(response);
        }, netCommand);
    }
    #endregion CallMessage

    #region CallMessageAsync
    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="TRquest">返回消息类型</typeparam>
    /// <typeparam name="TResponse">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<TResponse>> CallMessageAsync<TRquest, TResponse>(this Network network, TRquest data, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<TResponse>>();
        network.Session.Subscribe(network.Channel, data, (p) =>
        {
            var state = p.TryRead(out TResponse response);
            var result = new CallResult<TResponse>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);

        return tcs.Task;
    }

    /// <summary>
    /// 订阅一条RPC消息
    /// </summary>
    /// <typeparam name="TResponse">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<CallResult<TResponse>> CallMessageAsync<TResponse>(this Network network, int netCommand)
    {
        var tcs = new TaskCompletionSource<CallResult<TResponse>>();
        network.Session.Subscribe(network.Channel, (p) =>
        {
            var state = p.TryRead(out TResponse response);
            var result = new CallResult<TResponse>(response, state);
            tcs.TrySetResult(result);
        }, netCommand);
        return tcs.Task;
    }
    #endregion

    #region Broadcast all connections
    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast<TRequest>(this Network network, TRequest data, int netCommand)
    {
        network.Session.Broadcast(data, netCommand, 0);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this Network network, int netCommand)
    {
        network.Session.Broadcast(netCommand, 0);
    }
    #endregion Broadcast

    #region Broadcast list connections
    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast<TRequest>(this IEnumerable<Network> networks, TRequest data, int netCommand)
    {
        foreach (var network in networks)
        {
            network.Send(data, netCommand);
        }
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <param name="networks">一组网络</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this IEnumerable<Network> networks, int netCommand)
    {
        foreach (var network in networks)
        {
            network.Send(netCommand);
        }
    }
    #endregion
}