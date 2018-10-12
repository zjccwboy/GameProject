using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.IO;

namespace H6Game.Base
{
    public class NetConnectingConfig
    {
        public NetConnectConfigEntity Config { get; private set; }

        public NetConnectingConfig()
        {
            var fullName = $"{Directory.GetCurrentDirectory()}\\NetConnecting.json";
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

                    Config = BsonSerializer.Deserialize<NetConnectConfigEntity>(json);
                }
            }

            if (Config == null || string.IsNullOrWhiteSpace(Config.ProxyHost))
                return false;

            return true;
        }

        private async void SaveConfigFile(string fullName)
        {
            Config = new NetConnectConfigEntity
            {
                ProxyHost = "payapi.test.com",
                ProtocalType = ProtocalType.Kcp,
                IsCompress = false,
                IsEncrypt = false,
                Port = 50000,
                Desc = "客户端连接服务端的IP端口配置。",
            };

            using (var fileStream = new FileStream(fullName, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamWriter(fileStream))
                {
                    var json = Config.ToJson();
                    await sr.WriteAsync(json);
                    await sr.FlushAsync();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"外网连接信息未配置，系统会自动生成模板：{fullName}");
                }
            }
        }
    }
}
