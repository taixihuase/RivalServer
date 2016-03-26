//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：Login.cs
//
// 文件功能描述：
//
// 登录用户账号，响应登录服务器转发的登录账号请求
//
// 创建标识：taixihuase 20160317
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//-----------------------------------------------------------------------------------------------------------

using C2SProtocol.User;
using Photon.SocketServer;
using Protocol;
using S2SProtocol.Common;

namespace MasterServer.LoginServerRequest
{
    /// <summary>
    /// 类型：类
    /// 名称：Login
    /// 作者：taixihuase
    /// 作用：响应登录请求
    /// 编写日期：2016/3/17
    /// </summary>
    public static class Login
    {
        /// <summary>
        /// 类型：方法
        /// 名称：OnRequest
        /// 作者：taixihuase
        /// 作用：当收到请求时，进行处理
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        public static void OnRequest(OperationRequest operationRequest, SendParameters sendParameters, PeerToSubServer peer)
        {
            TryLogin(operationRequest, sendParameters, peer);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryLogin
        /// 作者：taixihuase
        /// 作用：通过登录数据尝试登录
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void TryLogin(OperationRequest operationRequest, SendParameters sendParameters, PeerToSubServer peer)
        {
            var login = Serialization.Deserialize<LoginInfo>(operationRequest.Parameters[(byte) S2SParaCode.ClientLogin]);
            var guid = Serialization.Deserialize<SocketGuid>(operationRequest.Parameters[(byte)S2SParaCode.ClientSocket]);
            PeerToSubServer.Log.Debug(login.Account);
            PeerToSubServer.Log.Debug("Guid = " + guid.GetSocketToString());
        }
    }
}
