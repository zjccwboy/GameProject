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
        private static NetDistributionsComponent DistributionsComponent { get; set; }
        static Dispatcher()
        {
            DistributionsComponent = GetNetDistributionsComponent();
        }

        public static void Dispatching(this Network network, Packet packet)
        {
            try
            {
                if (MessageSubscriberStorage.TryGetSubscribers(packet.NetCommand, out Dictionary<Type, ISubscriber> subscribers))
                {
                    foreach (var subscriber in subscribers.Values)
                        subscriber.Receive(network);

                    return;
                }

                if(DistributionsComponent != null)
                {
                    //非代理服务忽略GetGateEndPoint指令获取网关IP端口信息请求。
                    if (!DistributionsComponent.IsProxyServer && packet.NetCommand == (int)SysNetCommand.GetGateEndPoint)
                    {
                        return;
                    }
                }
                
                throw new NetworkException($"消息命令:{packet.NetCommand} 类型:{packet.MsgTypeCode}没有被订阅。");

            }
            catch (Exception e)
            {
                Log.Error(e, LoggerBllType.System, packet.ToJson());
            }
        }

        private static NetDistributionsComponent GetNetDistributionsComponent()
        {
            try
            {
                return Game.Scene.GetComponent<NetDistributionsComponent>();
            }
            catch
            {
                return null;
            }
        }
    }
}