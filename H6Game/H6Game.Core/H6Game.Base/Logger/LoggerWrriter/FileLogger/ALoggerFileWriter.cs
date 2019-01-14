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
    public abstract class ALoggerFileWriter : SynchronizationThreadContextObject, ILoggerFileWriter
    {
        private LoggerConfigEntity Config { get; }
        private StreamWriter logOutput { get; set; }
        private string Path { get; }
        private DateTime FileLastCreateTime { get { return FileInfoManager.LastCreateFileTime[this.LogLevel]; } }
        private int LastCreateFileSize { get { return (int)FileInfoManager.LastCreateFileSize[this.LogLevel]; } }
        private string LevelName { get { return FileInfoManager.LevelNames[this.LogLevel]; } }
        public abstract LogLevel LogLevel { get; }
        public Action<string> CreateFileCallBack { get; set; }
        public ALoggerFileWriter()
        {
            this.Config = LoggerFactory.Config;
            this.Path = this.Config.Path.EndsWith("\\") ? this.Config.Path : this.Config.Path + "\\";
        }
        public void CreateFile(string customName, string levelName, string fileName)
        {
            if (this.logOutput != null)
            {
                this.logOutput.Flush();
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

            var levelName = FileInfoManager.LevelNames[this.LogLevel];
            var customName = GetCustomNameName();
            var fileName = GenerateFileName(customName, levelName);

            var isCreateNew = FileInfoManager.LogFiles.Add(fileName);
            if (isCreateNew)
            {
                CreateFile(customName, levelName, fileName);
            }
            else
            {
                OpenFile(fileName);
            }

            var logStr = GenrateLogString(entity);
            try
            {
                await this.logOutput.WriteLineAsync(logStr);
                this.logOutput.Flush();
                FileInfoManager.LastCreateFileSize[this.LogLevel] += logStr.Length;
                await this.SyncContext;
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
        }

        private string GenrateLogString(TLogger entity)
        {
            var builder = new StringBuilder();
            builder.Append($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ")}{Enum.GetName(typeof(LogLevel), this.LogLevel)} {entity.FStackInfo}");
            if (!string.IsNullOrEmpty(entity.FMessage))
            {
                builder.Append($" {entity.FMessage}");
            }
            if (!string.IsNullOrEmpty(entity.FArgs))
            {
                builder.Append($" args:{entity.FArgs}");
            }
            if (!string.IsNullOrEmpty(entity.FExceptionName))
            {
                builder.AppendLine();
                builder.AppendLine($"name:{entity.FExceptionName}");
                builder.AppendLine($"message:{entity.FExceptionMessage}");
                builder.AppendLine($"exception:{entity.FExceptionInfo.Trim()}");
            }
            return builder.ToString();
        }
        private string GetCustomNameName()
        {
            const string startName = "100000";
            var newName = startName;

            if (!FileInfoManager.LastCreateFileNames.TryGetValue(this.LogLevel, out string fileName))
            {
                return newName;
            }

            var oldName = GetVerName(fileName);
            var number = int.Parse(oldName);
            var addVerName = (number + 1).ToString();

            if (!string.IsNullOrEmpty(fileName))
            {
                var writeInterval = (DateTime.UtcNow - this.FileLastCreateTime).TotalSeconds;
                if (this.LastCreateFileSize > this.Config.MaxBytes || writeInterval > this.Config.SecondInterval)
                {
                    newName = addVerName;
                }
                else
                {
                    foreach (var lvName in FileInfoManager.LevelNames.Values)
                    {
                        if (lvName == this.LevelName)
                            continue;

                        var comparName = GenerateFileName(addVerName, lvName);
                        if (!FileInfoManager.LogFiles.Contains(comparName))
                            continue;

                        return addVerName;
                    }
                    newName = number.ToString();
                }
            }
            return newName;
        }

        private string GenerateFileName(string verName, string levelName)
        {
            var fileName = $"{verName}_{levelName}.log";
            return fileName;
        }

        private static string[] SplitStr = new string[] { "_" };
        private string GetVerName(string fileName)
        {
            var list = fileName.Split(SplitStr, StringSplitOptions.RemoveEmptyEntries);
            return list[0];
        }
    }
}
