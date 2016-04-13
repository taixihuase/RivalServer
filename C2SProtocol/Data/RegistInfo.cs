//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：RegistInfo.cs
//
// 文件功能描述：
//
// 注册新账号信息
//
// 创建标识：taixihuase 20160315
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

using ProtoBuf;

namespace C2SProtocol.Data
{
    /// <summary>
    /// 类型：类
    /// 名称：RegistInfo
    /// 作者：taixihuase
    /// 作用：注册账号信息
    /// 编写日期：2016/3/15
    /// </summary>
    [ProtoContract]
    public class RegistInfo
    {
        /// <summary>
        /// 注册邮箱
        /// </summary>
        [ProtoMember(1)]
        public string Email { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [ProtoMember(2)]
        public string Nickname { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [ProtoMember(3)]
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [ProtoMember(4)]
        public string Captcha { get; set; }

        /// <summary>
        /// 注册校验状态
        /// </summary>
        [ProtoMember(5)]
        public CheckStatus Status { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：CheckStatus
        /// 作者：taixihuase
        /// 作用：校验情况枚举值
        /// 编写日期：2016/3/17
        /// </summary>
        public enum CheckStatus
        {
            Unchecked,
            EmailExist,
            EmailNotExist,
            CaptchaNotObtained,
            CaptchaMatched,
            CaptchaNotMatched,
            Ok,
        }

        /// <summary>
        /// 类型：方法
        /// 名称：RegistInfo
        /// 作者：taixihuase
        /// 作用：通过邮箱、昵称、密码和验证码构造注册数据
        /// 编写日期：2016/3/15
        /// </summary>
        /// <param name="email"></param>
        /// <param name="nickname"></param>
        /// <param name="password"></param>
        /// <param name="captcha"></param>
        public RegistInfo(string email, string nickname, string password, string captcha)
        {
            Email = email;
            Nickname = nickname;
            Password = password;
            Captcha = captcha;
            Status = CheckStatus.Unchecked;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：RegistInfo
        /// 作者：taixihuase
        /// 作用：默认构造注册数据
        /// 编写日期：2016/3/15
        /// </summary>
        public RegistInfo()
        {
            Email = string.Empty;
            Nickname = string.Empty;
            Password = string.Empty;
            Captcha = string.Empty;
            Status = CheckStatus.Unchecked;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SetCheckStatus
        /// 作者：taixihuase
        /// 作用：设置检测状态
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="status"></param>
        public void SetCheckStatus(CheckStatus status = CheckStatus.Unchecked)
        {
            Status = status;
        }
    }
}
