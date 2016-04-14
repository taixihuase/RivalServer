//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：PeerToLogicServer.cs
//
// 文件功能描述：
//
// 网关代理服务器与逻辑服务器的连线实例
//
// 创建标识：taixihuase 20160203
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
    /// 名称：PeerToLogicServer
    /// 作者：taixihuase
    /// 作用：用于网关代理服务器与逻辑服务器之间的数据传输
    /// 编写日期：2016/1/31
    /// </summary>
    public class PeerToLogicServer : OutboundS2SPeer
    {
        public readonly ProxyServerApplication Server;

        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public ExtendedPoolFiber Fiber { get; set; }

        public string Name { get; set; }

        public SocketGuid RemoteSocket { get; protected set; }

        public readonly ServerInfo ReconnectInfo;

        /// <summary>
        /// 类型：方法
        /// 名称：PeerToLogicServer
        /// 作者：taixihuase
        /// 作用：构造连接进程实例
        /// 编写日期：2016/2/13
        /// </summary>
        /// <param name="info"></param>
        /// <param name="server"></param>
        public PeerToLogicServer(ServerInfo info, ProxyServerApplication server): base(server)
        {
            Name = info.ServerName;
            RemoteSocket = info.Socket;
            ReconnectInfo = info;
            Server = server;
            Initialize();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：ConnectToLogic
        /// 作者：taixihuase
        /// 作用：连接逻辑服务器
        /// 编写日期：2016/2/16
        /// </summary>
        public void ConnectToLogic()
        {
            lock (this)
            {
                string ip = RemoteSocket.GetIpToString();
                if (ip == IpTool.GetPublicIpAddress())
                    ip = "127.0.0.1";

                ConnectTcp(
                    new IPEndPoint(IPAddress.Parse(ip), RemoteSocket.Port), Name, null);
            }
        }

        #region protected override methods

        /// <summary>
        /// 类型：方法
        /// 名称：OnConnectionEstablished
        /// 作者：taixihuase
        /// 作用：网关代理服务器连接逻辑服务器成功后进行处理
        /// 编写日期：2016/2/16
        /// </summary>
        /// <param name="responseObject"></param>
        protected override void OnConnectionEstablished(object responseObject)
        {
            LogicServerCollection.Instance.AddLogicServer(RemoteSocket, this);
            Server.ConnectingLogicServer.RemoveAll(i => Equals(i, RemoteSocket));
            Log.Debug(
                $"[{ServerTime.Instance.Time}]{Server.Info.ServerType} 成功连接 Logic Server [Socket]{RemoteSocket.GetSocketToString()} [Server Name]{Name}");
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnConnectionFailed
        /// 作者：taixihuase
        /// 作用：网关代理服务器连接逻辑服务器失败时进行重连
        /// 编写日期：2016/2/16
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        protected override void OnConnectionFailed(int errorCode, string errorMessage)
        {
            Log.Debug(
                $"[{ServerTime.Instance.Time}]{Server.Info.ServerType} 无法连接 {Name} [Socket]{RemoteSocket.GetSocketToString()}");
            Server.ReconnectToServer(ReconnectInfo);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnOperationRequest
        /// 作者：taixihuase
        /// 作用：响应并处理服务端发来的请求，该方法禁用
        /// 编写日期：2016/2/3
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
        /// 作用：当与逻辑服务器断开连接时进行处理
        /// 编写日期：2016/2/3
        /// </summary>
        /// <param name="reasonCode"></param>
        /// <param name="reasonDetail"></param>
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            Release();
            Log.Debug($"[{ServerTime.Instance.Time}]{Server.Info.ServerType} 与 Logic Server 连接中断 [Socket]{RemoteSocket.GetSocketToString()}");
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnEvent
        /// 作者：taixihuase
        /// 作用：监听逻辑服务器发来的广播并回调触发事件
        /// 编写日期：2016/2/3
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="sendParameters"></param>
        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {

        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnOperationResponse
        /// 作者：taixihuase
        /// 作用：网关代理服务器发送请求后，接收并处理相应的主服务器响应内容
        /// 编写日期：2016/2/3
        /// </summary>
        /// <param name="operationResponse"></param>
        /// <param name="sendParameters"></param>
        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            switch ((S2SOpCode)operationResponse.OperationCode)
            {
            }
        }

        #endregion

        /// <summary>
        /// 类型：方法
        /// 名称：SendOperationRequestToLogic
        /// 作者：taixihuase
        /// 作用：向逻辑服务器发送请求
        /// 编写日期：2016/2/13
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="paraCode"></param>
        /// <param name="obj"></param>
        public void SendOperationRequestToLogic(S2SOpCode opCode, S2SParaCode? paraCode = null, object obj = null)
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

        #region private methods

        /// <summary>
        /// 类型：方法
        /// 名称：Initialize
        /// 作者：taixihuase
        /// 作用：进行资源初始化
        /// 编写日期：2016/2/13
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
        /// 编写日期：2016/2/13
        /// </summary>
        private void Release()
        {
            LogicServerCollection.Instance.RemoveLogicServer(RemoteSocket);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：ReconnectToLogic
        /// 作者：taixihuase
        /// 作用：重连逻辑服务器
        /// 编写日期：2016/2/13
        /// </summary>
        /// <param name="delay"></param>
        private void ReconnectToLogic(long delay)
        {
            lock (this)
            {
                Fiber.Schedule(ConnectToLogic, delay);
            }
        }

        #endregion
    }
}
