using System.IO;

namespace H6Game.Base
{
    [Event(EventType.Awake)]
    [SingletCase]
    public class NetConfigComponent : BaseComponent
    {
        public SysConfig ConfigEntity { get; private set; }

        public override void Awake()
        {
            var path = $"{Directory.GetCurrentDirectory()}\\SysConfig.json";
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

                    ConfigEntity = json.JsonToObject<SysConfig>();
                }
            }

            if(ConfigEntity.InNetConfig == null || ConfigEntity.OuNetConfig == null || string.IsNullOrEmpty(ConfigEntity.OuNetHost))
                return false;

            if (ConfigEntity.InNetConfig.CenterEndPoint != null
                && !string.IsNullOrEmpty(ConfigEntity.InNetConfig.CenterEndPoint.IP)
                && !string.IsNullOrEmpty(ConfigEntity.InNetConfig.LocalEndPoint.IP))
                return true;

            return false;
        }

        private void SaveConfigile(string path)
        {
            ConfigEntity = new SysConfig
            {
                IsCenterServer = true,
                OuNetHost = "payapi.test.com",
                IsCompress = false,
                IsEncrypt = false,

                InNetConfig = new InNetConfigEntity
                {
                    CenterEndPoint = new EndPointEntity { IP = "127.0.0.1", Port = 40000, Desc = "分布式中心服务监听IP端口" },
                    LocalEndPoint = new EndPointEntity { IP = "127.0.0.1", Port = 40000, Desc = "本地服务监听IP端口" },
                },

                OuNetConfig = new EndPointEntity
                {
                    Port = 50000,
                    IP = "127.0.0.1",
                },

#if SERVER
                DbConfig = new DbConfigEntity
                {
                    ConnectionString = "mongodb://localhost:27017",
                },
#endif
            };

            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamWriter(fileStream))
                {
                    var json = ConfigEntity.ToJson(Newtonsoft.Json.Formatting.Indented);
                    sr.Write(json);
                    LogRecord.Log(LogLevel.Error, $"{this.GetType()}/SaveConfigile", $"未配置服务IP地址端口信息.");
                }
            }
        }
    }
}
