using MongoDB.Bson;
using System;
using System.Collections.Generic;

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
                if( MetodContextPool.TryGetContext(packet.NetCommand, out List<MetodContext> contexts))
                {
                    foreach(var context in contexts)
                        context.Owner.Invoke(context, network);

                    return;
                }

                if (packet.ActorId > 0)
                {
                    var actorSubscribers = MessageSubscriberPool.GetActorSubscribers(packet.NetCommand);
                    foreach(var subscriber in actorSubscribers)
                    {
                        subscriber.Receive(network);
                    }
                    return;
                }

                var subscribers = MessageSubscriberPool.GetSubscribers(packet.NetCommand);
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