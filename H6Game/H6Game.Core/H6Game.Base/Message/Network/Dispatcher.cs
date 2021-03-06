﻿using H6Game.Base.Component;
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
            var type = MessageCommandStorage.GetMsgType(packet.MsgTypeCode);
            if (MessageSubscriberStorage.TryGetSubscriber(packet.NetCommand, type, out ISubscriber subscriber))
            {
                subscriber.Receive(network);
                return;
            }

            Log.Fatal($"消息命令:{packet.NetCommand} 消息类型:{packet.MsgTypeCode}找不到订阅器。", LoggerBllType.Network, packet.ToJson());
        }
    }
}