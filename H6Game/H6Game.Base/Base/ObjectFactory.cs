using H6Game.Message;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class ObjectFactory
    {
        private static readonly HashSet<Type> messageTypes = new HashSet<Type>();
        private static readonly HashSet<Type> messageHandlerTypes = new HashSet<Type>();
        private static readonly HashSet<Type> componentTypes = new HashSet<Type>();



    }
}
