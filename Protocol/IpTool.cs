//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：IpTool.cs
//
// 文件功能描述：
//
// 获取公网 IP 工具
//
// 创建标识：taixihuase 20160211
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//-----------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Protocol
{
    /// <summary>
    /// 类型：类
    /// 名称：IpTool
    /// 作者：taixihuase
    /// 作用：IP 工具类
    /// 编写日期：2016/2/11 
    /// </summary>
    public static class IpTool
    {
        private static readonly object ObjToLock = new object();

        /// <summary>
        /// 类型：方法
        /// 名称：CheckIp
        /// 作者：taixihuase
        /// 作用：验证 IP 字符串合法性
        /// 编写日期：2016/2/21
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static bool CheckIp(string src)
        {
            if (Regex.IsMatch(src, @"^(?!192\.168\.)((([1-9]?[0-9])|1[0-9]{2}|2([0-4][0-9]|5[0-5]))\.){3}(([1-9]?[0-9])|1[0-9]{2}|2([0-4][0-9]|5[0-5]))$"))
                return true;
            return false;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetPublicIpAddress
        /// 作者：taixihuase
        /// 作用：获取公网 IP 地址
        /// 编写日期：2016/2/11 
        /// </summary>
        /// <returns></returns>
        public static string GetPublicIpAddress()
        {
            lock (ObjToLock)
            {
                string tempip = "0.0.0.0";
                try
                {
                    WebRequest wr = WebRequest.Create("http://www.ipip.net/");
                    Stream s = wr.GetResponse().GetResponseStream();
                    if (s != null)
                    {
                        StreamReader sr = new StreamReader(s, Encoding.UTF8);
                        string all = sr.ReadToEnd();
                        int start = all.IndexOf("您当前的IP：", StringComparison.Ordinal) + 7;
                        int end = all.IndexOf("<", start, StringComparison.Ordinal);
                        tempip = all.Substring(start, end - start);
                        sr.Close();
                        s.Close();
                    }
                }
                catch
                {
                    tempip =  GetPublicIpAddress();
                }
                if (!CheckIp(tempip))
                {
                    tempip = GetPublicIpAddress();
                }
                return tempip;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：ConvertIpToInt32
        /// 作者：taixihuase
        /// 作用：将 IP 地址转换为 32 位整型数值
        /// 编写日期：2016/2/11
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static int ConvertIpToInt32(string ip)
        {
            try
            {
                byte[] bytes = IPAddress.Parse(ip).GetAddressBytes();
                return BitConverter.ToInt32(bytes, 0);
            }
            catch
            {
                throw new Exception(ip);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：ConvertIpToString
        /// 作者：taixihuase
        /// 作用：将 IP 数值转换为点分十进制字符串
        /// 编写日期：2016/2/11
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string ConvertIpToString(int ip)
        {
            return new IPAddress(BitConverter.GetBytes(ip)).ToString();
        }
    }
}
