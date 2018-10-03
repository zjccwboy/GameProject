using MongoDB.Bson;
using System;

namespace H6Game.Base
{
    /// <summary>
    /// 消息分发给所有订阅MessageCmd的Hadler类
    /// </summary>
    public class NetworkDispatcher
    {
        public static void Dispatch(Network network, Packet packet)
        {
            try
            {
                if (packet.ActorId > 0)
                {
                    var actorSubscribers = SubscriberMsgPool.GetActorSubscriber(packet.MessageCmd);
                    foreach(var subscriber in actorSubscribers)
                    {
                        subscriber.Receive(network);
                    }
                    return;
                }

                var subscribers = SubscriberMsgPool.GetSubscriber(packet.MessageCmd);
                foreach (var subscriber in subscribers)
                {
                    subscriber.Receive(network);
                }
            }
            catch (Exception e)
            {
                Log.Error(packet.ToJson(), e, LoggerBllType.System);
            }
        }
    }
}