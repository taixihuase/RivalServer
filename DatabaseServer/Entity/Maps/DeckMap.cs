//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：DeckMap.cs
//
// 文件功能描述：
//
// Deck 映射
//
// 创建标识：taixihuase 20160408
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
    /// 名称：DeckMap
    /// 作者：taixihuase
    /// 作用：映射 Deck 实体到 Deck 表
    /// 编写日期：2016/4/8
    /// </summary>
    public class DeckMap : EntityTypeConfiguration<Deck>
    {
        /// <summary>
        /// 类型：方法
        /// 名称：DeckMap
        /// 作者：taixihuase
        /// 作用：配置 Deck 实体映射
        /// 编写日期：2016/4/8
        /// </summary>
        public DeckMap()
        {
            ToTable("Deck");

            HasKey(t => new {t.Id, t.Index});

            Property(t => t.Id).HasColumnName("UserId");

            Property(t => t.Index).HasColumnName("DeckIndex");

            Property(t => t.Name).HasColumnName("Name").HasMaxLength(8);

            Property(t => t.IsDefault).HasColumnName("IsDefault");

            HasRequired(t => t.Player).WithMany(t => t.Decks).HasForeignKey(t => t.Id).WillCascadeOnDelete();

            HasOptional(t => t.LordCard).WithMany().HasForeignKey(t => t.LordCardId);

            Property(t => t.LordCardId).HasColumnName("LordCardId");

            HasMany(t => t.SummonCards).WithMany().Map(m =>
            {
                m.ToTable("Deck_Card_Mapping");
                m.MapLeftKey("UserId", "DeckIndex");
                m.MapRightKey("CardId");
            });

            Property(t => t.CardCount).HasColumnName("CardCount");
        }
    }
}
