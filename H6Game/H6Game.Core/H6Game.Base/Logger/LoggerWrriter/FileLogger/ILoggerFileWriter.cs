﻿
using System;
using System.Threading.Tasks;

namespace H6Game.Base.Logger
{
    public interface ILoggerFileWriter : ILoggerWriter
    {
        LogLevel LogLevel { get; }
        Action<string> CreateFileCallBack { get; set; }
        void CreateFile(string customName, string levelName, string fileName);
    }
}
