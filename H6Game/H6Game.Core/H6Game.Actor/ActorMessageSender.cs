using H6Game.Base;
using H6Game.Base.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace H6Game.Actor
{
    public class ActorMessageSender
    {
        /// <summary>
        /// 从当前Actor发送消息给另一个Actor
        /// </summary>
        /// <typeparam name="TActorMessage"></typeparam>
        /// <param name="actor"></param>
        /// <param name="message"></param>
        /// <param name="command"></param>
        public static void SendActor<TActorMessage>(TActorMessage message, NetCommand command)
            where TActorMessage : IActorMessage
        {
           Game.Scene.GetComponent<ActorComponentStorage>().SendActor(message, command);
        }

        /// <summary>
        /// 请求一条Actor消息
        /// </summary>
        /// <typeparam name="TActorResponse"></typeparam>
        /// <param name="actor"></param>
        /// <param name="message"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<TActorResponse> CallActor<TActorResponse>(IActorMessage message, NetCommand command)
            where TActorResponse : IActorMessage
        {
            return await Game.Scene.GetComponent<ActorComponentStorage>().CallActor<TActorResponse>(message, command);
        }
    }
}
