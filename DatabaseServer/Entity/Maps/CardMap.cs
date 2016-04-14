//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：CardMap.cs
//
// 文件功能描述：
//
// Card 映射
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

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DatabaseServer.Entity.Models;

namespace DatabaseServer.Entity.Maps
{
    /// <summary>
    /// 类型：类
    /// 名称：CardMap
    /// 作者：taixihuase
    /// 作用：映射 Card 实体到 Card 表
    /// 编写日期：2016/4/4
    /// </summary>
    public class CardMap : EntityTypeConfiguration<Card>
    {
        /// <summary>
        /// 类型：方法
        /// 名称：CardMap
        /// 作者：taixihuase
        /// 作用：配置 Card 实体映射
        /// 编写日期：2016/4/4
        /// </summary>
        public CardMap()
        {
            HasKey(t => t.Id);

            Map<Card>(m =>
            {             
                Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).HasColumnName("CardId");
                Property(t => t.Name).IsRequired().HasMaxLength(10).HasColumnName("Name");
                Property(t => t.Type).HasColumnName("Type");
                Property(t => t.MainAttribute).HasColumnName("MainAttribute");
                Property(t => t.Rarity).HasColumnName("Rarity");
                HasMany(t => t.CardEffects).WithMany().Map(ef =>
                {
                    ef.ToTable("Card_Effect_Mapping");
                    ef.MapLeftKey("CardId");
                    ef.MapRightKey("EffectId");
                });
                HasOptional(t => t.CardPack).WithMany(t => t.Cards).HasForeignKey(t => t.CardPackId).WillCascadeOnDelete(false);
                Property(t => t.CardPackId).HasColumnName("CardPackId");
            }).ToTable("Card");

            Map<SummonCard>(m =>
            {
                m.ToTable("SummonCard");
                Property(t => t.MainAttribute).HasColumnName("MainAttribute");
            });

            Map<SpellCard>(m =>
            {
                m.ToTable("SpellCard");
            });

            Map<MonsterCard>(m =>
            {
                m.ToTable("MonsterCard");
                m.Property(t => t.Flexibility).HasColumnName("Flexibility");
                m.Property(t => t.Range).HasColumnName("Range");
                m.Property(t => t.CombatAttribute.AttackAttribute).HasColumnName("MonsterAttackAttribute");
                m.Property(t => t.CombatAttribute.Attack).HasColumnName("MonsterAttack");
                m.Property(t => t.CombatAttribute.ShieldAttribute).HasColumnName("MonsterShieldAttribute");
                m.Property(t => t.CombatAttribute.Shield).HasColumnName("MonsterShield");
            });

            Map<LordCard>(m =>
            {
                m.ToTable("LordCard");
                m.Property(t => t.CombatAttribute.AttackAttribute).HasColumnName("LordAttackAttribute");
                m.Property(t => t.CombatAttribute.Attack).HasColumnName("LordAttack");
                m.Property(t => t.CombatAttribute.ShieldAttribute).HasColumnName("LordShieldAttribute");
                m.Property(t => t.CombatAttribute.Shield).HasColumnName("LordShield");
            });
        }
    }
}
