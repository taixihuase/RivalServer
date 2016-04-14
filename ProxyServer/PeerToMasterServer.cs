//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：PeerToMasterServer.cs
//
// 文件功能描述：
//
// 网关代理服务器与主服务器的连线实例
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

using System.Collections.Generic;
using System.Net;
using ExitGames.Concurrency.Fibers;
using ExitGames.Logging;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using Protocol;
using S2SProtocol.Common;

namespace ProxyServer
{
    /// <summary>
    /// 类型：类
    /// 名称：PeerToMasterServer
    /// 作者：taixihuase
    /// 作用：用于网关代理服务器与主服务器之间的数据传输
    /// 编写日期：2016/1/31
    /// </summary>
    public class PeerToMasterServer : OutboundS2SPeer
    {
        public readonly ProxyServerApplication Server;

        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public ExtendedPoolFiber Fiber { get; set; }

        /// <summary>
        /// 类型：方法
        /// 名称：PeerToMasterServer
        /// 作者：taixihuase
        /// 作用：构造连接进程实例
        /// 编写日期：2016/1/31
        /// </summary>
        /// <param name="server"></param>
        public PeerToMasterServer(ProxyServerApplication server) : base(server)
        {
            Server = server;
            Initialize();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnConnectionEstablished
        /// 作者：taixihuase
        /// 作用：网关代理服务器连接主服务器成功后进行处理
        /// 编写日期：2016/2/16
        /// </summary>
        /// <param name="responseObject"></param>
        protected override void OnConnectionEstablished(object responseObject)
        {
            Log.Debug(
                $"[{ServerTime.Instance.Time}]{Server.Info.ServerType} 成功连接 Master Server [Socket]{S2SProtocol.Common.ServerSettings.Default.IpOfMasterServer}:{S2SProtocol.Common.ServerSettings.Default.PortOfMasterServer}");
            Fiber.Enqueue(GetServerLoad);
            Fiber.Enqueue(RegistToMaster);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnConnectionFailed
        /// 作者：taixihuase
        /// 作用：网关代理服务器连接主服务器失败时进行重连
        /// 编写日期：2016/2/16
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        protected override void OnConnectionFailed(int errorCode, string errorMessage)
        {
            Log.Debug(
                $"[{ServerTime.Instance.Time}]{Server.Info.ServerType} 无法连接 {S2SProtocol.Common.ServerSettings.Default.NameOfMasterServer} [Socket]{S2SProtocol.Common.ServerSettings.Default.IpOfMasterServer}:{S2SProtocol.Common.ServerSettings.Default.PortOfMasterServer}");
            Fiber.Enqueue(() => ReconnectToMaster(S2SProtocol.Common.ServerSettings.Default.ReconnectToMasterInterval));
        }

        #region protected override methods

        /// <summary>
        /// 类型：方法
        /// 名称：OnOperationRequest
        /// 作者：taixihuase
        /// 作用：响应并处理服务端发来的请求，该方法禁用
        /// 编写日期：2016/1/31
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnDisconnect
        /// 作者：taixihuase
        /// 作用：当与主服务器断开连接时进行处理
        /// 编写日期：2016/1/31
        /// </summary>
        /// <param name="reasonCode"></param>
        /// <param name="reasonDetail"></param>
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            Release();
            Fiber.Enqueue(() => ReconnectToMaster(S2SProtocol.Common.ServerSettings.Default.ReconnectToMasterInterval));
            Log.Debug(
                $"[{ServerTime.Instance.Time}]{Server.Info.ServerType} 与 Master Server 连接中断 [Socket]{S2SProtocol.Common.ServerSettings.Default.IpOfMasterServer}:{S2SProtocol.Common.ServerSettings.Default.PortOfMasterServer}");
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnEvent
        /// 作者：taixihuase
        /// 作用：监听主服务器发来的广播并回调触发事件
        /// 编写日期：2016/1/31
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="sendParameters"></param>
        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
            switch ((S2SEventCode) eventData.Code)
            {
                case S2SEventCode.ConnectLobbyServer:
                    OnConnectLobbyServer(eventData);
                    break;
                case S2SEventCode.DisconnectLobbyServer:
                    OnDisconnectLobbyServer(eventData);
                    break;
                case S2SEventCode.ConnectLogicServer:
                    OnConnectLogicServer(eventData);
                    break;
                case S2SEventCode.DisconnectLogicServer:
                    OnDisconnectLogicServer(eventData);
                    break;
                case S2SEventCode.ConnectProxyServer:
                    OnConnectProxyServer(eventData);
                    break;
                case S2SEventCode.DisconnectProxyServer:
                    break;
                case S2SEventCode.SetDefaultLogicServer:
                    OnSetDefaultLogicServer(eventData);
                    break;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnOperationResponse
        /// 作者：taixihuase
        /// 作用：网关代理服务器发送请求后，接收并处理相应的主服务器响应内容
        /// 编写日期：2016/1/31
        /// </summary>
        /// <param name="operationResponse"></param>
        /// <param name="sendParameters"></param>
        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            switch ((S2SOpCode) operationResponse.OperationCode)
            {
                case S2SOpCode.RegistSubServer:
                    OnRegistResponse(operationResponse);
                    break;

                case S2SOpCode.ReportServerLoad:
                    OnReportLoad(operationResponse);
                    break;
            }
        }

        #endregion

        /// <summary>
        /// 类型：方法
        /// 名称：SendOperationRequestToMaster
        /// 作者：taixihuase
        /// 作用：向主服务器发送请求
        /// 编写日期：2016/1/31
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="paraCode"></param>
        /// <param name="obj"></param>
        public void SendOperationRequestToMaster(S2SOpCode opCode, S2SParaCode? paraCode = null, object obj = null)
        {
            var request = new OperationRequest((byte)opCode);
            if (paraCode.HasValue && obj != null)
            {
                request.Parameters = Serialization.IsNeed(obj)
                    ? new Dictionary<byte, object> {{(byte) paraCode, Serialization.Serialize(obj)}}
                    : new Dictionary<byte, object> {{(byte) paraCode, obj}};
            }

            SendOperationRequest(request, new SendParameters());
        }

        /// <summary>
        /// 类型：方法
        /// 名称：ConnectToMaster
        /// 作者：taixihuase
        /// 作用：连接主服务器
        /// 编写日期：2016/2/16
        /// </summary>
        public void ConnectToMaster()
        {
            lock (this)
            {
                string ip = S2SProtocol.Common.ServerSettings.Default.IpOfMasterServer;
                if (ip == IpTool.GetPublicIpAddress())
                    ip = "127.0.0.1";

                ConnectTcp(
                    new IPEndPoint(IPAddress.Parse(ip),
                        S2SProtocol.Common.ServerSettings.Default.PortOfMasterServer), S2SProtocol.Common.ServerSettings.Default.NameOfMasterServer, null);
            }
        }

        #region private methods

        /// <summary>
        /// 类型：方法
        /// 名称：Initialize
        /// 作者：taixihuase
        /// 作用：进行资源初始化
        /// 编写日期：2016/1/31
        /// </summary>
        private void Initialize()
        {
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
            LogicServerCollection.Instance.Clear();
            Server.PeerToLobby?.Dispose();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：ReconnectToMaster
        /// 作者：taixihuase
        /// 作用：重连主服务器
        /// 编写日期：2016/2/2
        /// </summary>
        /// <param name="delay"></param>
        private void ReconnectToMaster(long delay)
        {
            lock (this)
            {
                Fiber.Schedule(ConnectToMaster, delay);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetServerLoad
        /// 作者：taixihuase
        /// 作用：获取服务器负载情况
        /// 编写日期：2016/1/31
        /// </summary>
        private void GetServerLoad()
        {
            Server.Load?.GetServerLoad();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：RegistToMaster
        /// 作者：taixihuase
        /// 作用：向主服务器发送注册请求
        /// 编写日期：2016/1/31
        /// </summary>
        private void RegistToMaster()
        {
            ServerInfo serverInfo = Server.Info;
            serverInfo.ListeningPort = ServerSettings.Default.PortForClientPeer;
            serverInfo.Socket = new SocketGuid(IpTool.GetPublicIpAddress(), (ushort) LocalPort);

            Log.Debug(
                $"[{ServerTime.Instance.Time}]成功获取本服务器信息 [Socket]{Server.Info.GetServerAddress()} [Listening Port]{Server.Info.ListeningPort}");

            SendOperationRequestToMaster(S2SOpCode.RegistSubServer, S2SParaCode.SubServerInfo,
                serverInfo);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnRegistResponse
        /// 作者：taixihuase
        /// 作用：处理注册服务器结果
        /// 编写日期：2016/1/31
        /// </summary>
        /// <param name="response"></param>
        private void OnRegistResponse(OperationResponse response)
        {
            short ret = response.ReturnCode;
            if (ret == (short) S2SRetCode.Failure)
            {
                Disconnect();
                Fiber.Enqueue(() => ReconnectToMaster(S2SProtocol.Common.ServerSettings.Default.ReconnectToMasterInterval));
                return;
            }
            if (ret == (short) S2SRetCode.Success)
            {
                Fiber.Enqueue(ReportLoadToMaster);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：ReportLoadToMaster
        /// 作者：taixihuase
        /// 作用：向主服务器发送负载情况
        /// 编写日期：2016/1/31
        /// </summary>
        private void ReportLoadToMaster()
        {
            if (Server.Load.ServerLoadLevel == ServerLoad.LoadLevel.Undetected)
            {
                Fiber.Schedule(ReportLoadToMaster, 1000);
                return;
            }
            SendOperationRequestToMaster(S2SOpCode.ReportServerLoad, S2SParaCode.SubServerLoad, Server.Load);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnReportLoad
        /// 作者：taixihuase
        /// 作用：处理汇报负载结果
        /// 编写日期：2016/3/8
        /// </summary>
        /// <param name="operationResponse"></param>
        private void OnReportLoad(OperationResponse operationResponse)
        {
            if (operationResponse.ReturnCode == (short) S2SRetCode.Success)
            {
                Server.Load.Clear();
                Fiber.Schedule(GetServerLoad, S2SProtocol.Common.ServerSettings.Default.GetServerLoadInterval);
                Fiber.Schedule(ReportLoadToMaster, S2SProtocol.Common.ServerSettings.Default.ReportServerLoadInterval);
            }
        }
        /// <summary>
        /// 类型：方法
        /// 名称：OnConnectLobbyServer
        /// 作者：taixihuase
        /// 作用：当收到连接大厅服务器事件消息时进行处理
        /// 编写日期：2016/2/21
        /// </summary>
        /// <param name="event"></param>
        private void OnConnectLobbyServer(IEventData @event)
        {
            var info = Serialization.Deserialize<ServerInfo>(@event.Parameters[(byte) S2SParaCode.SubServerInfo]);
            if (Server.PeerToLobby != null)
            {
                if (!Equals(Server.PeerToLobby.ReconnectInfo, info))
                {
                    Server.Fiber.Enqueue(() => Server.ConnectToServer(info));
                }
                return;
            }
            Server.Fiber.Enqueue(() => Server.ConnectToServer(info));
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnConnectLogicServer
        /// 作者：taixihuase
        /// 作用：当收到连接逻辑服务器事件消息时进行处理
        /// 编写日期：2016/2/21
        /// </summary>
        /// <param name="event"></param>
        private void OnConnectLogicServer(IEventData @event)
        {
            var info = Serialization.Deserialize<ServerInfo>(@event.Parameters[(byte)S2SParaCode.SubServerInfo]);
            if (LogicServerCollection.Instance.GuidToLogicServer.ContainsKey(info.Socket))
            {
                var logic = LogicServerCollection.Instance.GuidToLogicServer[info.Socket];
                if (!Equals(logic.ReconnectInfo, info))
                {
                    logic.Dispose();
                    Server.Fiber.Enqueue(() => Server.ConnectToServer(info));
                }
                return;
            }
            Server.Fiber.Enqueue(() => Server.ConnectToServer(info));
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnConnectProxyServer
        /// 作者：taixihuase
        /// 作用：当收到网关代理服务器连接事件消息时进行处理
        /// 编写日期：2016/2/21
        /// </summary>
        /// <param name="event"></param>
        private void OnConnectProxyServer(IEventData @event)
        {
            var infos = Serialization.Deserialize<Dictionary<ServerType, List<ServerInfo>>>(@event.Parameters[(byte)S2SParaCode.SubServerInfo]);
            if (infos.ContainsKey(ServerType.LobbyServer))
            {
                var lobby = infos[ServerType.LobbyServer];
                foreach (var info in lobby)
                {
                    Server.Fiber.Enqueue(() => Server.ConnectToServer(info));
                }
            }
            if (infos.ContainsKey(ServerType.LogicServer))
            {
                var logic = infos[ServerType.LogicServer];
                foreach (var info in logic)
                {
                    Server.Fiber.Enqueue(() => Server.ConnectToServer(info));
                }
            }           
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnDisconnectLobbyServer
        /// 作者：taixihuase
        /// 作用：当收到大厅服务器断线消息时进行处理
        /// 编写日期：2016/2/14
        /// </summary>
        /// <param name="event"></param>
        private void OnDisconnectLobbyServer(IEventData @event)
        {
            SocketGuid socket =
                Serialization.Deserialize<SocketGuid>(@event.Parameters[(byte) S2SParaCode.SubServerSocket]);
            Log.Debug($"[{ServerTime.Instance.Time}]一个 Lobby Server 已断开连接 [Socket]{socket.GetSocketToString()}");

            if (Server.PeerToLobby != null &&
                Equals(Server.PeerToLobby.RemoteSocket, socket))
            {
                Server.PeerToLobby.Dispose();
                Server.PeerToLobby = null;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnDisconnectLogicServer
        /// 作者：taixihuase
        /// 作用：当收到逻辑服务器断线消息时进行处理
        /// 编写日期：2016/2/14
        /// </summary>
        /// <param name="event"></param>
        private void OnDisconnectLogicServer(IEventData @event)
        {
            SocketGuid socket =
                Serialization.Deserialize<SocketGuid>(@event.Parameters[(byte) S2SParaCode.SubServerSocket]);
            Log.Debug($"[{ServerTime.Instance.Time}]一个 Logic Server 已断开连接 [Socket]{socket.GetSocketToString()}");

            if (LogicServerCollection.Instance.GuidToLogicServer.ContainsKey(socket))
            {                
                var peer = LogicServerCollection.Instance.GuidToLogicServer[socket];
                LogicServerCollection.Instance.RemoveLogicServer(socket);
                peer.Dispose();
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnSetDefaultLogicServer
        /// 作者：taixihuase
        /// 作用：当收到设置默认逻辑服务器消息时进行处理
        /// 编写日期：2016/3/7
        /// </summary>
        /// <param name="event"></param>
        private void OnSetDefaultLogicServer(IEventData @event)
        {
            List<SocketGuid> list =
                Serialization.Deserialize<List<SocketGuid>>(@event.Parameters[(byte) S2SParaCode.SocketList]);
            LogicServerCollection.Instance.UpdateDefaultLogicServers(list);
        }

        #endregion
    }
}
