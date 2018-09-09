using System;

namespace H6Game.Base
{
    /// <summary>
    /// 系统日志接口
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 系统崩溃错误日志
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="args">设置格式的对象的数组</param>
        void Fatal(string message, params object[] args);

        /// <summary>
        /// 系统崩溃错误日志
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="exception">异常对象</param>
        /// <param name="args">设置格式的对象的数组</param>
        void Fatal(string message, Exception exception, params object[] args);

        /// <summary>
        /// 系统消息日志
        /// </summary>
        /// <param name="message">要记录的信息</param>
        /// <param name="args">设置格式的对象的数组</param>
        void Info(string message, params object[] args);

        /// <summary>
        /// 系统警告日志
        /// </summary>
        /// <param name="message">警告信息</param>
        /// <param name="args">设置格式的对象的数组</param>
        void Warning(string message, params object[] args);

        /// <summary>
        /// 系统错误日志
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="args">设置格式的对象的数组</param>
        void Error(string message, params object[] args);

        /// <summary>
        /// 系统错误日志
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="exception">异常对象</param>
        /// <param name="args">设置格式的对象的数组</param>
        void Error(string message, Exception exception, params object[] args);

        /// <summary>
        /// 系统错误日志
        /// </summary>
        /// <param name="exception">异常对象</param>
        /// <param name="args">设置格式的对象的数组</param>
        void Error(Exception exception, params object[] args);

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="args">设置格式的对象的数组</param>
        void Debug(string message, params object[] args);
    }

}
