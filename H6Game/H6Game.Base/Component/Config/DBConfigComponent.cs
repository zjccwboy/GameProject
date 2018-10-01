using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.IO;

namespace H6Game.Base
{
    public class DBConfigComponent
    {
        public DbConfig Config { get; private set; }
        public DBConfigComponent()
        {
            var fullName = $"{Directory.GetCurrentDirectory()}\\H6Game.DbConfig.json";
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

                    Config = BsonSerializer.Deserialize<DbConfig>(json);
                }
            }

            if (Config == null || string.IsNullOrWhiteSpace(Config.ConnectionString) || string.IsNullOrWhiteSpace(Config.DatabaseName)) 
                return false;

            return true;
        }

        private async void SaveConfigFile(string fullName)
        {
            Config = new DbConfig
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "H6Game",
            };

            using (var fileStream = new FileStream(fullName, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamWriter(fileStream))
                {
                    var json = Config.ToJson();
                    await sr.WriteAsync(json);
                    await sr.FlushAsync();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"数据库连接信息未配置，系统会自动生成模板：{fullName}");
                }
            }
        }

    }
}
