using H6Game.Base.Entity;
using System.IO;

namespace H6Game.Base
{
    public class ConfigNetComponent : BaseComponent
    {
        public SysConfig ConfigEntity { get; private set; }

        public override void Start()
        {
            var path = $"{Directory.GetCurrentDirectory()}\\SysConfig.json";
            if (!ReadConfigFile(path))
            {
                SaveConfigile(path);
            }
        }

        private bool ReadConfigFile(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamReader(fileStream))
                {
                    var json = sr.ReadToEnd();
                    if (string.IsNullOrEmpty(json))
                    {
                        return false;
                    }
                    ConfigEntity = json.ConvertToObject<SysConfig>();
                }
            }

            if(ConfigEntity.InNetConfig == null || ConfigEntity.OuNetConfig == null)
            {
                return false;
            }

            if (ConfigEntity.InNetConfig.CenterEndPoint != null
                && !string.IsNullOrEmpty(ConfigEntity.InNetConfig.CenterEndPoint.IP)
                && !string.IsNullOrEmpty(ConfigEntity.InNetConfig.LocalEndPoint.IP)
                && !string.IsNullOrEmpty(ConfigEntity.OuNetConfig.Host))
            {
                return true;
            }

            return false;
        }

        private void SaveConfigile(string path)
        {
            ConfigEntity = new SysConfig
            {
                IsCenterServer = true,

                InNetConfig = new InNetConfigEntity
                {
                    CenterEndPoint = new EndPointEntity { IP = "127.0.0.1", Port = 40000, Desc = "分布式中心服务监听IP端口" },
                    LocalEndPoint = new EndPointEntity { IP = "127.0.0.1", Port = 40000, Desc = "本地服务监听IP端口" },
                },

                OuNetConfig = new OutNetConfigEntity
                {
                    Port = 50001,
                    Host = "payapi.test.com",
                },

#if SERVER
                DbConfig = new DbConfigEntity
                {
                    ConnectionString = string.Empty,
                },
#endif
            };

            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamWriter(fileStream))
                {
                    var json = ConfigEntity.ConvertToJson(Newtonsoft.Json.Formatting.Indented);
                    sr.Write(json);
                    LogRecord.Log(LogLevel.Error, $"{this.GetType()}/SaveConfigile", $"未配置服务IP地址端口信息.");
                }
            }
        }
    }
}
