﻿//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：Avatar.cs
//
// 文件功能描述：
//
// Avatar 实体
//
// 创建标识：taixihuase 20160404
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using C2SProtocol.Data;

namespace DatabaseServer.Entity.Models
{
    /// <summary>
    /// 类型：类
    /// 名称：Avatar
    /// 作者：taixihuase
    /// 作用：Avatar 实体
    /// 编写日期：2016/4/4
    /// </summary>
    public class Avatar
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 类型：方法
        /// 名称：ToAvatarInfo
        /// 作者：taixihuase
        /// 作用：转换为 AvatarInfo 对象
        /// 编写日期：2016/4/14
        /// </summary>
        /// <returns></returns>
        public AvatarInfo ToAvatarInfo()
        {
            return new AvatarInfo {AvatarId = Id, Name = Name};
        }
    }
}
