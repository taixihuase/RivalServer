//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：CardPackMap.cs
//
// 文件功能描述：
//
// CardPack 映射
//
// 创建标识：taixihuase 20160411
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
    /// 名称：CardPackMap
    /// 作者：taixihuase
    /// 作用：映射 CardPack 实体到 CardPack 表
    /// 编写日期：2016/4/11
    /// </summary>
    public class CardPackMap : EntityTypeConfiguration<CardPack>
    {
        /// <summary>
        /// 类型：方法
        /// 名称：CardPackMap
        /// 作者：taixihuase
        /// 作用：配置 CardPack 实体映射
        /// 编写日期：2016/4/11
        /// </summary>
        public CardPackMap()
        {
            ToTable("CardPack");

            HasKey(t => t.Id);

            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).HasColumnName("CardPackId");

            Property(t => t.Type).HasColumnName("Type");

            Property(t => t.Name).IsRequired().HasMaxLength(10).HasColumnName("Name");

            Property(t => t.Price).HasColumnName("Price");
        }
    }
}
