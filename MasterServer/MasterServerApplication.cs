//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：MasterServerApplication.cs
//
// 文件功能描述：
//
// 主服务器应用程序
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
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ExitGames.Concurrency.Fibers;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;
using Photon.SocketServer;
using Protocol;
using S2SProtocol.Common;
using LogManager = ExitGames.Logging.LogManager;
// ReSharper disable UnusedVariable

namespace MasterServer
{
    /// <summary>
    /// 类型：类
    /// 名称：MasterServerApplication
    /// 作者：taixihuase
    /// 作用：主服务器应用程序
    /// 编写日期：2016/1/25
    /// </summary>
    public class MasterServerApplication : ApplicationBase
    {
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public ServerInfo Info { get; set; }

        public ExtendedPoolFiber Fiber { get; set; }

        #region protected override methods

        /// <summary>
        /// 类型：方法
        /// 名称：CreatePeer
        /// 作者：taixihuase
        /// 作用：每当一个子服务器连接时，生成一个新的 Peer 并回传给 Server
        /// 编写日期：2016/1/25
        /// </summary>
        /// <param name="initRequest"></param>
        /// <returns></returns>
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            if (initRequest.LocalPort == ServerSettings.Default.PortForSubServerPeer)
            {
                // for S2S connections
                return new PeerToSubServer(initRequest, this);
            }

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
            Log.Debug($"[{ServerTime.Instance.Time}]{Info.ServerType} 正在运行 [Server Name]{Info.ServerName}");
            Fiber.ScheduleOnInterval(SendDefaultLogicToProxy, 30000, 60000);
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
            Log.Debug($"[{ServerTime.Instance.Time}]{Info.ServerType} 正在停止 [Server Name]{Info.ServerName}");
        }

        #endregion

        #region private methods

        /// <summary>
        /// 类型：方法
        /// 名称：Initialize
        /// 作者：taixihuase
        /// 作用：进行资源初始化
        /// 编写日期：2016/1/25
        /// </summary>
        private void Initialize()
        {
            var time = ServerTime.Instance.Time;
            Thread.Sleep(1000);
            Info = new ServerInfo
            {
                ServerType = (ServerType) Enum.Parse(typeof (ServerType), ServerSettings.Default.ServerType),
                ServerName = ServerSettings.Default.ServerName
            };
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

        }

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
        /// 名称：SendDefaultLogicToProxy
        /// 作者：taixihuase
        /// 作用：向网关代理服务器发送默认逻辑服务器配置
        /// 编写日期：2016/3/7
        /// </summary>
        private void SendDefaultLogicToProxy()
        {
            List<PeerToSubServer> proxys;
            List<List<SocketGuid>> logics;
            if (SubServerCollection.Instance.SetDefaultLogicForProxy(out proxys, out logics))
            {
                for (int i = 0; i < proxys.Count; ++i)
                {
                    proxys[i].SendEventToSub(S2SEventCode.SetDefaultLogicServer, S2SParaCode.SocketList, logics[i]);

                    Log.Debug("\n");
                    Log.Debug("-------------------------------------------------------------------------------");
                    Log.Debug(
                        $"[{ServerTime.Instance.Time}]{proxys[i].Type} 更新默认逻辑服务器列表 [Socket]{proxys[i].Guid.GetSocketToString()} [Server Name]{proxys[i].Name} [Cpu Load]{proxys[i].CpuLoad}");
                    foreach (var socketGuid in logics[i])
                        Log.Debug($"[Socket]{socketGuid.GetSocketToString()}");
                    Log.Debug("-------------------------------------------------------------------------------\n");
                }
            }
        }

        #endregion
    }
}
