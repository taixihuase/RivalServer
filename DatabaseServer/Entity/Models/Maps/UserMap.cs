//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：UserMap.cs
//
// 文件功能描述：
//
// User 映射
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

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DatabaseServer.Entity.Models.Maps
{
    /// <summary>
    /// 类型：类
    /// 名称：UserMap
    /// 作者：taixihuase
    /// 作用：映射 User 实体到 User 表
    /// 编写日期：2016/3/22
    /// </summary>
    public class UserMap : EntityTypeConfiguration<User>
    {
        /// <summary>
        /// 类型：方法
        /// 名称：UserMap
        /// 作者：taixihuase
        /// 作用：配置 User 实体映射
        /// 编写日期：2016/3/22
        /// </summary>
        public UserMap()
        {
            ToTable("User");

            HasKey(t => t.Id)
                .Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("UserId");

            Property(t => t.Account).HasMaxLength(30).IsRequired().HasColumnName("Account");

            Property(t => t.Nickname).HasMaxLength(10).IsRequired().HasColumnName("Nickname");

            Property(t => t.Password).HasMaxLength(30).IsRequired().HasColumnName("Password");

            Property(t => t.RegistTime).IsRequired().HasColumnName("RegistTime");
        }
    }
}
