using H6Game.Base.Config;
using H6Game.Base.SyncContext;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace H6Game.Base.Logger
{
    public class CreateFileCallbackModel
    {
        public string CustomName { get; set; }
        public string LevelName { get; set; }
        public string FileName { get; set; }
    }

    public abstract class ALoggerFileWriter : SynchronizationThreadContextObject, ILoggerFileWriter
    {
        private LoggerConfigEntity Config { get; }
        private StreamWriter logOutput { get; set; }
        private StringBuilder MessageBuilder { get; } = new StringBuilder();
        private string Path { get; }
        public abstract LogLevel LogLevel { get; }
        public Action<string> CreateFileCallBack { get; set; }
        public ALoggerFileWriter()
        {
            this.Config = LoggerFactory.Config;
            this.Path = this.Config.Path.EndsWith("\\") ? this.Config.Path : this.Config.Path + "\\";
        }
        public async Task CreateFile(string customName, string levelName, string fileName)
        {
            if (this.logOutput != null)
            {
                await this.logOutput.FlushAsync();
                this.logOutput.Dispose();
                this.logOutput = null;
            }

            var fullName = $"{this.Path}{fileName}";
            this.logOutput = new StreamWriter(File.Open(fullName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite), Encoding.UTF8);

            var fileInfo = new FileInfo(fullName);
            fileInfo.CreationTime = DateTime.Now;

            FileInfoManager.LastCreateFileNames[this.LogLevel] = fileName;
            FileInfoManager.LastCreateFileSize[this.LogLevel] = 0;
            FileInfoManager.LastCreateFileTime[this.LogLevel] = DateTime.UtcNow;

            CreateFileCallBack?.Invoke(customName);
        }

        private void OpenFile(string fileName)
        {
            if (logOutput != null)
                return;

            var fullName = $"{this.Path}{fileName}";
            this.logOutput = new StreamWriter(File.Open(fullName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite), Encoding.UTF8);
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

        public async Task WriteMessage(TLogger entity)
        {
            if (!CanWrite(this.LogLevel))
                return;

            try
            {
                var levelName = FileInfoManager.LevelNames[this.LogLevel];
                var customName = GetCustomNameName();
                var fileName = $"{customName}_{levelName}.log";

                var isCreateNew = FileInfoManager.LogFiles.Add(fileName);
                if (isCreateNew)
                {
                    await CreateFile(customName, levelName, fileName);
                }
                else
                {
                    OpenFile(fileName);
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
                FileInfoManager.LastCreateFileSize[this.LogLevel] += MessageBuilder.Length;
                await this.SyncContext;
                this.logOutput.Flush();
            }
            catch(Exception e)
            {
                Console.Write(e.ToString());
            }
        }

        private const string StartName = "100000";
        private string GetCustomNameName()
        {
            var newName = StartName;
            if(!FileInfoManager.LastCreateFileNames.TryGetValue(this.LogLevel, out string fileName))
            {
                return newName;
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                var lastCreateTime = FileInfoManager.LastCreateFileTime[this.LogLevel];
                var fileSize = (int)FileInfoManager.LastCreateFileSize[this.LogLevel];
                var writeInterval = (DateTime.UtcNow - lastCreateTime).TotalSeconds;

                var oldName = GetVerName(fileName);
                var number = int.Parse(oldName);
                if (fileSize > this.Config.MaxBytes || writeInterval > this.Config.SecondInterval)
                {
                    newName = (number + 1).ToString();
                }
                else
                {
                    var levelName = FileInfoManager.LevelNames[this.LogLevel];
                    var addName = (number + 1).ToString();
                    foreach (var ln in FileInfoManager.LevelNames.Values)
                    {
                        if (ln == levelName)
                            continue;

                        var comparName = $"{addName}_{ln}.log";
                        if (!FileInfoManager.LogFiles.Contains(comparName))
                            continue;

                        return addName;
                    }
                    newName = number.ToString();
                }
            }
            return newName;
        }

        private static string[] SplitStr = new string[] { "_" };
        private string GetVerName(string fileName)
        {
            var list = fileName.Split(SplitStr, StringSplitOptions.RemoveEmptyEntries);
            return list[0];
        }
    }
}
