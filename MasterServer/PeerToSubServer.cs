//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：PeerToSubServer.cs
//
// 文件功能描述：
//
// 主服务器与子服务器的连线实例
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
using ExitGames.Concurrency.Fibers;
using ExitGames.Logging;
using MasterServer.LoginServerRequest;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using Protocol;
using S2SProtocol.Common;

namespace MasterServer
{
    /// <summary>
    /// 类型：类
    /// 名称：PeerToSubServer
    /// 作者：taixihuase
    /// 作用：用于主服务器与子服务器之间的数据传输
    /// 编写日期：2016/1/25
    /// </summary>
    public class PeerToSubServer : InboundS2SPeer
    {
        public readonly MasterServerApplication Server;

        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public ServerType Type { get; protected set; }

        public string Name { get; protected set; }

        public SocketGuid Guid { get; protected set; }

        public ushort ListeningPort { get; protected set; }

        public float CpuLoad { get; protected set; }

        public ServerLoad.LoadLevel LoadLevel { get; protected set; }

        public ExtendedPoolFiber Fiber { get; set; }

        /// <summary>
        /// 类型：方法
        /// 名称：PeerToSubServer
        /// 作者：taixihuase
        /// 作用：构造 PeerToSubServer 对象
        /// 编写日期：2016/1/25
        /// </summary>
        /// <param name="initRequest"></param>
        /// <param name="server"></param>
        public PeerToSubServer(InitRequest initRequest, MasterServerApplication server) : base(initRequest)
        {
            Server = server;
            Guid = new SocketGuid(initRequest.PhotonPeer.GetRemoteIP(), initRequest.PhotonPeer.GetRemotePort());
            Initialize();
            Log.Debug(
                $"[{ServerTime.Instance.Time}]一个子服务器成功连接本服务器 [Socket]{Guid.GetSocketToString()}");
            Log.Debug(
                $"[{ServerTime.Instance.Time}]{initRequest.PhotonPeer.GetLocalIP()}:{initRequest.PhotonPeer.GetLocalPort()}--{initRequest.PhotonPeer.GetRemoteIP()}:{initRequest.PhotonPeer.GetRemotePort()}");
        }

