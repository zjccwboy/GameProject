using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.IO;

namespace H6Game.Base
{
    [Event(EventType.Awake)]
    [SingletCase]
    public class DBConfigComponent : BaseComponent
    {
        public DbConfigEntity ConfigEntity { get; private set; }
        public override void Awake()
        {
            var path = $"{Directory.GetCurrentDirectory()}\\DbConfig.json";
            if (!ReadConfigFile(path))
                SaveConfigile(path);
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

                    ConfigEntity = BsonSerializer.Deserialize<DbConfigEntity>(json);
                }
            }

            if (ConfigEntity ==null || string.IsNullOrWhiteSpace(ConfigEntity.ConnectionString) || string.IsNullOrWhiteSpace(ConfigEntity.DatabaseName)) 
                return false;

            return true;
        }

        private void SaveConfigile(string path)
        {
            ConfigEntity = new DbConfigEntity
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "H6Game",
            };

            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamWriter(fileStream))
                {
                    var json = ConfigEntity.ToJson();
                    sr.Write(json);
                    Log.Logger.Error($"数据库连接信息未配置.");
                }
            }
        }

    }
}
