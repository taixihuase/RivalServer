﻿//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：CardEffectMap.cs
//
// 文件功能描述：
//
// CardEffect 映射
//
// 创建标识：taixihuase 20160405
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
    /// 名称：CardEffectMap
    /// 作者：taixihuase
    /// 作用：映射 CardEffect 实体到 CardEffect 表
    /// 编写日期：2016/4/5
    /// </summary>
    public class CardEffectMap : EntityTypeConfiguration<CardEffect>
    {
        /// <summary>
        /// 类型：方法
        /// 名称：CardEffectMap
        /// 作者：taixihuase
        /// 作用：配置 CardEffect 实体映射
        /// 编写日期：2016/4/5
        /// </summary>
        public CardEffectMap()
        {
            ToTable("CardEffect");

            HasKey(t => t.Id);

            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).HasColumnName("CardEffectId");

            Property(t => t.Owner).HasColumnName("Owner");

            Property(t => t.Condition).HasColumnName("Condition");

            Property(t => t.Description).IsRequired().HasMaxLength(30).HasColumnName("Description");

            Property(t => t.Value).HasColumnName("Value");
        }
    }
}
