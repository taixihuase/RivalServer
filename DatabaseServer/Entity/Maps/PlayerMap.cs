//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：PlayerMap.cs
//
// 文件功能描述：
//
// Player 映射
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

using System.Data.Entity.ModelConfiguration;
using DatabaseServer.Entity.Models;

namespace DatabaseServer.Entity.Maps
{
    /// <summary>
    /// 类型：类
    /// 名称：PlayerMap
    /// 作者：taixihuase
    /// 作用：映射 Player 实体到 Player 表
    /// 编写日期：2016/4/4
    /// </summary>
    public class PlayerMap : EntityTypeConfiguration<Player>
    {
        /// <summary>
        /// 类型：方法
        /// 名称：PlayerMap
        /// 作者：taixihuase
        /// 作用：配置 Play 实体映射
        /// 编写日期：2016/4/4
        /// </summary>
        public PlayerMap()
        {
            Property(t => t.LevelId).HasColumnName("Level");

            HasRequired(t => t.Level).WithMany(t => t.Players).WillCascadeOnDelete(false);

            Property(t => t.Experience).HasColumnName("Experience");

            Property(t => t.DefaultAvatarId).HasColumnName("AvatarId");

            HasRequired(t => t.DefaultAvatar).WithMany().WillCascadeOnDelete(false);

            HasMany(t => t.Avatars).WithMany().Map(m =>
            {
                m.ToTable("Player_Avatar_Mapping");
                m.MapLeftKey("PlayerId");
                m.MapRightKey("AvatarId");
            });

            Property(t => t.DefaultTitleId).HasColumnName("TitleId");

            HasRequired(t => t.DefaultTitle).WithMany().WillCascadeOnDelete(false);

            HasMany(t => t.Titles).WithMany().Map(m =>
            {
                m.ToTable("Player_Title_Mapping");
                m.MapLeftKey("PlayerId");
                m.MapRightKey("TitleId");
            });

            Property(t => t.VirtualCurrency).HasColumnName("Currency");

            Property(t => t.Win).HasColumnName("Win");

            Property(t => t.Total).HasColumnName("Total");

            HasMany(t => t.Cards).WithMany().Map(m =>
            {
                m.ToTable("CardPool_Mapping");
                m.MapLeftKey("UserId");
                m.MapRightKey("CardId");
            });
        }
    }
}
