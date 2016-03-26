//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：PeerToClient.cs
//
// 文件功能描述：
//
// 网关代理服务器与客户端的连线实例
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
using ExitGames.Logging;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using Protocol;
using C2SProtocol.Common;
using S2SProtocol.Common;

namespace ProxyServer
{
    /// <summary>
    /// 类型：类
    /// 名称：PeerToClient
    /// 作者：taixihuase
    /// 作用：用于网关代理服务器与客户端之间的数据传输
    /// 编写日期：2016/1/31
    /// </summary>
    public class PeerToClient : ClientPeer
    {
        public readonly ProxyServerApplication ProxyServer;

        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public SocketGuid Guid { get; set; }

        /// <summary>
        /// 类型：方法
        /// 名称：PeerToClient
        /// 作者：taixihuase
        /// 作用：构造 PeerToClient 对象
        /// 编写日期：2016/1/31
        /// </summary>
        /// <param name="initRequest"></param>
        /// <param name="server"></param>
        public PeerToClient(InitRequest initRequest, ProxyServerApplication server) : base(initRequest)
        {
            ProxyServer = server;
            Guid = new SocketGuid(initRequest.PhotonPeer.GetRemoteIP(), initRequest.PhotonPeer.GetRemotePort());
            Initialize();
            Log.Debug($"[{DateTime.Now}]一个客户端成功连接本服务器 [Socket]{Guid.GetSocketToString()}");
        }

        /// <summary>
        /// 类型：方法
        /// 名称：CreateEventData
        /// 作者：taixihuase
        /// 作用：生成简单事件数据对象
        /// 编写日期：2016/3/15
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
        /// 编写日期：2016/3/15
        /// </summary>
        /// <param name="event"></param>
        /// <param name="para"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public EventData CreateEventData(S2SEventCode @event, S2SParaCode para, object obj)
        {
            return new EventData((byte)@event)
            {
                Parameters = new Dictionary<byte, object> { { (byte)para, Serialization.Serialize(obj) } }
            };
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SendEventToClient
        /// 作者：taixihuase
        /// 作用：向当前客户端发送简单广播事件
        /// 编写日期：2016/3/15
        /// </summary>
        /// <param name="eventCode"></param>
        public void SendEventToClient(C2SEventCode eventCode)
        {
            var eventData = new EventData((byte)eventCode);
            SendEvent(eventData, new SendParameters());
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SendEventToClient
        /// 作者：taixihuase
        /// 作用：向当前客户端发送完整广播事件
        /// 编写日期：2016/3/15
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="paraCode"></param>
        /// <param name="obj"></param>
        public void SendEventToClient(C2SEventCode eventCode, C2SParaCode paraCode, object obj)
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
        /// 名称：SendOperationResponseToClient
        /// 作者：taixihuase
        /// 作用：向客户端发送简单答复
        /// 编写日期：2016/3/15
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="ret"></param>
        /// <param name="msg"></param>
        public void SendOperationResponseToClient(C2SOpCode opCode, C2SRetCode ret, string msg = null)
        {
            SendOperationResponseToClient(opCode, ret, null, null, msg);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SendOperationResponseToClient
        /// 作者：taixihuase
        /// 作用：向客户端发送完整答复
        /// 编写日期：2016/3/15
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="ret"></param>
        /// <param name="paraCode"></param>
        /// <param name="obj"></param>
        /// <param name="msg"></param>
        public void SendOperationResponseToClient(C2SOpCode opCode, C2SRetCode ret, C2SParaCode? paraCode = null,
            object obj = null, string msg = null)
        {
            var request = new OperationResponse((byte)opCode) { ReturnCode = (short)ret };

            if (paraCode.HasValue && obj != null)
            {
                request.Parameters = Serialization.IsNeed(obj)
                    ? new Dictionary<byte, object> { { (byte)paraCode, Serialization.Serialize(obj) } }
                    : new Dictionary<byte, object> { { (byte)paraCode, obj } };
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
        /// 作用：响应并处理客户端发来的请求
        /// 编写日期：2016/1/31
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            switch ((C2SOpCode)Enum.Parse(typeof(C2SOpCode), operationRequest.OperationCode.ToString()))
            {


            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnDisconnect
        /// 作者：taixihuase
        /// 作用：当与客户端断开连接时进行处理
        /// 编写日期：2016/1/31
        /// </summary>
        /// <param name="reasonCode"></param>
        /// <param name="reasonDetail"></param>
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            Release();
            Log.Debug($"[{DateTime.Now}]一个客户端断开连接 [Socket]{Guid.GetSocketToString()}");
        }

        #endregion

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

        }

        #endregion
    }
}
