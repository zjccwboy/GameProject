using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.IO;

namespace H6Game.Base
{
    [Event(EventType.Awake)]
    [SingletCase]
    public sealed class DistributionsConfigComponent : BaseComponent
    {
        public DistributionsConfig InnerConfig { get; private set; }

        public override void Awake()
        {
            var path = $"{Directory.GetCurrentDirectory()}\\DistributionsConfig.json";
            if (!ReadConfigFile(path))
                SaveConfigile(path);

            PacketConfig.IsCompress = InnerConfig.IsCompress;
            PacketConfig.IsEncrypt = InnerConfig.IsEncrypt;
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

                    InnerConfig = BsonSerializer.Deserialize<DistributionsConfig>(json);
                }
            }

            if(InnerConfig.InnerListenConfig == null || InnerConfig.OutListenConfig == null)
                return false;

            if (InnerConfig.InnerListenConfig.CenterEndPoint != null
                && !string.IsNullOrEmpty(InnerConfig.InnerListenConfig.CenterEndPoint.IP)
                && !string.IsNullOrEmpty(InnerConfig.InnerListenConfig.LocalEndPoint.IP))
                return true;

            return false;
        }

        private void SaveConfigile(string path)
        {
            InnerConfig = new DistributionsConfig
            {
                IsCenterServer = false,
                IsCompress = false,
                IsEncrypt = false,

                InnerListenConfig = new InnerListenConfig
                {
                    CenterEndPoint = new EndPointConfig { IP = "127.0.0.1", Port = 40000, Desc = "分布式中心服务监听IP端口" },
                    LocalEndPoint = new EndPointConfig { IP = "127.0.0.1", Port = 40000, Desc = "本地服务监听IP端口" },
                },

                OutListenConfig = new EndPointConfig
                {
                    Port = 50000,
                    IP = "127.0.0.1",
                },
            };

            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamWriter(fileStream))
                {
                    var json = InnerConfig.ToJson();
                    sr.Write(json);
                    Log.Logger.Error($"未配置服务IP地址端口信息.");
                }
            }
        }
    }
}
