//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：Regist.cs
//
// 文件功能描述：
//
// 注册新用户账号，响应客户端注册账号请求
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

using System;
using System.Collections.Generic;
using C2SProtocol.Common;
using Photon.SocketServer;
using Protocol;
using S2SProtocol.Common;

namespace LoginServer.ServerLogic
{
    /// <summary>
    /// 类型：类
    /// 名称：Regist
    /// 作者：taixihuase
    /// 作用：响应注册请求
    /// 编写日期：2016/3/16
    /// </summary>
    public static class Regist
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
            switch ((C2SOpCode)Enum.Parse(typeof(C2SOpCode), operationRequest.OperationCode.ToString()))
            {
                case C2SOpCode.RegistCheck:
                    TryCheck(operationRequest, sendParameters, peer);
                    break;
                case C2SOpCode.ApplyForCaptcha:
                    TryApplyForCaptcha(operationRequest, sendParameters, peer);
                    break;
                case C2SOpCode.Regist:
                    TryRegist(operationRequest, sendParameters, peer);
                    break;
            }
        }

        public static void OnResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            switch ((S2SOpCode)Enum.Parse(typeof(S2SOpCode), operationResponse.OperationCode.ToString()))
            {
                case S2SOpCode.ClientRegistCheck:
                    OnCheck(operationResponse, sendParameters);
                    break;
                case S2SOpCode.ClientApplyForCaptcha:
                    OnApplyForCaptcha(operationResponse, sendParameters);
                    break;
                case S2SOpCode.ClientRegist:
                    OnRegist(operationResponse, sendParameters);
                    break;
            }
        }

        #region Request

        /// <summary>
        /// 类型：方法
        /// 名称：TryCheck
        /// 作者：taixihuase
        /// 作用：通过注册数据尝试验证是否存在
        /// 编写日期：2016/3/16
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void TryCheck(OperationRequest operationRequest, SendParameters sendParameters, PeerToClient peer)
        {
            var guid = peer.Guid;
            var email = operationRequest.Parameters[(byte) C2SParaCode.Email];

            var request = new OperationRequest
            {
                OperationCode = (byte) S2SOpCode.ClientRegistCheck,
                Parameters = new Dictionary<byte, object>
                {
                    {(byte) S2SParaCode.ClientEmail, email},
                    {(byte) S2SParaCode.ClientSocket, Serialization.Serialize(guid)}
                }
            };

            peer.LoginServer.PeerToMaster.SendOperationRequest(request, sendParameters);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryCheck
        /// 作者：taixihuase
        /// 作用：通过注册数据尝试注册新用户
        /// 编写日期：2016/3/16
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void TryRegist(OperationRequest operationRequest, SendParameters sendParameters,
            PeerToClient peer)
        {
            var guid = peer.Guid;
            var regist = operationRequest.Parameters[(byte) C2SParaCode.Regist];

            var request = new OperationRequest
            {
                OperationCode = (byte) S2SOpCode.ClientRegist,
                Parameters = new Dictionary<byte, object>
                {
                    {(byte) S2SParaCode.ClientRegist, regist},
                    {(byte) S2SParaCode.ClientSocket, Serialization.Serialize(guid)}
                }
            };

            peer.LoginServer.PeerToMaster.SendOperationRequest(request, sendParameters);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryApplyForCaptcha
        /// 作者：taixihuase
        /// 作用：通过邮箱尝试申请验证码
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void TryApplyForCaptcha(OperationRequest operationRequest, SendParameters sendParameters,
            PeerToClient peer)
        {
            var guid = peer.Guid;
            var email = operationRequest.Parameters[(byte) C2SParaCode.Email];

            var request = new OperationRequest
            {
                OperationCode = (byte) S2SOpCode.ClientApplyForCaptcha,
                Parameters = new Dictionary<byte, object>
                {
                    {(byte) S2SParaCode.ClientEmail, email},
                    {(byte) S2SParaCode.ClientSocket, Serialization.Serialize(guid)}
                }
            };

            peer.LoginServer.PeerToMaster.SendOperationRequest(request, sendParameters);
        }

        #endregion

        #region Response

        private static void OnApplyForCaptcha(OperationResponse operationResponse, SendParameters sendParameters)
        {
            var guid =
                Serialization.Deserialize<SocketGuid>(operationResponse.Parameters[(byte) S2SParaCode.ClientSocket]);
            var peer = ClientPeerCollection.Instance.TryGetPeer(guid);

            if (peer != null)
            {
                if (operationResponse.ReturnCode == (short) S2SRetCode.Success)
                    peer.SendOperationResponseToClient(C2SOpCode.ApplyForCaptcha, C2SRetCode.Success,
                        operationResponse.DebugMessage);
                if (operationResponse.ReturnCode == (short) S2SRetCode.Failure)
                    peer.SendOperationResponseToClient(C2SOpCode.ApplyForCaptcha, C2SRetCode.Failure,
                        operationResponse.DebugMessage);
            }
        }

        private static void OnCheck(OperationResponse operationResponse, SendParameters sendParameters)
        {
            var guid =
                Serialization.Deserialize<SocketGuid>(operationResponse.Parameters[(byte)S2SParaCode.ClientSocket]);
            var peer = ClientPeerCollection.Instance.TryGetPeer(guid);

            if (peer != null)
            {
                if (operationResponse.ReturnCode == (short) S2SRetCode.Success)
                    peer.SendOperationResponseToClient(C2SOpCode.RegistCheck, C2SRetCode.Success,
                        operationResponse.DebugMessage);
                if (operationResponse.ReturnCode == (short) S2SRetCode.Failure)
                    peer.SendOperationResponseToClient(C2SOpCode.RegistCheck, C2SRetCode.Failure,
                        operationResponse.DebugMessage);
            }
        }

        private static void OnRegist(OperationResponse operationResponse, SendParameters sendParameters)
        {
            var guid =
                Serialization.Deserialize<SocketGuid>(operationResponse.Parameters[(byte) S2SParaCode.ClientSocket]);
            var peer = ClientPeerCollection.Instance.TryGetPeer(guid);

            if (peer != null)
            {
                if (operationResponse.ReturnCode == (short) S2SRetCode.Success)
                    peer.SendOperationResponseToClient(C2SOpCode.Regist, C2SRetCode.Success,
                        operationResponse.DebugMessage);
                if (operationResponse.ReturnCode == (short) S2SRetCode.Failure)
                    peer.SendOperationResponseToClient(C2SOpCode.Regist, C2SRetCode.Failure,
                        operationResponse.DebugMessage);
            }
        }

        #endregion

    }
}
