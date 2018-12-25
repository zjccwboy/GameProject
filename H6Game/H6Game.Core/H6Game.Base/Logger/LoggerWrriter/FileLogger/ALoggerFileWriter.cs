using H6Game.Base.Config;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H6Game.Base.Logger
{
    public abstract class ALoggerFileWriter : ILoggerFileWriter
    {
        private LoggerConfigEntity Config { get; }
        private StreamWriter logOutput { get; set; }
        private StringBuilder MessageBuilder { get; } = new StringBuilder();
        private string Path { get; }

        public abstract LogLevel LogLevel { get; }
        public bool IsWorking { get; private set; }
        public bool IsCreateNew { get; set; }

        public ALoggerFileWriter()
        {
            this.Config = LoggerFactory.Config;
            this.Path = this.Config.Path.EndsWith("\\") ? this.Config.Path : this.Config.Path + "\\";
        }

        public void CreateNewFile()
        {
            if (!CanWrite(this.LogLevel))
                return;

            if (logOutput != null)
            {
                logOutput.Flush();
                logOutput.Dispose();
                logOutput = null;
            }

            var fileName = NewFileFullName();
            logOutput = new StreamWriter(File.Open(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite), Encoding.UTF8);
            SetFileInfo(fileName);
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

        public async Task WriteMessage(TLogger entity)
        {
            IsWorking = true;

            if (this.IsCreateNew)
            {
                this.IsCreateNew = false;
                CreateNewFile();
            }

            MessageBuilder.Clear();
            MessageBuilder.Append($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ")}{Enum.GetName(typeof(LogLevel), this.LogLevel)} {entity.FStackInfo}");
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
                MessageBuilder.AppendLine();
                MessageBuilder.AppendLine($"name:{entity.FExceptionName}");
                MessageBuilder.AppendLine($"message:{entity.FExceptionMessage}");
                MessageBuilder.AppendLine($"exception:{entity.FExceptionInfo.Trim()}");
            }
            await this.logOutput.WriteLineAsync(MessageBuilder.ToString());
            this.logOutput.Flush();

            FileInfoManager.LastCreateFileSize[this.LogLevel] += MessageBuilder.Length;

            IsWorking = false;
        }

        private void SetFileInfo(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            FileInfoManager.LastCreateFileNames[this.LogLevel] = fileInfo.Name;
            FileInfoManager.LastCreateFileSize[this.LogLevel] = fileInfo.Length;
            FileInfoManager.LastCreateFileTime[this.LogLevel] = DateTime.UtcNow;
        }

        private string NewFileFullName()
        {
            var levelName = FileInfoManager.LevelNames[this.LogLevel];
            var customName = GetCustomNameName();
            var fileName = $"{levelName} {customName}";
            return $"{this.Path}{fileName}";
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
