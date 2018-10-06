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
                if( MetodContextPool.TryGetContext(packet.NetCommand, out List<MetodContext> contexts))
                {
                    foreach(var context in contexts)
                        context.Owner.Invoke(context, network);

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
                throw new NetworkException($"ExceptionMessage:{e.Message}{Environment.NewLine}Packet:{packet.ToJson()}", e);
            }
        }
    }
}