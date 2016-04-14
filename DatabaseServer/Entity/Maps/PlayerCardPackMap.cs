//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：PlayerCardPackMap.cs
//
// 文件功能描述：
//
// PlayerCardPack 映射
//
// 创建标识：taixihuase 20160414
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
    /// 名称：PlayerCardPackMap
    /// 作者：taixihuase
    /// 作用：映射 PlayerCardPack 实体到 PlayerCardPack 表
    /// 编写日期：2016/4/14
    /// </summary>
    public class PlayerCardPackMap : EntityTypeConfiguration<PlayerCardPack>
    {
        /// <summary>
        /// 类型：方法
        /// 名称：PlayerCardPackMap
        /// 作者：taixihuase
        /// 作用：配置 PlayerCardPack 实体
        /// 编写日期：2016/4/14
        /// </summary>
        public PlayerCardPackMap()
        {
            ToTable("Player_CardPack_Mapping");

            HasKey(t => new {t.Id, t.CardPackId});

            Property(t => t.Id).HasColumnName("UserId");

            Property(t => t.CardPackId).HasColumnName("CardPackId");

            HasRequired(t => t.Player).WithMany(t => t.PlayerCardPacks).HasForeignKey(t => t.Id);

            HasRequired(t => t.CardPack).WithMany().HasForeignKey(t => t.CardPackId);
        }
    }
}
