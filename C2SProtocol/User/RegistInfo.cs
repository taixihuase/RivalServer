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
using static System.String;

namespace C2SProtocol.User
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
        [ProtoMember(1, IsRequired = true)]
        public string Email { get; set; }

        [ProtoMember(2, IsRequired = true)]
        public string Nickname { get; set; }

        [ProtoMember(3, IsRequired = true)]
        public string Password { get; set; }

        [ProtoMember(4, IsRequired = true)]
        public string Captcha { get; set; }

        [ProtoMember(5, IsRequired = true)]
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
            Email = Empty;
            Nickname = Empty;
            Password = Empty;
            Captcha = Empty;
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
