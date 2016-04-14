//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：NetTool.cs
//
// 文件功能描述：
//
// 检测网络工具
//
// 创建标识：taixihuase 20160414
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
using System.Runtime.InteropServices;
// ReSharper disable UnusedMember.Local
// ReSharper disable InconsistentNaming

namespace Protocol
{
    /// <summary>
    /// 类型：类
    /// 名称：NetTool
    /// 作者：taixihuase
    /// 作用：网络工具类
    /// 编写日期：2016/4/14 
    /// </summary>
    public static class NetTool
    {
        [Flags]
        private enum ConnectionState
        {
            INTERNET_CONNECTION_MODEM = 0x1,
            INTERNET_CONNECTION_LAN = 0x2,
            INTERNET_CONNECTION_PROXY = 0x4,
            INTERNET_RAS_INSTALLED = 0x10,
            INTERNET_CONNECTION_OFFLINE = 0x20,
            INTERNET_CONNECTION_CONFIGURED = 0x40
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        static extern bool InternetGetConnectedState(ref ConnectionState lpdwFlags, int dwReserved);

        /// <summary>
        /// 类型：方法
        /// 名称：IsConnectedToInternet
        /// 作者：taixihuase
        /// 作用：检测网络连接情况
        /// 编写日期：2016/4/14 
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectedToInternet()
        {
            ConnectionState description = 0;
            bool conn = InternetGetConnectedState(ref description, 0);
            return conn;
        }
    }
}
