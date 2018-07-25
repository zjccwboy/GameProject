using H6Game.Message;
using System;
using System.Collections.Generic;

namespace H6Game.Base
{
    /// <summary>
    /// 消息处理类工厂
    /// </summary>
    public class MessageHandlerFactory
    {
        private static HashSet<Type> types;
        static MessageHandlerFactory()
        {
            types = ObjectFactory.GetTypes<IHandler>();
        }

        /// <summary>
        /// 创建消息处理类
        /// </summary>
        /// <param name="channel">通讯管道对象</param>
        /// <param name="netService">网络服务对象</param>
        /// <returns></returns>
        public static IEnumerable<IHandler> CreateHandlers(Session session, ANetChannel channel, ANetService netService)
        {
            var handlers = new List<IHandler>();
            foreach(var type in types)
            {
                var handler = (IHandler)Activator.CreateInstance(type);
                handler.Channel = channel;
                handler.NetService = netService;
                handler.Session = session;
                channel.OnReceive += handler.DoReceive;
                handlers.Add(handler);
            }
            return handlers;
        }
    }
}
