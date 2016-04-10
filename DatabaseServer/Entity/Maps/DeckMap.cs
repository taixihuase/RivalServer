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

            HasRequired(t => t.Player).WithMany(t => t.Decks).WillCascadeOnDelete();

            HasOptional(t => t.LordCard).WithMany().HasForeignKey(t => t.LordCardId);

            Property(t => t.LordCardId).HasColumnName("CardId1");

            #region CardId Mapping

            HasOptional(t => t.Card2).WithMany().HasForeignKey(t => t.CardId2);
            HasOptional(t => t.Card3).WithMany().HasForeignKey(t => t.CardId3);
            HasOptional(t => t.Card4).WithMany().HasForeignKey(t => t.CardId4);
            HasOptional(t => t.Card5).WithMany().HasForeignKey(t => t.CardId5);
            HasOptional(t => t.Card6).WithMany().HasForeignKey(t => t.CardId6);
            HasOptional(t => t.Card7).WithMany().HasForeignKey(t => t.CardId7);
            HasOptional(t => t.Card8).WithMany().HasForeignKey(t => t.CardId8);
            HasOptional(t => t.Card9).WithMany().HasForeignKey(t => t.CardId9);
            HasOptional(t => t.Card10).WithMany().HasForeignKey(t => t.CardId10);
            HasOptional(t => t.Card11).WithMany().HasForeignKey(t => t.CardId11);
            HasOptional(t => t.Card12).WithMany().HasForeignKey(t => t.CardId12);
            HasOptional(t => t.Card13).WithMany().HasForeignKey(t => t.CardId13);
            HasOptional(t => t.Card14).WithMany().HasForeignKey(t => t.CardId14);
            HasOptional(t => t.Card15).WithMany().HasForeignKey(t => t.CardId15);
            HasOptional(t => t.Card16).WithMany().HasForeignKey(t => t.CardId16);
            HasOptional(t => t.Card17).WithMany().HasForeignKey(t => t.CardId17);
            HasOptional(t => t.Card18).WithMany().HasForeignKey(t => t.CardId18);
            HasOptional(t => t.Card19).WithMany().HasForeignKey(t => t.CardId19);
            HasOptional(t => t.Card20).WithMany().HasForeignKey(t => t.CardId20);
            HasOptional(t => t.Card21).WithMany().HasForeignKey(t => t.CardId21);
            HasOptional(t => t.Card22).WithMany().HasForeignKey(t => t.CardId22);
            HasOptional(t => t.Card23).WithMany().HasForeignKey(t => t.CardId23);
            HasOptional(t => t.Card24).WithMany().HasForeignKey(t => t.CardId24);
            HasOptional(t => t.Card25).WithMany().HasForeignKey(t => t.CardId25);

            #endregion
        }
    }
}
