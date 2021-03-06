﻿using H6Game.Base;
using H6Game.Base.Message;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class NetworkExtensions
{
    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <typeparam name="TSender"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response<TSender>(this Network network, TSender data)
    {
        network.Session.Send(network.Channel, data, network.RecvPacket.NetCommand, network.RecvPacket.RpcId);
    }

    /// <summary>
    /// 回发消息，回发的消息所有的协议与接收到的消息保持一致
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="data">回发数据</param>
    public static void Response(this Network network, object data, Type type)
    {
        network.Session.Send(network.Channel, data, network.RecvPacket.NetCommand, network.RecvPacket.RpcId, type);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <typeparam name="TSender"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send<TSender>(this Network network, TSender data, ushort netCommand)
    {
        network.Session.Send(network.Channel, data, netCommand, 0);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="netCommand">表示这条消息指令</param>
    public static void Send(this Network network, ushort netCommand)
    {
        network.Session.Send(network.Channel, netCommand, 0);
    }

    /// <summary>
    /// 给一组Socket连接网络广播一条消息
    /// </summary>
    /// <typeparam name="TSender"></typeparam>
    /// <param name="networks">一组网络</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Send<TSender>(this IEnumerable<Network> networks, TSender data, ushort netCommand)
    {
        foreach (var network in networks)
        {
            network.Send(data, netCommand);
        }
    }

    /// <summary>
    /// 远程调用一条RPC消息
    /// </summary>
    /// <typeparam name="TRequest">返回消息类型</typeparam>
    /// <typeparam name="TResponse">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<TResponse> CallMessageAsync<TRequest, TResponse>(this Network network, TRequest data, ushort netCommand)
    {
        var tcs = new TaskCompletionSource<TResponse>();
        network.Session.Subscribe(network.Channel, data, (p) =>
        {
            var response = p.Read<TResponse>();
            tcs.TrySetResult(response);
        }, netCommand);

        return tcs.Task;
    }

    /// <summary>
    /// 远程调用一条RPC消息
    /// </summary>
    /// <typeparam name="TResponse">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<TResponse> CallMessageAsync<TResponse>(this Network network, object data, ushort netCommand)
    {
        var command = Convert.ToInt32(netCommand);
        var tcs = new TaskCompletionSource<TResponse>();
        network.Session.Subscribe(network.Channel, data, (p) =>
        {
            var response = p.Read<TResponse>();
            tcs.TrySetResult(response);
        }, netCommand);

        return tcs.Task;
    }

    /// <summary>
    /// 远程调用一条RPC消息
    /// </summary>
    /// <typeparam name="TResponse">返回消息类型</typeparam>
    /// <param name="network">网络类</param>
    /// <param name="netCommand">消息指令</param>
    /// <returns>返回消息数据。</returns>
    public static Task<TResponse> CallMessageAsync<TResponse>(this Network network, ushort netCommand)
    {
        var tcs = new TaskCompletionSource<TResponse>();
        network.Session.Subscribe(network.Channel, (p) =>
        {
            var response = p.Read<TResponse>();
            tcs.TrySetResult(response);
        }, netCommand);
        return tcs.Task;
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <typeparam name="TSender"></typeparam>
    /// <param name="network">网络类</param>
    /// <param name="data">发送数据</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast<TSender>(this Network network, TSender data, ushort netCommand)
    {
        network.Session.Broadcast(data, netCommand);
    }

    /// <summary>
    /// 给所有Socket连接广播一条消息
    /// </summary>
    /// <param name="network">网络类</param>
    /// <param name="netCommand">消息指令</param>
    public static void Broadcast(this Network network, ushort netCommand)
    {
        network.Session.Broadcast(netCommand);
    }
}