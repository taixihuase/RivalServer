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

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DatabaseServer.Entity.Models;

namespace DatabaseServer.Entity.Maps
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
            HasKey(t => t.Id);

            Map<User>(m =>
            {
                Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("UserId");

                Property(t => t.Account).IsRequired().HasMaxLength(30).HasColumnName("Account");

                Property(t => t.Nickname).IsRequired().HasMaxLength(10).HasColumnName("Nickname");

                Property(t => t.Password).IsRequired().HasMaxLength(30).HasColumnName("Password");

                Property(t => t.RegistTime).IsRequired().HasColumnName("RegistTime");

                HasMany(t => t.Friends).WithMany().Map(f =>
                {
                    f.ToTable("Friend_Mapping");
                    f.MapLeftKey("UserId_Self");
                    f.MapRightKey("UserId_Friend");
                });
            }).ToTable("User");

           Map<Player>(m =>
           {
               m.ToTable("Player");
           });
        }
    }
}
