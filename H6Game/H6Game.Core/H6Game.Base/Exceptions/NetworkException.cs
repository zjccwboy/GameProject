﻿using System;

namespace H6Game.Base
{
    public class NetworkException : AggregateException
    {
        public NetworkException(string message) : base(message) { }
    }
}