﻿using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Message
{
    public interface IRequest
    {
        uint MessageId { get; set; }
    }
}
