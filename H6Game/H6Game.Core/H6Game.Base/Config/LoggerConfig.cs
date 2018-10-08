using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.IO;

namespace H6Game.Base
{
    public class LoggerConfig
    {
        public LoggerConfigEntity Config { get; private set; }
        public LoggerConfig()
        {
            var fullName = $"{Path.GetDirectoryName(this.GetType().Assembly.Location)}\\LoggerConfig.json";
            if (!ReadConfigFile(fullName))
                SaveConfigFile(fullName);
        }

        private bool ReadConfigFile(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamReader(fileStream))
                {
                    var json = sr.ReadToEnd();
                    if (string.IsNullOrEmpty(json))
                        return false;

                    this.Config = BsonSerializer.Deserialize<LoggerConfigEntity>(json);
                }
            }

            if (this.Config == null 
                || string.IsNullOrWhiteSpace(this.Config.Path)
                || this.Config.DBConfig == null
                || string.IsNullOrWhiteSpace(this.Config.DBConfig.ConnectionString)
                || string.IsNullOrWhiteSpace(this.Config.DBConfig.DatabaseName)
                || this.Config.Debug == null
                || this.Config.Info == null
                || this.Config.Notice == null
                || this.Config.Warn == null
                || this.Config.Error == null
                || this.Config.Fatal == null
                )
                return false;

            return true;
        }

        private void SaveConfigFile(string fullName)
        {
            this.Config = new LoggerConfigEntity
            {
                Path = Path.GetDirectoryName(this.GetType().Assembly.Location) + "\\Logs",
                DBConfig = new DBConfigEntity
                {
                    ConnectionString = "mongodb://localhost:27017",
                    DatabaseName = "H6GameLogger",
                },
                SecondInterval = 1200,
                MaxBytes = 1024 * 1024 * 2,
                Debug = new LoggerInfoConfigEntity
                {
                    IsWriteDB = true,
                    IsWriteFile = true,
                    IsShowConsole = true,
                    Desc = "调试日志，项目测试阶段日志。",
                },
                Info = new LoggerInfoConfigEntity
                {
                    IsWriteDB = true,
                    IsWriteFile = true,
                    IsShowConsole = true,
                    Desc = "信息日志，比如程序加载。",
                },
                Notice = new LoggerInfoConfigEntity
                {
                    IsWriteDB = true,
                    IsWriteFile = true,
                    IsShowConsole = true,
                    Desc = "分析日志。",
                },
                Warn = new LoggerInfoConfigEntity
                {
                    IsWriteDB = true,
                    IsWriteFile = true,
                    IsShowConsole = true,
                    Desc = "警告日志。",
                },
                Error = new LoggerInfoConfigEntity
                {
                    IsWriteDB = true,
                    IsWriteFile = true,
                    IsShowConsole = true,
                    Desc = "错误日志，异常捕获日志。",
                },
                Fatal = new LoggerInfoConfigEntity
                {
                    IsWriteDB = true,
                    IsWriteFile = true,
                    IsShowConsole = true,
                    Desc = "严重错误日志，比如应用程序崩溃，业务逻辑错误的错误。",
                },
            };

            using (var fileStream = new FileStream(fullName, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamWriter(fileStream))
                {
                    var json = this.Config.ToJson();
                    sr.Write(json);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"日志配置文件没有配置，系统会自动生成模板：{fullName}。");
                }
            }
        }
    }
}
