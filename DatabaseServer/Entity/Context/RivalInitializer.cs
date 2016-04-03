//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：RivalInitializer.cs
//
// 文件功能描述：
//
// Rival 数据库初始化器
//
// 创建标识：taixihuase 20160403
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
using System.Data.Entity;
using System.Data.Entity.Migrations;
using DatabaseServer.Entity.Models;

namespace DatabaseServer.Entity.Context
{
    /// <summary>
    /// 类型：类
    /// 名称：RivalInitializer
    /// 作者：taixihuase
    /// 作用：重建 Rival 数据库时进行初始化
    /// 编写日期：2016/4/3
    /// </summary>
    public class RivalInitializer:DropCreateDatabaseIfModelChanges<RivalContext>
    {
        /// <summary>
        /// 类型：方法
        /// 名称：Seed
        /// 作者：taixihuase
        /// 作用：设定初始化种子，仅第一次建立数据库时调用该方法，因迁移引发的重建数据库操作不会调用该方法
        /// 编写日期：2016/4/3
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(RivalContext context)
        {

        }
    }
}
