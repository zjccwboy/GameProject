using System;
using System.Threading.Tasks;

namespace H6Game.Base
{
    public abstract class H6Controller : IController
    {
        private static Type ValueType { get; } = typeof(IValue);

        public void Invoke(MetodContext context, Network network)
        {
            switch(context.MetodType)
            {
                case MetodType.Invoke:
                    if(context.ParameterTypes == null || context.ParameterTypes.Length == 0)
                    {
                        InvokeSync(context, network);
                    }
                    else
                    {
                        var message = GetMessage(context, network);
                        InvokeSync(context, network, message);
                    }
                    break;
                case MetodType.ExistReturnInvoke:
                    if (context.ParameterTypes == null || context.ParameterTypes.Length == 0)
                    {
                        ExistReturnInvokeSync(context, network);
                    }
                    else
                    {
                        var message = GetMessage(context, network);
                        ExistReturnInvokeSync(context, network, message);
                    }
                    break;
                case MetodType.InvokeAsync:
                    if (context.ParameterTypes == null || context.ParameterTypes.Length == 0)
                    {
                        InvokeAsync(context, network);
                    }
                    else
                    {
                        var message = GetMessage(context, network);
                        InvokeAsync(context, network, message);
                    }
                    break;
                case MetodType.ExistReturnInvokeAsync:
                    if (context.ParameterTypes == null || context.ParameterTypes.Length == 0)
                    {
                        ExistReturnInvokeAsync(context, network);
                    }
                    else
                    {
                        var message = GetMessage(context, network);
                        ExistReturnInvokeAsync(context, network, message);
                    }
                    break;
            }
        }

        private static object GetMessage(MetodContext context, Network network)
        {
            var packet = network.RecvPacket;
            var messageType = MessageSubscriberPool.GetMsgType(packet.MsgTypeCode);
            if (packet.TryRead(messageType, out object message))
            {
                var msgType = message.GetType();
                var isValue = ValueType.IsAssignableFrom(msgType) & context.ParameterTypes[0] != msgType;
                if (isValue)
                {
                    return message.GetValue();
                }
                else
                {
                    return message;
                }
            }
            throw new NetworkException($"反序列化类型:{messageType}失败");
        }

        private static void InvokeSync(MetodContext context, Network network, object message)
        {

            context.Parameters[0] = message;
            context.MethodInfo.Invoke(context.Owner, context.Parameters);
        }

        private static void InvokeSync(MetodContext context, Network network)
        {
            context.MethodInfo.Invoke(context.Owner, null);
        }

        private static void ExistReturnInvokeSync(MetodContext context, Network network, object message)
        {
            context.Parameters[0] = message;
            var response = context.MethodInfo.Invoke(context.Owner, context.Parameters);
            network.Response(response);
        }

        private static void ExistReturnInvokeSync(MetodContext context, Network network)
        {
            var response = context.MethodInfo.Invoke(context.Owner, null);
            network.Response(response);
        }

        private static async void InvokeAsync(MetodContext context, Network network, object message)
        {
            context.Parameters[0] = message;
            await (Task)context.MethodInfo.Invoke(context.Owner, context.Parameters);
        }

        private static async void InvokeAsync(MetodContext context, Network network)
        {
            await (Task)context.MethodInfo.Invoke(context.Owner, null);
        }

        private static async void ExistReturnInvokeAsync(MetodContext context, Network network, object message)
        {
            context.Parameters[0] = message;
            var response = await (Task<object>)context.MethodInfo.Invoke(context.Owner, context.Parameters);
            network.Response(response);
        }

        private static async void ExistReturnInvokeAsync(MetodContext context, Network network)
        {
            var response = await (Task<object>)context.MethodInfo.Invoke(context.Owner, null);
            network.Response(response);
        }
    }
}
