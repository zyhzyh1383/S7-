using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Common
{
    /// <summary>
    /// Author：钟永辉
    /// Create Date：2022-8-3 17:21:41
    /// Description：文件操作类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="filePath">文件所在的全路径</param>
        /// <returns></returns>
        public static bool Exist(string filePath)
        {
            bool result;
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException(nameof(filePath));
                result = File.Exists(filePath);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="filePath">文件所在的全路径</param>
        /// <returns></returns>
        public static bool Create(string filePath)
        {
            bool result;
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException(nameof(filePath));
                if (!FileHelper.Exist(filePath))
                {
                    File.Create(filePath);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件所在的全路径</param>
        /// <returns></returns>
        public static bool Delete(string filePath)
        {
            bool result;
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException(nameof(filePath));
                if (FileHelper.Exist(filePath))
                {
                    File.Delete(filePath);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 文件拷贝
        /// </summary>
        /// <param name="sourceFileName">源文件路径</param>
        /// <param name="destFileName">目标文件路径</param>
        /// <returns></returns>
        public static bool Copy(string sourceFileName, string destFileName)
        {
            bool result;
            try
            {
                if (string.IsNullOrEmpty(sourceFileName))
                    throw new ArgumentNullException(nameof(sourceFileName));
                if (FileHelper.Exist(sourceFileName))
                {
                    File.Copy(sourceFileName, destFileName);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 文件剪切
        /// </summary>
        /// <param name="sourceFileName">源文件路径</param>
        /// <param name="destFileName">目标文件路径</param>
        /// <returns></returns>
        public static bool Move(string sourceFileName, string destFileName)
        {
            bool result;
            try
            {
                if (string.IsNullOrEmpty(sourceFileName))
                    throw new ArgumentNullException(nameof(sourceFileName));
                if (FileHelper.Exist(sourceFileName))
                {
                    File.Move(sourceFileName, destFileName);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 获得文件名
        /// </summary>
        /// <param name="filePath">文件所在的全路径</param>
        /// 抽烟1_ (1).jpeg
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetFileName(string filePath)
        {
            string fileName;
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException("filePath");
                fileName = Path.GetFileName(filePath);
            }
            catch
            {
                fileName = null;
            }
            return fileName;
        }

        /// <summary>
        /// 获得文件名但是不包含扩展名
        /// </summary>
        /// 抽烟1_ (1)
        /// <param name="filePath">文件所在的全路径</param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtension(string filePath)
        {
            string fileName;
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException("filePath");
                fileName = Path.GetFileNameWithoutExtension(filePath);
            }
            catch
            {
                fileName = null;
            }
            return fileName;
        }

        /// <summary>
        /// 获得文件的扩展名
        /// </summary>
        /// .jpeg
        /// <param name="filePath">文件所在的全路径</param>
        /// <returns></returns>
        public static string GetExtension(string filePath)
        {
            string extentsionName;
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException("filePath");
                extentsionName = Path.GetExtension(filePath);
            }
            catch
            {
                extentsionName = null;
            }
            return extentsionName;
        }

        /// <summary>
        /// 获得文件所在文件夹路径名称
        /// </summary>
        /// C:\Users\GHZN\Desktop\百度图片\自己测试的图片\测试抽烟
        /// <param name="filePath">文件所在的全路径</param>
        /// <returns></returns>
        public static string GetDirectoryName(string filePath)
        {
            string directoryName;
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException("filePath");
                directoryName = Path.GetDirectoryName(filePath);
            }
            catch
            {
                directoryName = null;
            }
            return directoryName;
        }

        /// <summary>
        /// 获得文件所在的全路径
        /// </summary>
        /// C:\Users\GHZN\Desktop\百度图片\自己测试的图片\测试抽烟\抽烟1_ (1).jpeg
        /// <param name="filePath">文件所在的全路径</param>
        /// <returns></returns>
        public static string GetFullPath(string filePath)
        {
            string fullPath;
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException("filePath");
                fullPath = Path.GetFullPath(filePath);
            }
            catch
            {
                fullPath = null;
            }
            return fullPath;
        }

        /// <summary>
        /// 连接两个字符串作为路径
        /// </summary>
        /// c:\a\b.txt
        /// <param name="path1">路径1</param>
        /// <param name="path2">路径2</param>
        /// <returns></returns>
        public static string Combine(string path1, string path2)
        {
            string tempPath;
            try
            {
                if (string.IsNullOrEmpty(path1))
                    throw new ArgumentNullException("path1");
                if (string.IsNullOrEmpty(path2))
                    throw new ArgumentNullException("path2");
                tempPath = Path.Combine(path1, path2);
            }
            catch
            {
                tempPath = null;
            }
            return tempPath;
        }

        /// <summary>
        /// 读取文件字符串
        /// </summary>
        /// <param name="filePath">文件所在的全路径</param>
        /// <returns></returns>
        public static string ReadAllText(string filePath)
        {
            return ReadAllText(filePath, Encoding.UTF8);
        }

        /// <summary>
        /// 读取文件内容字符串
        /// </summary>
        /// <param name="filePath">文件所在的全路径</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string ReadAllText(string filePath, Encoding encoding)
        {
            string content;
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException("filePath");
                content = File.ReadAllText(filePath, encoding);
            }
            catch
            {
                content = null;
            }
            return content;
        }

        /// <summary>
        /// 读取文件内容获取byte[]
        /// </summary>
        /// <param name="filePath">文件所在的全路径</param>
        /// <returns></returns>
        public static byte[] ReadAllBytes(string filePath)
        {
            byte[] buffer;
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException("filePath");
                buffer = File.ReadAllBytes(filePath);
            }
            catch
            {
                buffer = null;
            }
            return buffer;
        }

        /// <summary>
        /// 以字符串的形式写入文件
        /// </summary>
        /// <param name="filePath">文件所在的全路径</param>
        /// <param name="content">字节</param>
        /// <returns></returns>
        public static bool WriteAllText(string filePath, string content)
        {
            return WriteAllText(filePath, content, Encoding.UTF8);
        }

        /// <summary>
        /// 以字符串的形式写入文件
        /// </summary>
        /// <param name="filePath">文件所在的全路径</param>
        /// <param name="buffer">字节</param>
        /// <returns></returns>
        public static bool WriteAllText(string filePath, string content, Encoding encoding)
        {
            bool result;
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException("filePath");
                if (string.IsNullOrEmpty(content))
                    result = false;
                File.WriteAllText(filePath, content, encoding);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 以字符串的形式追加到文件内容
        /// </summary>
        /// <param name="filePath">文件所在的全路径</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static bool AppendAllText(string filePath, string content)
        {
            return AppendAllText(filePath, content, Encoding.UTF8);
        }

        /// <summary>
        /// 以字符串的形式追加到文件内容
        /// </summary>
        /// <param name="filePath">文件所在的全路径</param>
        /// <param name="content">内容</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static bool AppendAllText(string filePath, string content, Encoding encoding)
        {
            bool result;
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException("filePath");
                if (string.IsNullOrEmpty(content))
                    result = false;
                File.AppendAllText(filePath, content, encoding);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 以字节的形式写入文件
        /// </summary>
        /// <param name="filePath">文件所在的全路径</param>
        /// <param name="buffer">字节</param>
        /// <returns></returns>
        public static bool WriteAllBytes(string filePath, byte[] buffer)
        {
            bool result;
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException("filePath");
                if (buffer == null)
                    result = false;
                File.WriteAllBytes(filePath, buffer);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 获得byte[]字节数组
        /// </summary>
        /// <param name="str">待读取的字符串</param>
        /// <returns></returns>
        public static byte[] GetBytes(string str)
        {
            return GetBytes(str, Encoding.UTF8);
        }

        /// <summary>
        /// 获得byte[]字节数组
        /// </summary>
        /// <param name="str">待读取的字符串</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static byte[] GetBytes(string str, Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes(str);
            return buffer;
        }
    }
}
