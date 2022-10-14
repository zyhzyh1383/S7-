using System;
using System.IO;
namespace Common
{
    /// <summary>
    /// Author：钟永辉
    /// Create Date：2022-8-3 17:21:41
    /// Description：自定义日志类
    /// 
    public class LogFileHelper
    {

        public enum LogLevel
        {
            Info = 1,
            Error = 2,
            DeBug = 3,
            Warning = 4
        }

        private static readonly object LogLock = new object();

        /// <summary>
        /// 日志默认存放路径
        /// </summary>
        private static readonly string LogPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\LogFiles\";

        /// <summary>
        /// 只记录信息Info
        /// </summary>
        /// <param name="content">内容</param>
        public static void Info(string content)
        {
            Info(null, content);
        }

        /// <summary>
        /// 路径加信息Info
        /// </summary>
        /// <param name="filePath">相对路径</param>
        /// <param name="content">内容</param>
        public static void Info(string filePath, string content)
        {
            Info(filePath, null, content);
        }

        /// <summary>
        /// 写入日志Info
        /// </summary>
        /// <param name="filePath">相对路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="content">内容</param>
        public static void Info(string filePath, string fileName, string content)
        {
            WirteLog(filePath, fileName, content, LogLevel.Info);
        }

        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="ex"></param>
        public static void Error(Exception ex)
        {
            Error(ex.Message.ToString());
        }

        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="content"></param>
        public static void Error(string content)
        {
            Error(null, content);
        }

        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        public static void Error(string filePath, string content)
        {
            Error(filePath, null, content);
        }

        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public static void Error(string filePath, string fileName, string content)
        {
            WirteLog(filePath, fileName, content, LogLevel.Error);
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="filePath">文件目录路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="content">日志内容</param>
        /// <param name="logLevel">日志级别</param>
        private static void WirteLog(string filePath, string fileName, string content, LogLevel logLevel)
        {
            lock (LogLock)
            {
                try
                {
                    if (string.IsNullOrEmpty(filePath))
                    {
                        switch (logLevel)
                        {
                            case LogLevel.Info:
                                filePath = "Info";
                                break;
                            case LogLevel.Error:
                                filePath = "Error";
                                break;
                            case LogLevel.DeBug:
                                filePath = "DeBug";
                                break;
                            case LogLevel.Warning:
                                filePath = "Warning";
                                break;
                            default:
                                break;
                        }
                    }
                    if (string.IsNullOrEmpty(content))
                        return;
                    filePath = LogPath + filePath;
                    if (!DirectoryHelper.Exists(filePath))
                        DirectoryHelper.CreateDirectory(filePath);
                    if (string.IsNullOrEmpty(fileName))
                        fileName = DateTime.Now.ToString("yyyyMMdd");
                    var logFileName = filePath + "/" + fileName + ".log";//生成日志文件  
                    var fs = !FileHelper.Exist(logFileName) ? new FileStream(logFileName, FileMode.Create) : new FileStream(logFileName, FileMode.Append);
                    var sw = new StreamWriter(fs);
                    sw.WriteLine($"记录时间:{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    sw.WriteLine($"日志内容:┏︻︻︻︻︻︻︻︻︻︻︻︻︻︻︻︻︻︻︻︻︻︻┓");
                    sw.WriteLine($"{content}");
                    sw.WriteLine($"日志内容:┗︼︼︼︼︼︼︼︼︼︼︼︼︼︼︼︼︼︼︼︼︼︼┛");
                    sw.Flush();
                    sw.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
