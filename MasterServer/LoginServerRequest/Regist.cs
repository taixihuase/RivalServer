//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：Regist.cs
//
// 文件功能描述：
//
// 注册新用户账号，响应登录服务器转发的注册账号请求
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

using System;
using C2SProtocol.User;
using MasterServer.DataCollection;
using Photon.SocketServer;
using Protocol;
using S2SProtocol.Common;

namespace MasterServer.LoginServerRequest
{
    /// <summary>
    /// 类型：类
    /// 名称：Regist
    /// 作者：taixihuase
    /// 作用：响应注册请求
    /// 编写日期：2016/3/17
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
        public static void OnRequest(OperationRequest operationRequest, SendParameters sendParameters,
            PeerToSubServer peer)
        {
            switch ((S2SOpCode)Enum.Parse(typeof(S2SOpCode), operationRequest.OperationCode.ToString()))
            {
                case S2SOpCode.ClientRegistCheck:
                    TryCheck(operationRequest, sendParameters, peer);
                    break;
                case S2SOpCode.ClientApplyForCaptcha:
                    TryApplyForCaptcha(operationRequest, sendParameters, peer);
                    break;
                case S2SOpCode.ClientRegist:
                    TryRegist(operationRequest, sendParameters, peer);
                    break;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryApplyForCaptcha
        /// 作者：taixihuase
        /// 作用：尝试为指定邮箱获取验证码并发送
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        /// <param name="peer"></param>
        private static void TryApplyForCaptcha(OperationRequest operationRequest, SendParameters sendParameters,
            PeerToSubServer peer)
        {
            var email = operationRequest.Parameters[(byte) S2SParaCode.ClientEmail] as string;
            if (CheckEmail(email) == RegistInfo.CheckStatus.EmailNotExist)
            {
                string captcha = CaptchaCollection.Instance.CreateRandomCaptcha(6);
                CaptchaCollection.Instance.UpdateCaptchaWithEmail(email, captcha, ServerSettings.Default.CaptchaLifeTime);
                peer.SendOperationResponseToSub(
                    S2SOpCode.ClientApplyForCaptcha,
                    S2SRetCode.Success,
                    S2SParaCode.ClientSocket,
                    operationRequest.Parameters[(byte)S2SParaCode.ClientSocket],
                    "验证码已发送至指定邮箱，若未收到，请稍后重新获取");
                return;
            }
            peer.SendOperationResponseToSub(
                S2SOpCode.ClientApplyForCaptcha,
                S2SRetCode.Failure,
                S2SParaCode.ClientSocket,
                operationRequest.Parameters[(byte)S2SParaCode.ClientSocket],
                "该邮箱已被注册");
        }


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
        private static void TryCheck(OperationRequest operationRequest, SendParameters sendParameters,
            PeerToSubServer peer)
        {
            var email = operationRequest.Parameters[(byte) S2SParaCode.ClientEmail] as string;
            if (CheckEmail(email) == RegistInfo.CheckStatus.EmailNotExist)
            {
                peer.SendOperationResponseToSub(
                    S2SOpCode.ClientRegistCheck,
                    S2SRetCode.Success,
                    S2SParaCode.ClientSocket,
                    operationRequest.Parameters[(byte) S2SParaCode.ClientSocket],
                    "邮箱未被注册，可以使用");
                return;
            }
            peer.SendOperationResponseToSub(
                S2SOpCode.ClientRegistCheck,
                S2SRetCode.Failure,
                S2SParaCode.ClientSocket,
                operationRequest.Parameters[(byte) S2SParaCode.ClientSocket],
                "该邮箱已被注册");
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
            PeerToSubServer peer)
        {
            var regist =
                Serialization.Deserialize<RegistInfo>(operationRequest.Parameters[(byte) S2SParaCode.ClientRegist]);
            if (CheckEmail(regist.Email) == RegistInfo.CheckStatus.EmailNotExist)
            {
                var check = CheckCaptcha(regist.Email, regist.Captcha);
                PeerToSubServer.Log.Debug(CaptchaCollection.Instance.GetCaptcha(regist.Email));
                switch (check)
                {
                    case RegistInfo.CheckStatus.CaptchaMatched:
                        CaptchaCollection.Instance.RemoveCaptchWithEmail(regist.Email, regist.Captcha);
                        peer.SendOperationResponseToSub(
                            S2SOpCode.ClientRegist,
                            S2SRetCode.Success,
                            S2SParaCode.ClientSocket,
                            operationRequest.Parameters[(byte) S2SParaCode.ClientSocket],
                            "新账号注册成功");
                        return;
                    case RegistInfo.CheckStatus.CaptchaNotMatched:
                        peer.SendOperationResponseToSub(
                            S2SOpCode.ClientRegist,
                            S2SRetCode.Failure,
                            S2SParaCode.ClientSocket,
                            operationRequest.Parameters[(byte) S2SParaCode.ClientSocket],
                            "验证码不正确");
                        return;
                    case RegistInfo.CheckStatus.CaptchaNotObtained:
                        peer.SendOperationResponseToSub(
                            S2SOpCode.ClientRegist,
                            S2SRetCode.Failure,
                            S2SParaCode.ClientSocket,
                            operationRequest.Parameters[(byte) S2SParaCode.ClientSocket],
                            "未获取验证码或验证码已过期");
                        return;
                }
            }
            peer.SendOperationResponseToSub(
                S2SOpCode.ClientRegist,
                S2SRetCode.Failure,
                S2SParaCode.ClientSocket,
                operationRequest.Parameters[(byte) S2SParaCode.ClientSocket],
                "该邮箱已被注册");
        }

        /// <summary>
        /// 类型：方法
        /// 名称：CheckEmail
        /// 作者：taixihuase
        /// 作用：验证邮箱是否已被注册
        /// 编写日期：2016/3/16
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private static RegistInfo.CheckStatus CheckEmail(string email)
        {
            return UserCollection.Instance.IsUserExist(email);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：CheckEmail
        /// 作者：taixihuase
        /// 作用：验证发送到目标邮箱的验证码是否匹配
        /// 编写日期：2016/3/16
        /// </summary>
        /// <param name="email"></param>
        /// <param name="captcha"></param>
        /// <returns></returns>
        private static RegistInfo.CheckStatus CheckCaptcha(string email, string captcha)
        {
            return CaptchaCollection.Instance.IsCaptchaMatch(email, captcha);
        }
    }
}
