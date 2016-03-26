//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：Login.cs
//
// 文件功能描述：
//
// 登录用户账号，响应客户端登录账号请求
//
// 创建标识：taixihuase 20160316
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
using C2SProtocol.Common;
using C2SProtocol.User;
using Photon.SocketServer;
using Protocol;
using S2SProtocol.Common;

namespace LoginServer.ServerLogic
{
    /// <summary>
    /// 类型：类
    /// 名称：Login
    /// 作者：taixihuase
    /// 作用：响应登录请求
    /// 编写日期：2016/3/16
    /// </summary>
    public static class Login
    {
        /// <summary>
        /// 类型：方法
        /// 名称：OnRequest
        /// 作者：taixihuase
        /// 作用：当收到请求时，进行处理
        /// 编写日期：2016/3/16
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        public static void OnRequest(OperationRequest operationRequest, SendParameters sendParameters, PeerToClient peer)
        {
            TryLogin(operationRequest, sendParameters, peer);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryLogin
        /// 作者：taixihuase
        /// 作用：通过登录数据尝试登录
        /// 编写日期：2016/3/16
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void TryLogin(OperationRequest operationRequest, SendParameters sendParameters, PeerToClient peer)
        {
            var guid = peer.Guid;
            var login = operationRequest.Parameters[(byte) C2SParaCode.Login];

            var request = new OperationRequest
            {
                OperationCode = (byte)S2SOpCode.ClientLogin,
                Parameters = new Dictionary<byte, object>
                {
                    {(byte) S2SParaCode.ClientLogin, login},
                    {(byte) S2SParaCode.ClientSocket, Serialization.Serialize(guid)}
                }
            };

            peer.LoginServer.PeerToMaster.SendOperationRequest(request, sendParameters);
        }
    }
}
