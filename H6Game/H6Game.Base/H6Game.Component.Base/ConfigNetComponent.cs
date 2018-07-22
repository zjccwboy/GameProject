using H6Game.Component.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common;

namespace H6Game.Component.Base
{
    public class ConfigNetComponent : BaseComponent
    {
        public NetConfigEntity ConfigEntity;

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

            if (!string.IsNullOrEmpty(ConfigEntity.DespacherServer.IP))
            {
                return true;
            }

            return false;
        }

        private void SaveJsonFile(string path)
        {
            ConfigEntity = new NetConfigEntity
            {
                GameServer = new EndPointEntity { IP = string.Empty, Desc = "游戏服务" },
                GateServer = new EndPointEntity { IP = string.Empty, Desc = "网关服务" },
                AccountServer = new EndPointEntity { IP = string.Empty, Desc = "账号服务" },
                PayServer = new EndPointEntity { IP = string.Empty, Desc = "支付服务" },
                ResourceServer = new EndPointEntity { IP = string.Empty, Desc = "资源服务" },
                LoginServer = new EndPointEntity { IP = string.Empty, Desc = "登陆服务" },
                DespacherServer = new EndPointEntity { IP = string.Empty, Desc = "分布式分发服务" },
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
