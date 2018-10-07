using System;
using System.Threading.Tasks;

namespace H6Game.Base
{
    /// <summary>
    /// 网络控制器组件
    /// </summary>
    public abstract class NetController : BaseComponent, IController
    {
        private static Type ValueType { get; } = typeof(IValue);

        /// <summary>
        /// 当前接收数据的网络管道对象，该管道对象的生命周期只在控制器订阅消息方法之内，当订阅消息方法结束时那该对象就会被设置成null。
        /// </summary>
        protected ANetChannel CurrentNetChannel { get;private set; }

        public void Invoke(MetodContext context, Network network)
        {
            try
            {
                this.CurrentNetChannel = network.Channel;
                switch (context.MetodType)
                {
                    case MetodType.Invoke:
                        if (context.ParameterTypes.Length == 0)
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
                        if (context.ParameterTypes.Length == 0)
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
                        if (context.ParameterTypes.Length == 0)
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
                        if (context.ParameterTypes.Length == 0)
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
            finally
            {
                this.CurrentNetChannel = null;
            }
        }

        private static object GetMessage(MetodContext context, Network network)
        {
            var packet = network.RecvPacket;
            var messageType = MessageCommandPool.GetMsgType(packet.MsgTypeCode);
            if (!packet.TryRead(messageType, out object message))
                throw new NetworkException($"反序列化类型:{messageType}失败");

            var type = context.ParameterTypes[0];
            var msgType = message.GetType();
            var isValue = ValueType.IsAssignableFrom(msgType) & type != msgType;
            if (isValue)
            {
                if (type == typeof(int))
                {
                    return (int)(MyInt32)message;
                }
                else if (type == typeof(uint))
                {
                    return (uint)(MyUInt32)message;
                }
                else if (type == typeof(long))
                {
                    return (long)(MyLong)message;
                }
                else if (type == typeof(ulong))
                {
                    return (ulong)(MyULong)message;
                }
                else if (type == typeof(float))
                {
                    return (float)(MyFloat)message;
                }
                else if (type == typeof(decimal))
                {
                    return (decimal)(MyDecimal)message;
                }
                else if (type == typeof(double))
                {
                    return (double)(MyDouble)message;
                }
                else if (type == typeof(byte))
                {
                    return (byte)(MyByte)message;
                }
                else if (type == typeof(sbyte))
                {
                    return (sbyte)(MySByte)message;
                }
                else if (type == typeof(bool))
                {
                    return (bool)(MyBoolean)message;
                }
                else if (type == typeof(short))
                {
                    return (short)(MyShort)message;
                }
                else if (type == typeof(ushort))
                {
                    return (ushort)(MyUShort)message;
                }
                else if (type == typeof(char))
                {
                    return (char)(MyChar)message;
                }
                else if(type == typeof(DateTime))
                {
                    return (DateTime)(MyDateTime)message;
                }
                else if(type == typeof(Guid))
                {
                    return (Guid)(MyGuid)message;
                }
                throw new NetworkException($"为定义的值类型:{messageType}");
            }
            else
            {
                return message;
            }
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
            await Response(context, network, context.Parameters);
        }

        private static async void ExistReturnInvokeAsync(MetodContext context, Network network)
        {
            await Response(context, network, null);
        }

        private static async Task Response(MetodContext context, Network network, object[] message)
        {
            var type = context.ReturnType.GenericTypeArguments[0];
            if (type == typeof(string))
            {
                var response = await (Task<string>)context.MethodInfo.Invoke(context.Owner, message);
                network.Response(response);
            }
            else if (type == typeof(int))
            {
                var response = await (Task<int>)context.MethodInfo.Invoke(context.Owner, message);
                network.Response(response);
            }
            else if (type == typeof(uint))
            {
                var response = await (Task<uint>)context.MethodInfo.Invoke(context.Owner, message);
                network.Response(response);
            }
            else if (type == typeof(long))
            {
                var response = await (Task<long>)context.MethodInfo.Invoke(context.Owner, message);
                network.Response(response);
            }
            else if (type == typeof(ulong))
            {
                var response = await (Task<ulong>)context.MethodInfo.Invoke(context.Owner, message);
                network.Response(response);
            }
            else if (type == typeof(float))
            {
                var response = await (Task<float>)context.MethodInfo.Invoke(context.Owner, message);
                network.Response(response);
            }
            else if (type == typeof(decimal))
            {
                var response = await (Task<decimal>)context.MethodInfo.Invoke(context.Owner, message);
                network.Response(response);
            }
            else if (type == typeof(double))
            {
                var response = await (Task<double>)context.MethodInfo.Invoke(context.Owner, message);
                network.Response(response);
            }
            else if (type == typeof(byte))
            {
                var response = await (Task<byte>)context.MethodInfo.Invoke(context.Owner, message);
                network.Response(response);
            }
            else if (type == typeof(sbyte))
            {
                var response = await (Task<sbyte>)context.MethodInfo.Invoke(context.Owner, message);
                network.Response(response);
            }
            else if (type == typeof(bool))
            {
                var response = await (Task<bool>)context.MethodInfo.Invoke(context.Owner, message);
                network.Response(response);
            }
            else if (type == typeof(short))
            {
                var response = await (Task<short>)context.MethodInfo.Invoke(context.Owner, message);
                network.Response(response);
            }
            else if (type == typeof(ushort))
            {
                var response = await (Task<ushort>)context.MethodInfo.Invoke(context.Owner, message);
                network.Response(response);
            }
            else if(type == typeof(char))
            {
                var response = await (Task<char>)context.MethodInfo.Invoke(context.Owner, message);
                network.Response(response);
            }
            else if (type == typeof(DateTime))
            {
                var response = await (Task<DateTime>)context.MethodInfo.Invoke(context.Owner, message);
                network.Response(response);
            }
            else if (type == typeof(Guid))
            {
                var response = await (Task<Guid>)context.MethodInfo.Invoke(context.Owner, message);
                network.Response(response);
            }
            else if (typeof(IMessage).IsAssignableFrom(type))
            {
                var tcs = new TaskCompletionSource<IMessage>(context.MethodInfo.Invoke(context.Owner, message));
                var response = await tcs.Task;
                network.Response(response);
            }
            else
            {
                throw new NetworkException($"消息类型:{type}不支持。");
            }
        }
    }
}
