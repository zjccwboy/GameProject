using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.IO;

namespace H6Game.Base
{
    public class AppTypeConfig
    {
        public ApplicationConfigEntity Config { get; private set; }

        public AppTypeConfig()
        {
            var fullName = $"{Directory.GetCurrentDirectory()}\\AppTypeConfig.json";
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

                    Config = BsonSerializer.Deserialize<ApplicationConfigEntity>(json);
                }
            }
            return Config != null;
        }

        private async void SaveConfigFile(string fullName)
        {
            Config = new ApplicationConfigEntity
            {
                AppType = ApplicationType.Default,
                Desc = "服务端应用类型配置， 全部应用服务=0,中心服务=1,网关代理服务=2,性能测试=3"
            };

            using (var fileStream = new FileStream(fullName, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamWriter(fileStream))
                {
                    var json = Config.ToJson();
                    await sr.WriteAsync(json);
                    await sr.FlushAsync();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"服务端应用类型未配置，系统会自动生成模板：{fullName}");
                }
            }
        }
    }
}
