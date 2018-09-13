using H6Game.Message;
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
                    var handlers = HandlerMsgPool.GetActorHandler(packet.MessageId);
                    foreach(var handler in handlers)
                    {
                        handler.Receive(network);
                    }
                    return;
                }
                else
                {
                    var handlers = HandlerMsgPool.GetHandler(packet.MessageId);
                    foreach (var handler in handlers)
                    {
                        handler.Receive(network);
                    }
                    return;
                }
                throw new NetworkException($"MessageCMD:{packet.MessageId}没有在Handler实现类中加入MessageCMDAttribute.");
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, packet.ToJson());
            }
        }
    }
}