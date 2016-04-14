//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：PeerToProxyServer.cs
//
// 文件功能描述：
//
// 逻辑服务器与网关代理服务器的连线实例
//
// 创建标识：taixihuase 20160216
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
using ExitGames.Logging;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using Protocol;
using S2SProtocol.Common;

namespace LogicServer
{
    /// <summary>
    /// 类型：类
    /// 名称：PeerToProxyServer
    /// 作者：taixihuase
    /// 作用：用于逻辑服务器与网关代理服务器之间的数据传输
    /// 编写日期：2016/2/16
    /// </summary>
    internal class PeerToProxyServer : InboundS2SPeer
    {
        public readonly LogicServerApplication Server;

        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public SocketGuid Guid { get; set; }

        /// <summary>
        /// 类型：方法
        /// 名称：PeerToProxyServer
        /// 作者：taixihuase
        /// 作用：构造 PeerToProxyServer 对象
        /// 编写日期：2016/2/16
        /// </summary>
        /// <param name="initRequest"></param>
        /// <param name="server"></param>
        public PeerToProxyServer(InitRequest initRequest, LogicServerApplication server)
            : base(initRequest)
        {
            Server = server;
            Guid = new SocketGuid(initRequest.PhotonPeer.GetRemoteIP(), initRequest.PhotonPeer.GetRemotePort());
            Initialize();
            Log.Debug($"[{ServerTime.Instance.Time}]一个 Proxy Server 成功连接本服务器 [Socket]{Guid.GetSocketToString()}");
        }

        /// <summary>
        /// 类型：方法
        /// 名称：CreateEventData
        /// 作者：taixihuase
        /// 作用：生成简单事件数据对象
        /// 编写日期：2016/3/26
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
        /// 编写日期：2016/3/26
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="paraCode"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public EventData CreateEventData(S2SEventCode eventCode, S2SParaCode paraCode, object obj)
        {
            return new EventData((byte)eventCode)
            {
                Parameters =
                    Serialization.IsNeed(obj)
                        ? new Dictionary<byte, object> { { (byte)paraCode, Serialization.Serialize(obj) } }
                        : new Dictionary<byte, object> { { (byte)paraCode, obj } }
            };
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SendEventToProxy
        /// 作者：taixihuase
        /// 作用：向当前网关代理服务器发送简单广播事件
        /// 编写日期：2016/3/26
        /// </summary>
        /// <param name="eventCode"></param>
        public void SendEventToProxy(S2SEventCode eventCode)
        {
            var eventData = new EventData((byte)eventCode);
            SendEvent(eventData, new SendParameters());
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SendEventToProxy
        /// 作者：taixihuase
        /// 作用：向当前网关代理服务器发送完整广播事件
        /// 编写日期：2016/3/26
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="paraCode"></param>
        /// <param name="obj"></param>
        public void SendEventToProxy(S2SEventCode eventCode, S2SParaCode paraCode, object obj)
        {
            var eventData = new EventData((byte)eventCode)
            {
                Parameters =
                    Serialization.IsNeed(obj)
                        ? new Dictionary<byte, object> { { (byte)paraCode, Serialization.Serialize(obj) } }
                        : new Dictionary<byte, object> { { (byte)paraCode, obj } }
            };

            SendEvent(eventData, new SendParameters());
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SendOperationResponseToProxy
        /// 作者：taixihuase
        /// 作用：向网关代理服务器发送简单答复
        /// 编写日期：2016/3/26
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="ret"></param>
        /// <param name="msg"></param>
        public void SendOperationResponseToProxy(S2SOpCode opCode, S2SRetCode ret, string msg = null)
        {
            SendOperationResponseToProxy(opCode, ret, null, null, msg);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SendOperationResponseToProxy
        /// 作者：taixihuase
        /// 作用：向网关代理服务器发送完整答复
        /// 编写日期：2016/3/26
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="ret"></param>
        /// <param name="paraCode"></param>
        /// <param name="obj"></param>
        /// <param name="msg"></param>
        public void SendOperationResponseToProxy(S2SOpCode opCode, S2SRetCode ret, S2SParaCode? paraCode = null,
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

        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
            throw new NotImplementedException();
        }

        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnOperationRequest
        /// 作者：taixihuase
        /// 作用：响应并处理网关代理服务器发来的请求
        /// 编写日期：2016/2/16
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            switch ((S2SOpCode)Enum.Parse(typeof(S2SOpCode), operationRequest.OperationCode.ToString()))
            {

            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnDisconnect
        /// 作者：taixihuase
        /// 作用：当与网关代理服务器断开连接时进行处理
        /// 编写日期：2016/2/16
        /// </summary>
        /// <param name="reasonCode"></param>
        /// <param name="reasonDetail"></param>
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            Release();
            Log.Debug($"[{ServerTime.Instance.Time}]一个 Proxy Server 断开连接 [Socket]{Guid.GetSocketToString()}");
        }

        #endregion

        #region private methods

        /// <summary>
        /// 类型：方法
        /// 名称：Initialize
        /// 作者：taixihuase
        /// 作用：进行资源初始化
        /// 编写日期：2016/2/16
        /// </summary>
        private void Initialize()
        {
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Release
        /// 作者：taixihuase
        /// 作用：释放资源
        /// 编写日期：2016/2/16
        /// </summary>
        private void Release()
        {

        }

        #endregion
    }
}