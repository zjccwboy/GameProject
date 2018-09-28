using MongoDB.Bson;
using System;

namespace H6Game.Base
{
    /// <summary>
    /// 消息分发给所有订阅MessageCmd的Hadler类
    /// </summary>
    public class NetworkDispatcher
    {
        public static void DoReceive(Network network, Packet packet)
        {
            try
            {
                if (packet.ActorId > 0)
                {
                    var subscribers = SubscriberMsgPool.GetActorSubscriber(packet.MessageCmd);
                    foreach(var subscriber in subscribers)
                    {
                        subscriber.Receive(network);
                    }
                    return;
                }
                else
                {
                    var subscribers = SubscriberMsgPool.GetSubscriber(packet.MessageCmd);
                    foreach (var subscriber in subscribers)
                    {
                        subscriber.Receive(network);
                    }
                    return;
                }
                throw new NetworkException($"MessageCMD:{packet.MessageCmd}没有在Handler实现类中加入MessageCMDAttribute.");
            }
            catch (Exception e)
            {
                Log.Error(packet.ToJson(), e, LoggerBllType.System);
            }
        }
    }
}