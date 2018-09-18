using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
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
            var fullName = $"{Directory.GetCurrentDirectory()}\\H6Game.OutNetConfig.json";
            if (!ReadConfigFile(fullName))
                SaveConfigile(fullName);

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

        private async void SaveConfigile(string fullName)
        {
            OutNetConfig = new OutNetConfig
            {
                OutNetHost = "payapi.test.com",
                IsCompress = false,
                IsEncrypt = false,
                Port = 50000,
            };

            using (var fileStream = new FileStream(fullName, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamWriter(fileStream))
                {
                    var json = OutNetConfig.ToJson();
                    await sr.WriteAsync(json);
                    await sr.FlushAsync();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"外网连接信息未配置，系统会自动生成模板：{fullName}");
                }
            }
        }
    }
}
