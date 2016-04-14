//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：Configuration.cs
//
// 文件功能描述：
//
// 进行数据库配置
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

using System.Data.Entity.Migrations;
using DatabaseServer.Entity.Context;

namespace DatabaseServer.Migrations
{
    /// <summary>
    /// 类型：类
    /// 名称：Configuration
    /// 作者：taixihuase
    /// 作用：数据库配置类
    /// 编写日期：2016/4/3
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<RivalContext>
    {
        /// <summary>
        /// 类型：方法
        /// 名称：Configuration
        /// 作者：taixihuase
        /// 作用：默认构造数据库配置类
        /// 编写日期：2016/4/3
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DatabaseServer.Entity.Context.RivalContext";
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Seed
        /// 作者：taixihuase
        /// 作用：每次迁移时，初始化数据库上下文，仅手动迁移会调用该方法
        /// 编写日期：2016/4/3
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(RivalContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
