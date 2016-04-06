//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：AvatarMap.cs
//
// 文件功能描述：
//
// Avatar 映射
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
    /// 名称：AvatarMap
    /// 作者：taixihuase
    /// 作用：映射 Avatar 实体到 Avatar 表
    /// 编写日期：2016/4/4
    /// </summary>
    public class AvatarMap : EntityTypeConfiguration<Avatar>
    {
        /// <summary>
        /// 类型：方法
        /// 名称：AvatarMap
        /// 作者：taixihuase
        /// 作用：配置 Avatar 实体映射
        /// 编写日期：2016/4/4
        /// </summary>
        public AvatarMap()
        {
            ToTable("Avatar");

            HasKey(t => t.Id);

            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("AvatarId");

            Property(t => t.Name).IsRequired().HasMaxLength(10).HasColumnName("Name");
        }
    }
}
