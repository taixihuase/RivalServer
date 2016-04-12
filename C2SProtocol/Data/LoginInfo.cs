//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：LoginInfo.cs
//
// 文件功能描述：
//
// 账号登录参数，存放登录操作的账号及密码
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
    /// 名称：LoginInfo
    /// 作者：taixihuase
    /// 作用：记录登录数据并用于传输
    /// 编写日期：2016/3/15
    /// </summary>
    [ProtoContract]
    public class LoginInfo
    {
        [ProtoMember(1)]
        public string Account { get; set; }

        [ProtoMember(2)]
        public string Password { get; set; }

        [ProtoMember(3)]
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
            AccountAlreadyLogged,
            AccountNotExist,
            PasswordWrong,
            Ok,
        }

        /// <summary>
        /// 类型：方法
        /// 名称：LoginInfo
        /// 作者：taixihuase
        /// 作用：通过账号名和密码构造登陆数据
        /// 编写日期：2016/3/15
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        public LoginInfo(string account, string password)
        {
            Account = account;
            Password = password;
            Status = CheckStatus.Unchecked;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：LoginInfo
        /// 作者：taixihuase
        /// 作用：默认构造登陆数据
        /// 编写日期：2016/3/15
        /// </summary>
        public LoginInfo()
        {
            Account = string.Empty;
            Password = string.Empty;
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
