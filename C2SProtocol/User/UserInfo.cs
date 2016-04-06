//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：UserInfo.cs
//
// 文件功能描述：
//
// 记录账号基本信息
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
//----------------------------------------------------------------------------------------------------------

using ProtoBuf;
using static System.String;

namespace C2SProtocol.User
{
    /// <summary>
    /// 类型：类
    /// 名称：UserInfo
    /// 作者：taixihuase
    /// 作用：记录账户基本信息
    /// 编写日期：2016/3/17
    /// </summary>
    [ProtoContract]
    public class UserInfo
    {
        [ProtoMember(1, IsRequired = true)]
        public uint UniqueId { get; set; }

        [ProtoMember(2, IsRequired = true)]
        public string Account { get; set; }

        [ProtoMember(3, IsRequired = true)]
        public string Nickname { get; set; }

        [ProtoMember(4, IsRequired = true)]
        public UserStatus Status { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：UserStatus
        /// 作者：taixihuase
        /// 作用：用户基本状态枚举
        /// 编写日期：2016/3/17
        /// </summary>
        public enum UserStatus
        {
            Default,
            Offline,
            Loginning, 
            Idle,
            Battling,
        }

        /// <summary>
        /// 类型：方法
        /// 名称：UserInfo
        /// 作者：taixihuase
        /// 作用：通过编号、邮箱、昵称构造用户基本数据
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="id"></param>
        /// <param name="account"></param>
        /// <param name="nickname"></param>
        /// <param name="status"></param>
        public UserInfo(uint id , string account, string nickname, UserStatus status)
        {
            UniqueId = id;
            Account = account;
            Nickname = nickname;
            Status = status;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：UserInfo
        /// 作者：taixihuase
        /// 作用：默认构造用户数据
        /// 编写日期：2016/3/17
        /// </summary>
        public UserInfo()
        {
            UniqueId = uint.MinValue;
            Account = Empty;
            Nickname = Empty;
            Status = UserStatus.Offline;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SetUserStatus
        /// 作者：taixihuase
        /// 作用：设置用户状态
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="status"></param>
        public void SetUserStatus(UserStatus status = UserStatus.Default)
        {
            Status = status;
        }
    }
}
