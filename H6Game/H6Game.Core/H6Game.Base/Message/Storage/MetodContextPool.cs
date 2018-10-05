using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

namespace H6Game.Base
{
    public static class MetodContextPool
    {
        private static Dictionary<int, List<MetodContext>> MetodContexts { get; } = new Dictionary<int, List<MetodContext>>();
        private static Dictionary<Type, HashSet<int>> ControllerCmds { get; } = new Dictionary<Type, HashSet<int>>();

        public static bool TryGetContext(int netCommand, out List<MetodContext> contexts)
        {
            return MetodContexts.TryGetValue(netCommand, out contexts);
        }

        public static void Remove(IController controller)
        {
            var type = controller.GetType();
            if (!ControllerCmds.TryGetValue(type, out HashSet<int> cmds))
                return;

            foreach (var cmd in cmds)
            {
                if (MetodContexts.TryGetValue(cmd, out List<MetodContext> contexts))
                {
                    var deletes = contexts.Where(a => a.Owner.GetType() == type);
                    foreach (var context in deletes)
                    {
                        contexts.Remove(context);
                    }
                }
            }
            ControllerCmds.Remove(type);
        }

        public static void Add(IController controller)
        {
            var type = controller.GetType();

            var hsCmds = new HashSet<int>();
            ControllerCmds[type] = hsCmds;

            var methodInfos = type.GetMethods();
            foreach (var method in methodInfos)
            {
                var attribute = method.GetCustomAttribute(typeof(NetCommandAttribute));
                if (attribute == null)
                    continue;

                var parameters = method.GetParameters();
                var parameterTypes = parameters == null ? new Type[0] : parameters.Select(a => a.ParameterType).ToArray();
                var context = new MetodContext
                {
                    Owner = controller,
                    ParameterTypes = parameterTypes,
                    Parameters = new object[parameterTypes.Length],
                    ReturnType = method.ReturnType,
                    IsAsyncMetod = method.ReturnType.BaseType == typeof(Task) || method.ReturnType == typeof(Task),
                    MethodInfo = method,
                };

                if (context.IsAsyncMetod && context.ExistReturn())
                {
                    context.MetodType = MetodType.ExistReturnInvokeAsync;
                }
                else if (context.IsAsyncMetod && !context.ExistReturn())
                {
                    context.MetodType = MetodType.InvokeAsync;
                }
                else if (!context.IsAsyncMetod && context.ExistReturn())
                {
                    context.MetodType = MetodType.ExistReturnInvoke;
                }
                else if (!context.IsAsyncMetod && !context.ExistReturn())
                {
                    context.MetodType = MetodType.Invoke;
                }

                var cmds = (attribute as NetCommandAttribute).MessageCmds;
                foreach (var cmd in cmds)
                {
                    if (!MetodContexts.TryGetValue(cmd, out List<MetodContext> contexts))
                    {
                        contexts = new List<MetodContext>();
                        MetodContexts[cmd] = contexts;
                    }

                    ValidateContext(contexts, context, cmd);

                    contexts.Add(context);
                    hsCmds.Add(cmd);
                }
            }
        }

        /// <summary>
        /// 检查方法上下文中注册
        /// </summary>
        /// <param name="contexts"></param>
        /// <returns></returns>
        private static void ValidateContext(List<MetodContext> contexts, MetodContext newContext, int netCommand)
        {
            foreach (var context in contexts)
            {
                //两个无参方法不能订阅相同的NetCommand
                if (context.ParameterTypes.Length == 0 && newContext.ParameterTypes.Length == 0)
                    ThrowRepeatError(context, newContext, netCommand);


                //两个参数一样的方法不能订阅相同的NetCommand
                if (context.ParameterTypes[0] == newContext.ParameterTypes[0])
                    ThrowRepeatError(context, newContext, netCommand);

                if(newContext.ParameterTypes.Length == 0)
                {
                    var subscribers = MessageSubscriberPool.GetSubscribers(netCommand, null);
                    if (subscribers.Any())
                    {
                        ThrowRepeatSubscriberError(subscribers, newContext, netCommand);
                    }
                }
                else
                {
                    var subscribers = MessageSubscriberPool.GetSubscribers(netCommand, newContext.ParameterTypes[0]);
                    if (subscribers.Any())
                    {
                        ThrowRepeatSubscriberError(subscribers, newContext, netCommand);
                    }
                }
            }
        }

        private static void ThrowRepeatError(MetodContext context, MetodContext newContext, int netCommand)
        {
            throw new ControllerException($"方法:{context.MethodInfo.ReflectedType.Name}/{context.MethodInfo.Name}" +
                $"与方法:{newContext.MethodInfo.ReflectedType.Name}/{newContext.MethodInfo.Name}参数相同，" +
                $"消息类型相同时不能订阅一样的NetCommand:{netCommand}");
        }

        private static void ThrowRepeatSubscriberError(IEnumerable<ISubscriber> subscribers, MetodContext newContext, int netCommand)
        {
            var builder = new StringBuilder();
            foreach (var subscriber in subscribers)
            {
                builder.Append(nameof(subscriber));
                builder.Append(",");
            }
            throw new ControllerException($"方法:{newContext.MethodInfo.ReflectedType.Name}/{newContext.MethodInfo.Name} " +
                $"与类:{builder.ToString()}订阅消息类型相同，消息类型相同时不能订阅一样的NetCommand:{netCommand}");
        }
    }
}
