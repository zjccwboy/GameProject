using H6Game.Base.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace H6Game.Base
{
    public class ConfigNetComponent : BaseComponent
    {
        public NetConfig ConfigEntity { get; private set; }

        public ConfigNetComponent()
        {
            var path = $"{Directory.GetCurrentDirectory()}\\NetConfig.json";
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
                    ConfigEntity = json.ConvertToObject<NetConfig>();
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
            ConfigEntity = new NetConfig
            {
                IsCenterServer = true,

                InNetConfig = new InNetConfigEntity
                {
                    CenterEndPoint = new EndPointEntity { IP = string.Empty, Desc = "分布式中心服务监听IP端口" },
                    LocalEndPoint = new EndPointEntity { IP = string.Empty, Desc = "本地服务监听IP端口" },
                },

                OuNetConfig = new OutNetConfigEntity
                {
                    Host = "payapi.test.com",
                },
            };

            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamWriter(fileStream))
                {
                    var json = ConfigEntity.ConvertToJson(Newtonsoft.Json.Formatting.Indented);
                    sr.Write(json);
                    Console.WriteLine("未配置服务IP地址端口信息");
                }
            }
        }
    }
}
