using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;

namespace H6Game.Base
{
    public static class MetodContextManager
    {
        private static Dictionary<int, MetodContext> MetodContexts { get; } = new Dictionary<int, MetodContext>();
        static MetodContextManager()
        {
            Load();
        }

        public static bool TryGetContext(int messageCmd, out MetodContext context)
        {
            return MetodContexts.TryGetValue(messageCmd, out context);
        }

        public static void Load()
        {
            var controllerTypes = ObjectPool.GetTypes<IController>();
            foreach(var type in controllerTypes)
            {
                var controller = (IController)Activator.CreateInstance(type);

                var methodInfos = type.GetMethods();
                foreach(var method in methodInfos)
                {
                    var attribute = method.GetCustomAttribute(typeof(NetCommandAttribute));
                    if (attribute == null)
                        continue;

                    var parameters = method.GetParameters();
                    var parameterTypes = parameters == null? new Type[0]: parameters.Select(a => a.ParameterType).ToArray();
                    var context = new MetodContext
                    {
                        Owner = controller,
                        ParameterTypes = parameterTypes,
                        Parameters = new object[parameterTypes.Length],
                        ReturnType = method.ReturnType,
                        IsAsyncMetod = method.ReturnType.BaseType == typeof(Task) || method.ReturnType == typeof(Task),
                        MethodInfo = method,
                    };

                    if(context.IsAsyncMetod && context.ExistReturn())
                    {
                        context.MetodType = MetodType.ExistReturnInvokeAsync;
                    }
                    else if(context.IsAsyncMetod && !context.ExistReturn())
                    {
                        context.MetodType = MetodType.InvokeAsync;
                    }
                    else if(!context.IsAsyncMetod && context.ExistReturn())
                    {
                        context.MetodType = MetodType.ExistReturnInvoke;
                    }
                    else if(!context.IsAsyncMetod && !context.ExistReturn())
                    {
                        context.MetodType = MetodType.Invoke;
                    }

                    var cmds = (attribute as NetCommandAttribute).MessageCmds;
                    foreach(var cmd in cmds)
                    {
                        MetodContexts[cmd] = context;
                    }
                }
            }
        }
    }
}
