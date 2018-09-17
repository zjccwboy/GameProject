﻿using H6Game.Hotfix.Messages.Enums;
using System;

namespace H6Game.Hotfix.Messages.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageTypeAttribute : Attribute
    {
        public int TypeCode { get; }

        public MessageTypeAttribute(MessageType type)
        {
            TypeCode = (int)type;
        }
    }
}