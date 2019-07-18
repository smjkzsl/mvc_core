using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace Workman.Util
{
    /// <summary>
    /// 版 本  Workman-Lib V1.0.0CoreMvc
    /// Copyright (c) 2013-2018 水墨剑客
    /// 创建人：水墨剑客
    /// 日 期：2019.04.07
    /// 文件下载类
    /// </summary>
    public class FileDownHelper
    {

        public FileDownHelper()
        { }
        /// <summary>
        /// 参数为虚拟路径
        /// </summary>
        public static string FileNameExtension(string FileName)
        {
            return Path.GetExtension(MapPathFile(FileName));
        }
        /// <summary>
        /// 获取物理地址
        /// </summary>
        public static string MapPathFile(string FileName)
        {
            return HttpContext.Current.MapPath(FileName);
        }
        /// <summary>
        /// 验证文件是否存在
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static bool FileExists(string FileName)
        {
            string destFileName = FileName;
            if (File.Exists(destFileName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 普通下载
        /// </summary>
        /// <param name="FileName">文件虚拟路径</param>
        ///  /// <param name="name">返回客户端名称</param>
        public static void DownLoadold(string FileName, string name)
        {
            string destFileName = FileName;
            if (File.Exists(destFileName))
            {
                FileInfo fi = new FileInfo(destFileName);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Headers.Clear();
                //HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.Headers.Add("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8));
                HttpContext.Current.Response.Headers.Add("Content-Length", fi.Length.ToString());
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.WriteAsync(destFileName);
               // HttpContext.Current.Response.Flush();
               // HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// 分块下载
        /// </summary>
        /// <param name="FileName">文件虚拟路径</param>
        public static void DownLoad(string FileName)
        {
            
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="filename">文件名称</param>
        public static void DownLoadString(string content, string filename) {
            MemoryStream stream = new MemoryStream();
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(content);
            stream.Write(byteArray,0, byteArray.Length);
            stream.Position = 0;
            DownLoad(stream, filename);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="filename">文件名称</param>
        public static void DownLoadBase64(string content, string filename)
        {
            byte[] arr = Convert.FromBase64String(content);
            MemoryStream ms = new MemoryStream(arr);
            DownLoad(ms, filename);
        }

        /// <summary>
        /// 分块下载
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="filename">文件名</param>
        public static void DownLoad(MemoryStream stream, string filename)
        {
            
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath">物理地址</param>
        public static void DownLoadnew(string filePath)
        {
             
        }
        /// <summary>
        ///  输出硬盘文件，提供下载 支持大文件、续传、速度限制、资源占用小
        /// </summary>
        /// <param name="_Request">Page.Request对象</param>
        /// <param name="_Response">Page.Response对象</param>
        /// <param name="_fileName">下载文件名</param>
        /// <param name="_fullPath">带文件名下载路径</param>
        /// <param name="_speed">每秒允许下载的字节数</param>
        /// <returns>返回是否成功</returns>
        //---------------------------------------------------------------------
        //调用：
        // string FullPath=Server.MapPath("count.txt");
        // ResponseFile(this.Request,this.Response,"count.txt",FullPath,100);
        //---------------------------------------------------------------------
        public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
        {
            //TODO
            return true;
        }
    }
}
