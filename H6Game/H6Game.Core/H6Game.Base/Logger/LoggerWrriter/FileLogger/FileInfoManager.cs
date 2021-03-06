﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace H6Game.Base.Logger
{
    public static class FileInfoManager
    {
        public static Dictionary<LogLevel,string> LastCreateFileNames { get; } = new Dictionary<LogLevel, string>();
        public static Dictionary<LogLevel, long> LastCreateFileSize { get; } = new Dictionary<LogLevel, long>();
        public static Dictionary<LogLevel, DateTime> LastCreateFileTime { get; } = new Dictionary<LogLevel, DateTime>();
        public static HashSet<string> LogFiles { get; } = new HashSet<string>();
        public static Dictionary<string, LogLevel> NameLevels { get; } = new Dictionary<string, LogLevel>();
        public static Dictionary<LogLevel, string> LevelNames { get; } = new Dictionary<LogLevel, string>();

        public static void Load()
        {
            SetNameLevels();
        }

        /// <summary>
        /// 更新最新日志文件信息
        /// </summary>
        /// <param name="path"></param>
        public static void UpdateLastCreateFileInfo(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return;
            }

            var directory = new DirectoryInfo(path);
            var files = directory.GetFiles().ToList();
            foreach(var file in files)
            {
                LogFiles.Add(file.Name);
            }

            files = files.OrderByDescending(f=>f.CreationTimeUtc).ToList();
            foreach (var levelName in LevelNames.Values)
            {
                var level = NameLevels[levelName];
                foreach (var fileInfo in files)
                {
                    if (fileInfo.Name.Contains(levelName))
                    {
                        LastCreateFileNames[level] = fileInfo.Name;
                        LastCreateFileSize[level] = fileInfo.Length;
                        LastCreateFileTime[level] = fileInfo.CreationTimeUtc;
                        break;
                    }
                }
            }
        }

        private static void SetNameLevels()
        {
            foreach(LogLevel value in Enum.GetValues(typeof(LogLevel)))
            {
                var name = Enum.GetName(typeof(LogLevel), value);
                NameLevels[name] = value;
                LevelNames[value] = name;
            }
        }
    }
}
