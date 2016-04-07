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

            HasRequired(t => t.CardPool).WithMany(t => t.Decks).WillCascadeOnDelete();

            Property(t => t.LordCardId).HasColumnName("CardId1").IsOptional();

            #region CardId Mapping

            Property(t => t.CardId2).IsOptional();
            Property(t => t.CardId3).IsOptional();
            Property(t => t.CardId4).IsOptional();
            Property(t => t.CardId5).IsOptional();
            Property(t => t.CardId6).IsOptional();
            Property(t => t.CardId7).IsOptional();
            Property(t => t.CardId8).IsOptional();
            Property(t => t.CardId9).IsOptional();
            Property(t => t.CardId10).IsOptional();
            Property(t => t.CardId11).IsOptional();
            Property(t => t.CardId12).IsOptional();
            Property(t => t.CardId13).IsOptional();
            Property(t => t.CardId14).IsOptional();
            Property(t => t.CardId15).IsOptional();
            Property(t => t.CardId16).IsOptional();
            Property(t => t.CardId17).IsOptional();
            Property(t => t.CardId18).IsOptional();
            Property(t => t.CardId19).IsOptional();
            Property(t => t.CardId20).IsOptional();
            Property(t => t.CardId21).IsOptional();
            Property(t => t.CardId22).IsOptional();
            Property(t => t.CardId23).IsOptional();
            Property(t => t.CardId24).IsOptional();
            Property(t => t.CardId25).IsOptional();

            #endregion
        }
    }
}
