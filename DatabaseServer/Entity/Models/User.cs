//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：User.cs
//
// 文件功能描述：
//
// User 实体
//
// 创建标识：taixihuase 20160322
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using C2SProtocol.Data;

namespace DatabaseServer.Entity.Models
{
    /// <summary>
    /// 类型：类
    /// 名称：User
    /// 作者：taixihuase
    /// 作用：User 实体
    /// 编写日期：2016/3/22
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        public string Account { get; set; }

        public string Nickname { get; set; }

        public string Password { get; set; }

        public DateTime RegistTime { get; set; }

        public virtual ICollection<User> Friends { get; set; } = new List<User>();

        /// <summary>
        /// 类型：方法
        /// 名称：ToUserInfo
        /// 作者：taixihuase
        /// 作用：转换为 UserInfo 对象
        /// 编写日期：2016/4/14
        /// </summary>
        /// <returns></returns>
        public UserInfo ToUserInfo()
        {
            return new UserInfo(Id, Account, Nickname, UserInfo.UserStatus.Default);
        }
    }
}
