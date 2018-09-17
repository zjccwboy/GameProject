using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace H6Game.Base
{
    public abstract class ALoggerFileWriter : ILoggerWriter
    {
        private LoggerConfigEntity Config { get; }
        private StreamWriter logOutput;
        private LoggerFileWriterFatory WriterFatory { get; }
        private string CustomName { get; set; }
        public abstract LogLevel LogLevel { get;}
        private StringBuilder MessageBuilder { get; } = new StringBuilder();

        public ALoggerFileWriter(LoggerFileWriterFatory writerFatory)
        {
            this.Config = Game.Scene.GetComponent<LoggerConfigComponent>().Config;
            this.WriterFatory = writerFatory;

            if (!CanWrite(this.LogLevel))
                return;

            var fileName = GetFileName();
            logOutput = new StreamWriter(File.Open(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite), Encoding.UTF8);
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

        public async Task WriteMessage(LoggerEntity entity)
        {
            if (entity.FLogLevel != this.LogLevel)
                return;

            if (!CanWrite(this.LogLevel))
                return;

            if (!CanWrite())
            {
                FileHelper.UpdateFileInfo(this.Config.LoggerPath);
                WriterFatory.ReCreateWriters();
                await WriterFatory.WriteMessage(entity);
                return;
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
            await this.logOutput.FlushAsync();
        }

        private string GetFileName()
        {
            if (!Directory.Exists(this.Config.LoggerPath))
            {
                Directory.CreateDirectory(this.Config.LoggerPath);
            }

            var path = this.Config.LoggerPath.EndsWith("\\") ? this.Config.LoggerPath : this.Config.LoggerPath + "\\";

            var isNeedNewFile = !CanWrite();
            if (isNeedNewFile)
            {
                SetFileExtensionName();
                var levelName = FileHelper.LevelNames[this.LogLevel];
                var fileName = $"{levelName} {CustomName}";
                return $"{path}{fileName}";
            }

            return $"{path} {FileHelper.LastCreateFileNames[this.LogLevel]}";// FileHelper.LastCreateFileNames[this.LogLevel];
        }

        private void SetFileExtensionName()
        {
            CustomName = $"{DateTime.Now.Ticks}.log";
        }

        private bool CanWrite()
        {
            if (!FileHelper.LastCreateFileNames.ContainsKey(this.LogLevel))
                return false;

            if (!FileHelper.LastCreateFileSize.TryGetValue(this.LogLevel, out long size))
                return false;

            if (size >= this.Config.MaxBytes)
                return false;

            if (!FileHelper.LastCreateFileTime.TryGetValue(this.LogLevel, out DateTime dateTime))
                return false;

            var span = DateTime.UtcNow - dateTime;
            if (span.TotalSeconds >= this.Config.SecondInterval)
                return false;

            return true;
        }

        public void Dispose()
        {
            logOutput.Flush();
            logOutput.Close();
            logOutput.Dispose();
        }
    }
}
