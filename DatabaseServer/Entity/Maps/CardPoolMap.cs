//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：CardPoolMap.cs
//
// 文件功能描述：
//
// CardPool 映射
//
// 创建标识：taixihuase 20160407
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
    /// 名称：CardPoolMap
    /// 作者：taixihuase
    /// 作用：映射 CardPool 实体到 CardPool 表
    /// 编写日期：2016/4/7
    /// </summary>
    public class CardPoolMap : EntityTypeConfiguration<CardPool>
    {
        /// <summary>
        /// 类型：方法
        /// 名称：CardPoolMap
        /// 作者：taixihuase
        /// 作用：配置 CardPool 实体映射
        /// 编写日期：2016/4/7
        /// </summary>
        public CardPoolMap()
        {
            ToTable("CardPool");

            HasKey(t => t.Id);

            Property(t => t.Id).HasColumnName("CardPoolId");

            HasMany(t => t.Cards).WithMany(t => t.CardPools).Map(m =>
            {
                m.ToTable("CardPool_Card_Mapping");
                m.MapLeftKey("CardPoolId");
                m.MapRightKey("CardId");
            });
        }
    }
}
