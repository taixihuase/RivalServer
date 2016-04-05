//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：CombatAttributeMap.cs
//
// 文件功能描述：
//
// CombatAttribute 映射
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
using C2SProtocol.Entity.Models;

namespace DatabaseServer.Entity.Maps
{
    /// <summary>
    /// 类型：类
    /// 名称：CombatAttributeMap
    /// 作者：taixihuase
    /// 作用：映射 CombatAttribute 复杂类型到表
    /// 编写日期：2016/4/4
    /// </summary>
    public class CombatAttributeMap : ComplexTypeConfiguration<CombatAttribute>
    {
        /// <summary>
        /// 类型：方法
        /// 名称：CombatAttributeMap
        /// 作者：taixihuase
        /// 作用：配置 CombatAttribute 复杂类型映射
        /// 编写日期：2016/4/4
        /// </summary>
        public CombatAttributeMap()
        {
            Property(t => t.Attack).HasColumnName("Attack");

            Property(t => t.AttackAttribute).IsRequired().HasMaxLength(1).HasColumnName("AttackAttribute").HasColumnType("nchar");

            Property(t => t.Shield).HasColumnName("Shield");

            Property(t => t.ShieldAttribute).IsRequired().HasMaxLength(1).HasColumnName("ShieldAttribute").HasColumnType("nchar");
        }
    }
}
