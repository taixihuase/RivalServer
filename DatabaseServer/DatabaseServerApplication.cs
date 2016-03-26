//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：DatabaseServerApplication.cs
//
// 文件功能描述：
//
// 数据库服务器应用程序
//
// 创建标识：taixihuase 20160125
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
using ExitGames.Concurrency.Fibers;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;
using Photon.SocketServer;
using S2SProtocol.Common;
using LogManager = ExitGames.Logging.LogManager;

namespace DatabaseServer
{
    /// <summary>
    /// 类型：类
    /// 名称：DatabaseServerApplication
    /// 作者：taixihuase
    /// 作用：数据库服务器应用程序
    /// 编写日期：2016/1/25
    /// </summary>
    public class DatabaseServerApplication : ApplicationBase
    {
        
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public PeerToMasterServer PeerToMaster;

        public ServerInfo Info { get; set; }

        public ServerLoad Load { get; protected set; }

        public ExtendedPoolFiber Fiber { get; set; }

        public bool IsConnecting { get; protected set; }

        #region protected override methods

        /// <summary>
        /// 类型：方法
        /// 名称：CreatePeer
        /// 作者：taixihuase
        /// 作用：每当一个客户端连接时，生成一个新的 Peer 并回传给 Server，该方法禁用
        /// 编写日期：2016/1/25
        /// </summary>
        /// <param name="initRequest"></param>
        /// <returns></returns>
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return null;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Setup
        /// 作者：taixihuase
        /// 作用：启动并初始化服务端
        /// 编写日期：2016/1/25
        /// </summary>
        protected override void Setup()
        {
            CreateLogs();
            Initialize();
            Log.Debug($"[{DateTime.Now}]{Info.ServerType} 正在运行 [Server Name]{Info.ServerName}");
            Fiber.Schedule(PeerToMaster.ConnectToMaster, 0);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TearDown
        /// 作者：taixihuase
        /// 作用：关闭服务端并释放资源
        /// 编写日期：2016/1/25
        /// </summary>
        protected override void TearDown()
        {
            Release();
            Log.Debug($"[{DateTime.Now}]{Info.ServerType} 正在停止 [Server Name]{Info.ServerName}");
        }

        #endregion

        #region private methods

        /// <summary>
        /// 类型：方法
        /// 名称：CreateLogs
        /// 作者：taixihuase
        /// 作用：创建服务器日志
        /// 编写日期：2016/1/25
        /// </summary>
        private void CreateLogs()
        {
            GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(ApplicationRootPath, "log");

            string path = Path.Combine(BinaryPath, "log4net.config");
            var file = new FileInfo(path);
            if (file.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(file);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Initialize
        /// 作者：taixihuase
        /// 作用：进行资源初始化
        /// 编写日期：2016/1/25
        /// </summary>
        private void Initialize()
        {
            PeerToMaster = new PeerToMasterServer(this);
            Info = new ServerInfo
            {
                ServerType = (ServerType) Enum.Parse(typeof (ServerType), ServerSettings.Default.ServerType),
                ServerName = ServerSettings.Default.ServerName
            };
            IsConnecting = false;
            Load = new ServerLoad();
            Fiber = new ExtendedPoolFiber();
            Fiber.Start();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Release
        /// 作者：taixihuase
        /// 作用：释放资源
        /// 编写日期：2016/1/30
        /// </summary>
        private void Release()
        {
            Fiber.Stop();
        }
       
        #endregion  
    }
}
