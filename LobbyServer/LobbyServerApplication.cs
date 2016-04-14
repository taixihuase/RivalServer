//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：LobbyServerApplication.cs
//
// 文件功能描述：
//
// 大厅服务器应用程序
//
// 创建标识：taixihuase 20160131
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
using System.Threading;
using ExitGames.Concurrency.Fibers;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;
using Photon.SocketServer;
using S2SProtocol.Common;
using LogManager = ExitGames.Logging.LogManager;
// ReSharper disable UnusedVariable

namespace LobbyServer
{
    /// <summary>
    /// 类型：类
    /// 名称：LobbyServerApplication
    /// 作者：taixihuase
    /// 作用：大厅服务器应用程序
    /// 编写日期：2016/1/31
    /// </summary>
    public class LobbyServerApplication : ApplicationBase
    {

        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public PeerToMasterServer PeerToMaster;

        public ServerInfo Info { get; set; }

        public ServerLoad Load { get; protected set; }

        public ExtendedPoolFiber Fiber { get; set; }

        #region protected override methods

        /// <summary>
        /// 类型：方法
        /// 名称：CreatePeer
        /// 作者：taixihuase
        /// 作用：每当一个网关代理服务器连接时，生成一个新的 Peer 并回传给 Server
        /// 编写日期：2016/1/31
        /// </summary>
        /// <param name="initRequest"></param>
        /// <returns></returns>
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            if (initRequest.LocalPort == ServerSettings.Default.PortForProxyServerPeer)
            {
                // for S2S connections
                return new PeerToProxyServer(initRequest, this);
            }
            return null;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Setup
        /// 作者：taixihuase
        /// 作用：启动并初始化服务端
        /// 编写日期：2016/1/31
        /// </summary>
        protected override void Setup()
        {
            CreateLogs();
            Initialize();
            Log.Debug($"[{ServerTime.Instance.Time}]{Info.ServerType} 正在运行 [Server Name]{Info.ServerName}");
            Fiber.Schedule(PeerToMaster.ConnectToMaster, 0);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TearDown
        /// 作者：taixihuase
        /// 作用：关闭服务端并释放资源
        /// 编写日期：2016/1/31
        /// </summary>
        protected override void TearDown()
        {
            Release();
            Log.Debug($"[{ServerTime.Instance.Time}]{Info.ServerType} 正在停止 [Server Name]{Info.ServerName}");
        } 

        #endregion

        #region private methods

        /// <summary>
        /// 类型：方法
        /// 名称：CreateLogs
        /// 作者：taixihuase
        /// 作用：创建服务器日志
        /// 编写日期：2016/1/31
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
        /// 编写日期：2016/1/31
        /// </summary>
        private void Initialize()
        {
            var time = ServerTime.Instance.Time;
            Thread.Sleep(1000);
            PeerToMaster = new PeerToMasterServer(this);
            Info = new ServerInfo
            {
                ServerType = (ServerType) Enum.Parse(typeof (ServerType), ServerSettings.Default.ServerType),
                ServerName = ServerSettings.Default.ServerName
            };
            Load = new ServerLoad();
            Fiber = new ExtendedPoolFiber();
            Fiber.Start();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Release
        /// 作者：taixihuase
        /// 作用：释放资源
        /// 编写日期：2016/1/31
        /// </summary>
        private void Release()
        {
            Fiber.Stop();
        }

        #endregion
    }
}
