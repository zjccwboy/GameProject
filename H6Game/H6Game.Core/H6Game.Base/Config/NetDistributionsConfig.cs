using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.IO;

namespace H6Game.Base
{
    public sealed class NetDistributionsConfig
    {
        public NetConfigEntity Config { get; private set; }

        public NetDistributionsConfig()
        {
            var fullName = $"{Directory.GetCurrentDirectory()}\\DistributionsConfig.json";
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

                    Config = BsonSerializer.Deserialize<NetConfigEntity>(json);
                }
            }

            if(this.Config.CenterAcceptConfig == null || this.Config.LocalAcceptConfig == null || this.Config.OuterAcceptConfig == null)
                return false;

            if (Config.OuterAcceptConfig.OuterKcpAcceptConfig != null
                && this.Config.OuterAcceptConfig.OuterTcpAcceptConfig != null
                && this.Config.OuterAcceptConfig.OuterWebSocketConfig != null)
                return true;

            return false;
        }

        private async void SaveConfigFile(string fullName)
        {
            Config = new NetConfigEntity
            {
                IsProxyServer = false,
                IsCompress = false,
                IsEncrypt = false,
                CenterAcceptConfig = new EndPointConfigEntity { IP = "127.0.0.1", Port = 40000, Desc = "分布式中心服务监听IP端口" },
                LocalAcceptConfig = new EndPointConfigEntity { IP = "127.0.0.1", Port = 40000, Desc = "本地服务监听IP端口" },
                OuterAcceptConfig = new OuterAcceptConfigEntity
                {
                    OuterKcpAcceptConfig = new EndPointConfigEntity
                    {
                        Enable = true,
                        Port = 50000,
                        IP = "127.0.0.1",
                        ProtocalType =  ProtocalType.Kcp,
                        Desc = "监听KCP外网连接的IP端口配置项，一般用于跟客户端连接的网关监听IP端口。",
                    },
                    OuterTcpAcceptConfig = new EndPointConfigEntity
                    {
                        Enable = true,
                        Port = 50000,
                        IP = "127.0.0.1",
                        ProtocalType = ProtocalType.Tcp,
                        Desc = "监听TCP外网连接的IP端口配置项，一般用于跟客户端连接的网关监听IP端口。",
                    },
                    OuterWebSocketConfig = new EndPointWebSocketConfigEntity
                    {
                        Enable = true,
                        ProtocalType = ProtocalType.Wcp,
                        HttpPrefixed = "http://127.0.0.1:9000/",
                        Desc = "监听WebSocket外网连接的IP端口配置项，一般用于跟客户端连接的网关监听IP端口。",
                    },
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
