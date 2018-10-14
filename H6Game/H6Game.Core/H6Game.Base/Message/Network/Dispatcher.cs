using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace H6Game.Base
{
    /// <summary>
    /// 消息分发
    /// </summary>
    public static class Dispatcher
    {
        public static void Dispatch(this Network network, Packet packet)
        {
            try
            {
                if (MethodContextStorage.TryGetContext(packet.NetCommand, out List<MethodContext> contexts))
                {
                    foreach (var context in contexts)
                        context.Owner.Invoke(context, network);

                    return;
                }

                if (MessageSubscriberStorage.TryGetSubscribers(packet.NetCommand, out HashSet<ISubscriber> subscribers))
                {
                    foreach (var subscriber in subscribers)
                        subscriber.Receive(network);

                    return;
                }

                //非代理服务忽略GetGateEndPoint指令获取网关IP端口信息请求。
                if (!Game.Scene.GetComponent<NetDistributionsComponent>().IsProxyServer && packet.NetCommand == (int)SysNetCommand.GetGateEndPoint)
                {
                    return;
                }
                
                throw new NetworkException($"消息命令:{packet.NetCommand} 类型:{packet.MsgTypeCode}没有被订阅。");

            }
            catch (Exception e)
            {
                throw new NetworkException($"ExceptionMessage:{e.Message}{Environment.NewLine}Packet:{packet.ToJson()}", e);
            }
        }
    }
}