//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：ProxyServerApplication.cs
//
// 文件功能描述：
//
// 网关代理服务器应用程序
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
using System.Collections.Generic;
using System.IO;
using ExitGames.Concurrency.Fibers;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;
using Photon.SocketServer;
using Protocol;
using S2SProtocol.Common;
using LogManager = ExitGames.Logging.LogManager;

namespace ProxyServer
{
    /// <summary>
    /// 类型：类
    /// 名称：ProxyServerApplication
    /// 作者：taixihuase
    /// 作用：网关代理服务器应用程序
    /// 编写日期：2016/1/31
    /// </summary>
    public class ProxyServerApplication : ApplicationBase
    {

        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public PeerToMasterServer PeerToMaster { get; protected set; }

        public PeerToLobbyServer PeerToLobby { get; set; }

        public List<SocketGuid> ConnectingLogicServer { get; protected set; }

        public ServerInfo Info { get; set; }

        public ServerLoad Load { get; protected set; }

        public ServerType TypeOfConnectingServer { get; set; }

        public ExtendedPoolFiber Fiber { get; set; }

        public Queue<ServerInfo> ConnectQueue { get; set; }

        #region protected override methods

        /// <summary>
        /// 类型：方法
        /// 名称：CreatePeer
        /// 作者：taixihuase
        /// 作用：每当一个客户端连接时，生成一个新的 Peer 并回传给 Server
        /// 编写日期：2016/1/31
        /// </summary>
        /// <param name="initRequest"></param>
        /// <returns></returns>
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            if (initRequest.LocalPort == ServerSettings.Default.PortForClientPeer)
            {
                // for Client connections
                return new PeerToClient(initRequest, this);
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
            Log.Debug($"[{DateTime.Now}]{Info.ServerType} 正在运行 [Server Name]{Info.ServerName}");
            Fiber.Enqueue(ConnectToServer);
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
            Log.Debug($"[{DateTime.Now}]{Info.ServerType} 正在停止 [Server Name]{Info.ServerName}");
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
            ConnectingLogicServer = new List<SocketGuid>();
            Info = new ServerInfo
            {
                ServerType = (ServerType) Enum.Parse(typeof (ServerType), ServerSettings.Default.ServerType),
                ServerName = ServerSettings.Default.ServerName
            };
            Load = new ServerLoad();
            Fiber = new ExtendedPoolFiber();
            Fiber.Start();
            TypeOfConnectingServer = ServerType.UndefinedServer;
            ConnectQueue = new Queue<ServerInfo>();

            ServerInfo server = new ServerInfo
            {
                ServerType = ServerType.MasterServer,
                ServerName = S2SProtocol.Common.ServerSettings.Default.NameOfMasterServer,
                ListeningPort = S2SProtocol.Common.ServerSettings.Default.PortOfMasterServer,
                Socket =
                    new SocketGuid(S2SProtocol.Common.ServerSettings.Default.IpOfMasterServer,
                        S2SProtocol.Common.ServerSettings.Default.PortOfMasterServer)
            };

            ConnectQueue.Enqueue(server);
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

        /// <summary>
        /// 类型：方法
        /// 名称：ConnectToServer
        /// 作者：taixihuase
        /// 作用：连接服务器
        /// 编写日期：2016/1/31
        /// </summary>
        public void ConnectToServer()
        {
            if (Running == false)
                return;

            lock (this)
            {
                if (ConnectQueue.Count == 0)
                    return;

                var server = ConnectQueue.Dequeue();
                switch (TypeOfConnectingServer = server.ServerType)
                {
                    case ServerType.UndefinedServer:
                        break;

                    case ServerType.MasterServer:
                        PeerToMaster?.Dispose();
                        PeerToMaster = new PeerToMasterServer(this);
                        PeerToMaster.ConnectToMaster();
                        break;

                    case ServerType.LobbyServer:
                        if (PeerToLobby == null)
                        {
                            PeerToLobby = new PeerToLobbyServer(server, this);
                            PeerToLobby.ConnectToLobby();
                        }
                        else if (!Equals(PeerToLobby.RemoteSocket, server.Socket))
                        {
                            if (PeerToLobby.Connected)
                            {
                                PeerToLobby.Dispose();
                            }
                            PeerToLobby = new PeerToLobbyServer(server, this);
                            PeerToLobby.ConnectToLobby();
                        }
                        break;

                    case ServerType.LogicServer:
                        if (!LogicServerCollection.Instance.GuidToLogicServer.ContainsKey(server.Socket))
                        {
                            if (!ConnectingLogicServer.Exists(i => Equals(i, server.Socket)))
                            {
                                ConnectingLogicServer.Add(server.Socket);
                                PeerToLogicServer logic = new PeerToLogicServer(server, this);
                                logic.ConnectToLogic();
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：ConnectToServer
        /// 作者：taixihuase
        /// 作用：连接服务器
        /// 编写日期：2016/2/21
        /// </summary>
        /// <param name="server"></param>
        public void ConnectToServer(ServerInfo server)
        {
            lock (this)
            {
                ConnectQueue.Enqueue(server);
                Fiber.Enqueue(ConnectToServer);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：ReconnectToServer
        /// 作者：taixihuase
        /// 作用：重新连接服务器
        /// 编写日期：2016/2/3
        /// </summary>
        /// <param name="server"></param>
        public void ReconnectToServer(ServerInfo server)
        {
            lock (this)
            {
                Fiber.Schedule(() => ConnectToServer(server), 5000);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SetDefaultLogicServers
        /// 作者：taixihuase
        /// 作用：设置默认逻辑服务器列表
        /// 编写日期：2016/2/13
        /// </summary>
        /// <param name="servers"></param>
        public void SetDefaultLogicServers(List<SocketGuid> servers)
        {
            LogicServerCollection.Instance.UpdateDefaultLogicServers(servers);
        }
    }
}
