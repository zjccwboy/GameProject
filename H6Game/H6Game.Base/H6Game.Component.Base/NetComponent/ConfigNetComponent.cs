using H6Game.Component.Base.Entity;
using System;
using System.IO;

namespace H6Game.Component.Base
{
    public class ConfigNetComponent : BaseComponent
    {
        public NetConfigEntity ConfigEntity { get; private set; }

        public ConfigNetComponent()
        {
            var path = $"{Directory.GetCurrentDirectory()}\\NetConfig.json";
            if (!ReadJsonFile(path))
            {
                SaveJsonFile(path);
            }
        }

        private bool ReadJsonFile(string path)
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
                    ConfigEntity = json.ConvertToObject<NetConfigEntity>();
                }
            }

            if (!string.IsNullOrEmpty(ConfigEntity.RemoteEndPoint.IP) || !string.IsNullOrEmpty(ConfigEntity.LocalEndPoint.IP))
            {
                return true;
            }

            return false;
        }

        private void SaveJsonFile(string path)
        {
            ConfigEntity = new NetConfigEntity
            {
                RemoteEndPoint = new EndPointEntity { IP = string.Empty, Desc = "连接远程服务IP端口" },
                LocalEndPoint = new EndPointEntity { IP = string.Empty, Desc = "本地服务监听IP端口" },
            };

            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamWriter(fileStream))
                {
                    var json = ConfigEntity.ConvertToJson<NetConfigEntity>(Newtonsoft.Json.Formatting.Indented);
                    sr.Write(json);
                    Console.WriteLine("未配置服务IP地址");
                }
            }
        }
    }
}
