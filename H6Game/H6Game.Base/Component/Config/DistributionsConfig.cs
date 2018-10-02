using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.IO;

namespace H6Game.Base
{
    public sealed class DistributionsConfig
    {
        public DistributionsConfigEntity Config { get; private set; }

        public DistributionsConfig()
        {
            var fullName = $"{Directory.GetCurrentDirectory()}\\H6Game.DistributionsConfig.json";
            if (!ReadConfigFile(fullName))
                SaveConfigFile(fullName);

            PacketConfig.IsCompress = Config.IsCompress;
            PacketConfig.IsEncrypt = Config.IsEncrypt;
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

                    Config = BsonSerializer.Deserialize<DistributionsConfigEntity>(json);
                }
            }

            if(Config.InnerListenConfig == null || Config.OutListenConfig == null)
                return false;

            if (Config.InnerListenConfig.CenterEndPoint != null
                && !string.IsNullOrEmpty(Config.InnerListenConfig.CenterEndPoint.IP)
                && !string.IsNullOrEmpty(Config.InnerListenConfig.LocalEndPoint.IP))
                return true;

            return false;
        }

        private async void SaveConfigFile(string fullName)
        {
            Config = new DistributionsConfigEntity
            {
                IsCenterServer = false,
                IsCompress = false,
                IsEncrypt = false,

                InnerListenConfig = new InnerListenConfigEntity
                {
                    CenterEndPoint = new EndPointConfigEntity { IP = "127.0.0.1", Port = 40000, Desc = "分布式中心服务监听IP端口" },
                    LocalEndPoint = new EndPointConfigEntity { IP = "127.0.0.1", Port = 40000, Desc = "本地服务监听IP端口" },
                },

                OutListenConfig = new EndPointConfigEntity
                {
                    Port = 50000,
                    IP = "127.0.0.1",
                },
            };

            using (var fileStream = new FileStream(fullName, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamWriter(fileStream))
                {
                    var json = Config.ToJson();
                    await sr.WriteAsync(json);
                    await sr.FlushAsync();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"未配置服务IP地址端口信息，系统会自动生成模板：{fullName}");
                }
            }
        }
    }
}
