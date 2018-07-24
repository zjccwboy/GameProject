using H6Game.Base.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace H6Game.Base
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

            if (ConfigEntity.CenterEndPoint != null
                && !string.IsNullOrEmpty(ConfigEntity.CenterEndPoint.IP)
                && ConfigEntity.IPList.Any())
            {
                return true;
            }

            return false;
        }

        private void SaveJsonFile(string path)
        {
            ConfigEntity = new NetConfigEntity
            {
                CenterEndPoint = new EndPointEntity { IP = string.Empty, Desc = "分布式默认启动主机IP端口" },
                MinPort = 40001,
                MaxPort = 40020,
                IPList = new List<string>
                {
                    "127.0.0.1",
                    "192.168.30.13",
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
