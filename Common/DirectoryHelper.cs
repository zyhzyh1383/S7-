using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Author：钟永辉
    /// Create Date：2022-8-3 17:21:41
    /// Description：目录操作类
    /// </summary>
    public class DirectoryHelper
    {
        /// <summary>
        /// 文件夹是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Exists(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            return Directory.Exists(path);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CreateDirectory(string path)
        {
            bool result = false;
            try
            {
                if (!DirectoryHelper.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Delete(string path)
        {
            bool result = false;
            try
            {
                if (DirectoryHelper.Exists(path))
                {
                    Directory.Delete(path);
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 剪切
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <returns></returns>
        public static bool Move(string sourceDirName, string destDirName)
        {
            bool result = false;
            try
            {
                if (!DirectoryHelper.Exists(destDirName))
                {
                    Directory.Move(sourceDirName, destDirName);
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 获得指定文件夹下所有文件的全路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public static string[] GetFiles(string path, string searchPattern = "*")
        {
            string[] strs;
            try
            {
                if (string.IsNullOrEmpty(path))
                    throw new ArgumentNullException("path");
                strs = Directory.GetFiles(path, searchPattern);
            }
            catch
            {
                strs = null;
            }
            return strs;
        }

        /// <summary>
        /// 获得指定目录下所有文件夹的全路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public static string[] GetDirectories(string path, string searchPattern = "*")
        {
            string[] strs;
            try
            {
                if (string.IsNullOrEmpty(path))
                    throw new ArgumentNullException("path");
                strs = Directory.GetDirectories(path, searchPattern);
            }
            catch
            {
                strs = null;
            }
            return strs;
        }
    }
}
