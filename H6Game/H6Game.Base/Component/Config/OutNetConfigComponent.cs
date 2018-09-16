using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.IO;

namespace H6Game.Base
{
    [Event(EventType.Awake)]
    [SingletCase]
    public class OutNetConfigComponent : BaseComponent
    {
        public OutNetConfig OutNetConfig { get; private set; }
        public override void Awake()
        {
            var path = $"{Directory.GetCurrentDirectory()}\\H6Game.OutNetConfig.json";
            if (!ReadConfigFile(path))
                SaveConfigile(path);

            PacketConfig.IsCompress = OutNetConfig.IsCompress;
            PacketConfig.IsEncrypt = OutNetConfig.IsEncrypt;
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

                    OutNetConfig = BsonSerializer.Deserialize<OutNetConfig>(json);
                }
            }

            if (OutNetConfig == null || string.IsNullOrWhiteSpace(OutNetConfig.OutNetHost))
                return false;

            return true;
        }

        private void SaveConfigile(string path)
        {
            OutNetConfig = new OutNetConfig
            {
                OutNetHost = "payapi.test.com",
                IsCompress = false,
                IsEncrypt = false,
                Port = 50000,
            };

            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamWriter(fileStream))
                {
                    var json = OutNetConfig.ToJson();
                    sr.Write(json);
                    Log.Logger.Error($"外网连接信息未配置.");
                }
            }
        }
    }
}
