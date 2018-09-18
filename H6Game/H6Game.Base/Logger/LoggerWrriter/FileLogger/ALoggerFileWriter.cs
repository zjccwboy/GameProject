using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H6Game.Base
{
    public abstract class ALoggerFileWriter : ILoggerFileWriter
    {
        private LoggerConfigEntity Config { get; }
        private StreamWriter logOutput { get; set; }
        private LoggerFileWriterFatory WriterFatory { get; }
        private StringBuilder MessageBuilder { get; } = new StringBuilder();
        private string Path { get; }

        public abstract LogLevel LogLevel { get; }
        public bool IsWorking { get; private set; }
        public bool IsCreateNew { get; set; }

        public ALoggerFileWriter(LoggerFileWriterFatory writerFatory)
        {
            this.Config = Game.Scene.GetComponent<LoggerConfigComponent>().Config;
            this.WriterFatory = writerFatory;
            this.Path = this.Config.Path.EndsWith("\\") ? this.Config.Path : this.Config.Path + "\\";
        }

        public void CreateFile()
        {
            if (!CanWrite(this.LogLevel))
                return;

            if (logOutput != null)
            {
                logOutput.Flush();
                logOutput.Dispose();
                logOutput = null;
            }

            var fileName = GetFileName();
            logOutput = new StreamWriter(File.Open(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite), Encoding.UTF8);
            var fileInfo = new FileInfo(fileName);
            FileInfoManager.LastCreateFileNames[this.LogLevel] = fileInfo.Name;
            FileInfoManager.LastCreateFileSize[this.LogLevel] = fileInfo.Length;
            FileInfoManager.LastCreateFileTime[this.LogLevel] = fileInfo.CreationTimeUtc;
        }

        public bool CanWrite(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return Config.Debug.IsWriteFile;
                case LogLevel.Info:
                    return Config.Info.IsWriteFile;
                case LogLevel.Notice:
                    return Config.Notice.IsWriteFile;
                case LogLevel.Warn:
                    return Config.Warn.IsWriteFile;
                case LogLevel.Error:
                    return Config.Error.IsWriteFile;
                case LogLevel.Fatal:
                    return Config.Fatal.IsWriteFile;
            }
            return true;
        }

        public bool CanWrite()
        {
            if (!FileInfoManager.LastCreateFileSize.TryGetValue(this.LogLevel, out long size))
                return false;

            if (size >= this.Config.MaxBytes)
                return false;

            if (!FileInfoManager.LastCreateFileTime.TryGetValue(this.LogLevel, out DateTime dateTime))
                return false;

            var span = DateTime.UtcNow - dateTime;
            if (span.TotalSeconds >= this.Config.SecondInterval)
                return false;

            return true;
        }

        public async Task WriteMessage(LoggerEntity entity)
        {
            IsWorking = true;

            if (this.IsCreateNew)
            {
                this.IsCreateNew = false;
                CreateFile();
            }

            MessageBuilder.Clear();
            MessageBuilder.Append($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ")}{Enum.GetName(typeof(LogLevel), this.LogLevel)}");
            if (!string.IsNullOrEmpty(entity.FMessage))
            {
                MessageBuilder.Append($" {entity.FMessage}");
            }
            if (!string.IsNullOrEmpty(entity.FArgs))
            {
                MessageBuilder.Append($" args:{entity.FArgs}");
            }
            if (!string.IsNullOrEmpty(entity.FExceptionName))
            {
                MessageBuilder.AppendLine($"exception name:{entity.FExceptionName}");
                MessageBuilder.AppendLine($"exception:{ entity.FExceptionInfo}");
            }
            await this.logOutput.WriteLineAsync(MessageBuilder.ToString());
            this.logOutput.Flush();

            FileInfoManager.LastCreateFileSize[this.LogLevel] += MessageBuilder.Length;

            IsWorking = false;
        }

        private string GetFileName()
        {
            var isNeedNewFile = !CanWrite();
            if (isNeedNewFile)
            {
                var levelName = FileInfoManager.LevelNames[this.LogLevel];
                var customName = GetCustomNameName();
                var fileName = $"{levelName} {customName}";
                return $"{this.Path}{fileName}";
            }

            return $"{this.Path}{FileInfoManager.LastCreateFileNames[this.LogLevel]}";
        }
        
        private const string StartName = "100000";
        private string GetCustomNameName()
        {
            var newName = StartName;
            var isAddOne = true;
            if(!FileInfoManager.LastCreateFileNames.TryGetValue(this.LogLevel, out string fileName))
            {
                if(FileInfoManager.LastCreateFileNames.Count != 0)
                {
                    fileName = FileInfoManager.LastCreateFileNames.Values.FirstOrDefault();
                    isAddOne = false;
                }
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                var oldName = GetVerName(fileName);
                var number = int.Parse(oldName);
                newName = isAddOne ? (number + 1).ToString() : number.ToString();
            }

            return $"{newName} .log";
        }

        private static string[] SplitStr = new string[] { " " };
        private string GetVerName(string fileName)
        {
            var list = fileName.Split(SplitStr, StringSplitOptions.RemoveEmptyEntries);
            return list[1];
        }
    }
}
