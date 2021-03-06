﻿//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：Level.cs
//
// 文件功能描述：
//
// Level 实体
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

namespace DatabaseServer.Entity.Models
{
    /// <summary>
    /// 类型：类
    /// 名称：Level
    /// 作者：taixihuase
    /// 作用：Level 实体
    /// 编写日期：2016/4/4
    /// </summary>
    public class Level
    {
        public int Id { get; set; }

        public int UpgradeExp { get; set; }

        public int WinExp { get; set; }

        public int LoseExp { get; set; }
    }
}
