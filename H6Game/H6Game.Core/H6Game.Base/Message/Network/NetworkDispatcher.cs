using MongoDB.Bson;
using System;

namespace H6Game.Base
{
    /// <summary>
    /// 消息分发者类
    /// </summary>
    public class NetworkDispatcher
    {
        public static void Dispatch(Network network, Packet packet)
        {
            try
            {
                if( MetodContextManager.TryGetContext(packet.MessageCmd, out MetodContext context))
                {
                    context.Owner.Invoke(context, network);
                    return;
                }

                if (packet.ActorId > 0)
                {
                    var actorSubscribers = MessageSubscriberPool.GetActorSubscriber(packet.MessageCmd);
                    foreach(var subscriber in actorSubscribers)
                    {
                        subscriber.Receive(network);
                    }
                    return;
                }

                var subscribers = MessageSubscriberPool.GetSubscriber(packet.MessageCmd);
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