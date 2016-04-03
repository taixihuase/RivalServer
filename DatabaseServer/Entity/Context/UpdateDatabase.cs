//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：UpdateDatabase.cs
//
// 文件功能描述：
//
// 启动服务端程序或数据库更新控制台时自动进行最新的迁移
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
using System.Linq;
using DatabaseServer.Entity.Models;
using DatabaseServer.Migrations;

namespace DatabaseServer.Entity.Context
{
    /// <summary>
    /// 类型：类
    /// 名称：UpdateDatabase
    /// 作者：taixihuase
    /// 作用：更新数据库，只在每次启动程序时调用一次迁移
    /// 编写日期：2016/4/3
    /// </summary>
    public static class UpdateDatabase
    {
        /// <summary>
        /// 类型：方法
        /// 名称：Main
        /// 作者：taixihuase
        /// 作用：控制台下启动所用主函数，进行数据库自动迁移
        /// 编写日期：2016/4/3
        /// </summary>
        private static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RivalContext, Configuration>());
            using (RivalContext db = new RivalContext())
            {
                db.Refresh();
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Migration
        /// 作者：taixihuase
        /// 作用：进行数据库迁移
        /// 编写日期：2016/4/3
        /// </summary>
        public static void Migration()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RivalContext, Configuration>());
            using (RivalContext db = new RivalContext())
            {
                db.Refresh();
            }
        }
    }
}
