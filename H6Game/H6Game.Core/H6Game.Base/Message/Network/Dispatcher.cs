using H6Game.Base.Component;
using H6Game.Base.Exceptions;
using H6Game.Base.Logger;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace H6Game.Base.Message
{
    /// <summary>
    /// 消息分发
    /// </summary>
    public static class Dispatcher
    {
        public static void Dispatching(this Network network, Packet packet)
        {
            try
            {
                var type = MessageCommandStorage.GetMsgType(packet.MsgTypeCode);
                if (MessageSubscriberStorage.TryGetSubscriber(packet.NetCommand, type, out ISubscriber subscriber))
                {
                    subscriber.Receive(network);
                    return;
                }
               
                throw new NetworkException($"消息命令:{packet.NetCommand} 类型:{packet.MsgTypeCode}没有被订阅。");

            }
            catch (Exception e)
            {
                Log.Warn(e, LoggerBllType.System, packet.ToJson());
            }
        }
    }
}