using MongoDB.Driver;
using System;
using System.Linq;

namespace H6Game.Base
{
    [ComponentEvent(EventType.Awake)]
    [SingleCase]
    public class MongoConfig : BaseComponent
    {
        public override void Awake()
        {
            Game.Scene.AddComponent<EntityComponent>();
            AddRpositoryComponents();
            Log.Info("MongoDB初始化成功。", LoggerBllType.System);
        }

        private void AddRpositoryComponents()
        {
            var sysConfig = new DBConfig().Config;
            var sysDbClient = new MongoClient(sysConfig.ConnectionString);
            var sysDb = sysDbClient.GetDatabase(sysConfig.DatabaseName);
            
            var logConfig = new LoggerConfig().Config.DBConfig;
            var logDbClient = new MongoClient(logConfig.ConnectionString);
            var logDb = logDbClient.GetDatabase(logConfig.DatabaseName);

            var types = ObjectPool.GetTypes<IRpository>();
            foreach (var type in types)
            {
                AddComponent(type, sysConfig, logConfig, sysDbClient, logDbClient, sysDb, logDb);
            }
        }

        private void AddComponent(Type type, DBConfigEntity sysConfig, DBConfigEntity logConfig, MongoClient sysDbClient, MongoClient logDbClient, IMongoDatabase sysDb, IMongoDatabase logDb)
        {
            if (!typeof(IRpository).IsAssignableFrom(type))
                return;

            var isSingle = ComponentPool.IsSingleType(type);
            if (!isSingle)
                throw new ComponentException("规定Rpository类型组件只能定义成单例(SingleCase)组件。");

            var component = ComponentPool.Fetch(type);
            var rpository = component as IRpository;
            if(rpository.DBType == DBType.SysDb)
            {
                rpository.SetDBContext(sysDb, sysConfig.DatabaseName, sysDbClient);
                Game.Scene.AddComponent(component);
            }
            else if(rpository.DBType == DBType.LoggerDb)
            {
                rpository.SetDBContext(logDb, logConfig.DatabaseName, logDbClient);
                Game.Scene.AddComponent(component);
            }
        }
    }
}