        /// <summary>
        /// 类型：方法
        /// 名称：CreateEventData
        /// 作者：taixihuase
        /// 作用：生成简单事件数据对象
        /// 编写日期：2016/2/14
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public EventData CreateEventData(S2SEventCode @event)
        {
            return new EventData((byte)@event);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：CreateEventData
        /// 作者：taixihuase
        /// 作用：生成完整事件数据对象
        /// 编写日期：2016/2/14
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="paraCode"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public EventData CreateEventData(S2SEventCode eventCode, S2SParaCode paraCode, object obj)
        {
            return new EventData((byte) eventCode)
            {
                Parameters =
                    Serialization.IsNeed(obj)
                        ? new Dictionary<byte, object> {{(byte) paraCode, Serialization.Serialize(obj)}}
                        : new Dictionary<byte, object> {{(byte) paraCode, obj}}
            };
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SendEventToSub
        /// 作者：taixihuase
        /// 作用：向当前子服务器发送简单广播事件
        /// 编写日期：2016/3/15
        /// </summary>
        /// <param name="eventCode"></param>
        public void SendEventToSub(S2SEventCode eventCode)
        {
            var eventData = new EventData((byte)eventCode);
            SendEvent(eventData, new SendParameters());
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SendEventToSub
        /// 作者：taixihuase
        /// 作用：向当前子服务器发送完整广播事件
        /// 编写日期：2016/2/14
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="paraCode"></param>
        /// <param name="obj"></param>
        public void SendEventToSub(S2SEventCode eventCode, S2SParaCode paraCode, object obj)
        {
            var eventData = new EventData((byte) eventCode)
            {
                Parameters =
                    Serialization.IsNeed(obj)
                        ? new Dictionary<byte, object> {{(byte) paraCode, Serialization.Serialize(obj)}}
                        : new Dictionary<byte, object> {{(byte) paraCode, obj}}
            };

            SendEvent(eventData, new SendParameters());
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SendOperationResponseToSub
        /// 作者：taixihuase
        /// 作用：向子服务器发送简单答复
        /// 编写日期：2016/2/14
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="ret"></param>
        /// <param name="msg"></param>
        public void SendOperationResponseToSub(S2SOpCode opCode, S2SRetCode ret, string msg = null)
        {
            SendOperationResponseToSub(opCode, ret, null, null, msg);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SendOperationResponseToSub
        /// 作者：taixihuase
        /// 作用：向子服务器发送完整答复
        /// 编写日期：2016/2/14
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="ret"></param>
        /// <param name="paraCode"></param>
        /// <param name="obj"></param>
        /// <param name="msg"></param>
        public void SendOperationResponseToSub(S2SOpCode opCode, S2SRetCode ret, S2SParaCode? paraCode = null,
            object obj = null, string msg = null)
        {
            var request = new OperationResponse((byte)opCode) { ReturnCode = (short)ret };

            if (paraCode.HasValue && obj != null)
            {
                request.Parameters = Serialization.IsNeed(obj)
                    ? new Dictionary<byte, object> {{(byte) paraCode, Serialization.Serialize(obj)}}
                    : new Dictionary<byte, object> {{(byte) paraCode, obj}};
            }
            if (msg != null)
            {
                request.DebugMessage = msg;
            }

            SendOperationResponse(request, new SendParameters());
        }

        #region protected override methods

        /// <summary>
        /// 类型：方法
        /// 名称：OnOperationRequest
        /// 作者：taixihuase
        /// 作用：响应并处理子服务器发来的请求
        /// 编写日期：2016/1/25
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            switch ((S2SOpCode)Enum.Parse(typeof(S2SOpCode), operationRequest.OperationCode.ToString()))
            {
                    #region Sub Server Op

                case S2SOpCode.RegistSubServer:
                    OnSubServerRegist(operationRequest);
                    break;
                case S2SOpCode.ReportServerLoad:
                    OnSubServerReportLoad(operationRequest);
                    break;

                    #endregion

                    #region Client To Login Op

                case S2SOpCode.ClientLogin:
                    Login.OnRequest(operationRequest, sendParameters, this);
                    break;
                case S2SOpCode.ClientApplyForCaptcha:
                    Regist.OnRequest(operationRequest, sendParameters, this);
                    break;
                case S2SOpCode.ClientRegistCheck:
                    Regist.OnRequest(operationRequest, sendParameters, this);
                    break;
                case S2SOpCode.ClientRegist:
                    Regist.OnRequest(operationRequest, sendParameters, this);
                    break;

                    #endregion

            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnDisconnect
        /// 作者：taixihuase
        /// 作用：当与子服务器断开连接时进行处理
        /// 编写日期：2016/1/25
        /// </summary>
        /// <param name="reasonCode"></param>
        /// <param name="reasonDetail"></param>
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            Release();
            if (Type == ServerType.UndefinedServer)
            {
                Log.Debug(
                    $"[{ServerTime.Instance.Time}]一个 '{Type}' 断开连接 [Socket]{Guid.GetSocketToString()}");
            }
            else
            {
                Log.Debug(
                    $"[{ServerTime.Instance.Time}]一个 '{Type}' 断开连接 [Socket]{Guid.GetSocketToString()} [Server Name]{Name}");
            }
        }

        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
        }

        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
        }

        #endregion

        #region private methods

        /// <summary>
        /// 类型：方法
        /// 名称：Initialize
        /// 作者：taixihuase
        /// 作用：进行资源初始化
        /// 编写日期：2016/1/30
        /// </summary>
        private void Initialize()
        {
            Type = ServerType.UndefinedServer;
            LoadLevel = ServerLoad.LoadLevel.Undetected;
            Name = string.Empty;
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
            if (Type != ServerType.UndefinedServer)
            {
                SubServerCollection.Instance.RemoveSubServer(Guid);
            }
            Fiber.Stop();

            List<PeerToSubServer> proxys;
            SocketGuid disconnect = new SocketGuid(Guid.GetIpToString(), ListeningPort);
            switch (Type)
            {
                case ServerType.LogicServer:
                    if (SubServerCollection.Instance.TryGetProxyServerPeers(out proxys))
                    {
                        CreateEventData(S2SEventCode.DisconnectLogicServer, S2SParaCode.SubServerSocket, disconnect)
                            .SendTo(proxys, new SendParameters());
                    }
                    break;
                case ServerType.LobbyServer:
                    if (SubServerCollection.Instance.TryGetProxyServerPeers(out proxys))
                    {
                        CreateEventData(S2SEventCode.DisconnectLobbyServer, S2SParaCode.SubServerSocket, disconnect)
                            .SendTo(proxys, new SendParameters());
                    }
                    break;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnSubServerRegist
        /// 作者：taixihuase
        /// 作用：注册子服务器
        /// 编写日期：2016/1/25
        /// </summary>
        /// <param name="request"></param>
        private void OnSubServerRegist(OperationRequest request)
        {
            var para = request.Parameters;
            ServerInfo data =
                Serialization.Deserialize<ServerInfo>(para[(byte) S2SParaCode.SubServerInfo]);

            Guid = data.Socket;

            Type = data.ServerType;

            Name = data.ServerName;

            ListeningPort = data.ListeningPort;

            bool regist = SubServerCollection.Instance.AddSubServer(Guid, this);
            if (regist)
            {
                Log.Debug(
                    $"[{ServerTime.Instance.Time}]一个 '{Type}' 成功注册 [Socket]{Guid.GetSocketToString()} [Server Name]{Name} [Listening Port]{ListeningPort}");
                SendOperationResponseToSub(S2SOpCode.RegistSubServer, S2SRetCode.Success, "成功注册服务器");

                List<PeerToSubServer> proxys;
                EventData eventdata;
                switch (Type)
                {
                    case ServerType.LobbyServer:
                        if (SubServerCollection.Instance.TryGetProxyServerPeers(out proxys))
                        {
                            ServerInfo info = new ServerInfo
                            {
                                ServerType = Type,
                                ServerName = Name,
                                ListeningPort = ListeningPort,
                                Socket = new SocketGuid(Guid.GetIpToString(), ListeningPort)
                            };
                            eventdata = CreateEventData(S2SEventCode.ConnectLobbyServer, S2SParaCode.SubServerInfo,
                                info);
                            eventdata.SendTo(proxys, new SendParameters());
                        }
                        break;

                    case ServerType.LogicServer:
                        if (SubServerCollection.Instance.TryGetProxyServerPeers(out proxys))
                        {
                            ServerInfo info = new ServerInfo
                            {
                                ServerType = Type,
                                ServerName = Name,
                                ListeningPort = ListeningPort,
                                Socket = new SocketGuid(Guid.GetIpToString(), ListeningPort)
                            };
                            eventdata = CreateEventData(S2SEventCode.ConnectLogicServer, S2SParaCode.SubServerInfo,
                                info);
                            eventdata.SendTo(proxys, new SendParameters());
                        }
                        break;

                    case ServerType.ProxyServer:
                        List<PeerToSubServer> servers;
                        Dictionary<ServerType, List<ServerInfo>> infos = new Dictionary<ServerType, List<ServerInfo>>();
                        if (SubServerCollection.Instance.TryGetLogicServerPeers(out servers))
                        {
                            infos.Add(ServerType.LogicServer, new List<ServerInfo>());
                            foreach (var logic in servers)
                            {
                                ServerInfo i = new ServerInfo
                                {
                                    ServerType = logic.Type,
                                    ServerName = logic.Name,
                                    ListeningPort = logic.ListeningPort,
                                    Socket = new SocketGuid(logic.Guid.GetIpToString(), logic.ListeningPort)
                                };
                                infos[ServerType.LogicServer].Add(i);
                            }
                        }
                        if (SubServerCollection.Instance.TryGetServerPeers(ServerType.LobbyServer, out servers))
                        {
                            infos.Add(ServerType.LobbyServer, new List<ServerInfo>());
                            foreach (var lobby in servers)
                            {
                                ServerInfo i = new ServerInfo
                                {
                                    ServerType = lobby.Type,
                                    ServerName = lobby.Name,
                                    ListeningPort = lobby.ListeningPort,
                                    Socket = new SocketGuid(lobby.Guid.GetIpToString(), lobby.ListeningPort)
                                };
                                infos[ServerType.LobbyServer].Add(i);
                            }
                        }
                        SendEventToSub(S2SEventCode.ConnectProxyServer, S2SParaCode.SubServerInfo,
                            infos);
                        break;
                }
            }
            else
            {
                Log.Debug(
                    $"[{ServerTime.Instance.Time}]一个 '{Type}' 无法注册 [Socket]{Guid.GetSocketToString()} [Server Name]{Name} [Reason]该服务器已注册过");
                SendOperationResponseToSub(S2SOpCode.RegistSubServer, S2SRetCode.Failure, "该服务器已注册");
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnSubServerReportLoad
        /// 作者：taixihuase
        /// 作用：记录子服务器负载情况
        /// 编写日期：2016/1/30
        /// </summary>
        /// <param name="request"></param>
        private void OnSubServerReportLoad(OperationRequest request)
        {
            var para = request.Parameters;
            ServerLoad data = Serialization.Deserialize<ServerLoad>(para[(byte) S2SParaCode.SubServerLoad]);

            CpuLoad = data.CpuLoad;
            LoadLevel = data.ServerLoadLevel;

            Log.Debug(
                $"[{ServerTime.Instance.Time}][Server Name]{Name} 汇报负载 [CPU]{CpuLoad} [Load Level]{LoadLevel} [Server Type]{Type} [Socket]{Guid.GetSocketToString()}");

            SendOperationResponseToSub(S2SOpCode.ReportServerLoad, S2SRetCode.Success, null);
        }

        #endregion
    }
}
