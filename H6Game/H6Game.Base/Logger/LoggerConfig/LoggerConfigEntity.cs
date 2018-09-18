
namespace H6Game.Base
{
    public class LoggerConfigEntity
    {
        /// <summary>
        /// 日志文件存放目录路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 日志MongoDB配置项
        /// </summary>
        public DbConfig DBConfig { get; set; }

        /// <summary>
        /// 创建新文件的时间隔，秒为单位
        /// </summary>
        public int SecondInterval { get; set; }

        /// <summary>
        /// 单个日志文件最大字节数
        /// </summary>
        public long MaxBytes { get; set; }

        public LoggerInfoConfigEntity Debug { get; set; }
        public LoggerInfoConfigEntity Info { get; set; }
        public LoggerInfoConfigEntity Notice { get; set; }
        public LoggerInfoConfigEntity Warn { get; set; }
        public LoggerInfoConfigEntity Error { get; set; }
        public LoggerInfoConfigEntity Fatal { get; set; }
    }

    public class LoggerInfoConfigEntity
    {
        public bool IsWriteDB { get; set; }
        public bool IsWriteFile { get; set; }
        public bool IsShowConsole { get; set; }
        public string Desc { get; set; }
    }
}
