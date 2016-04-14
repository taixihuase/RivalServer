//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：TitleMap.cs
//
// 文件功能描述：
//
// Title 映射
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

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DatabaseServer.Entity.Models;

namespace DatabaseServer.Entity.Maps
{
    /// <summary>
    /// 类型：类
    /// 名称：TitleMap
    /// 作者：taixihuase
    /// 作用：映射 Title 实体到 Title 表
    /// 编写日期：2016/4/4
    /// </summary>
    public class TitleMap : EntityTypeConfiguration<Title>
    {
        /// <summary>
        /// 类型：方法
        /// 名称：TitleMap
        /// 作者：taixihuase
        /// 作用：配置 Title 实体映射
        /// 编写日期：2016/4/4
        /// </summary>
        public TitleMap()
        {
            ToTable("Title");

            HasKey(t => t.Id);

            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).HasColumnName("TitleId");

            Property(t => t.Name).IsRequired().HasMaxLength(10).HasColumnName("Name");
        }
    }
}
