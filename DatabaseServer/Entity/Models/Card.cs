//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：Card.cs
//
// 文件功能描述：
//
// Card 实体
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

using System.Collections.Generic;
using C2SProtocol.Data;

namespace DatabaseServer.Entity.Models
{
    /// <summary>
    /// 类型：类
    /// 名称：Card
    /// 作者：taixihuase
    /// 作用：Card 实体
    /// 编写日期：2016/4/4
    /// </summary>
    public class Card
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CardInfo.CardType? Type { get; set; }

        public CardInfo.AttributeType? MainAttribute { get; set; }

        public CardInfo.RarityType? Rarity { get; set; }

        public virtual ICollection<CardEffect> CardEffects { get; set; } = new List<CardEffect>();

        public virtual CardPack CardPack { get; set; }

        public int? CardPackId { get; set; }

        /// <summary>
        /// 类型：方法
        /// 名称：ToCardInfo
        /// 作者：taixihuase
        /// 作用：转换为 CardInfo 对象
        /// 编写日期：2016/4/14
        /// </summary>
        /// <returns></returns>
        public CardInfo ToCardInfo()
        {
            CardInfo card = new CardInfo
            {
                CardId = Id,
                Name = Name,
                Type = Type,
                MainAttribute = MainAttribute,
                Rarity = Rarity
            };
            foreach (var effect in CardEffects)
            {
                card.CardEffect.Add(effect.Id);
            }
            try
            {
                var l = this as LordCard;
                if (l != null)
                {
                    card.AttackAttribute = l.CombatAttribute.AttackAttribute;
                    card.Attack = l.CombatAttribute.Attack;
                    card.ShieldAttribute = l.CombatAttribute.ShieldAttribute;
                    card.Shield = l.CombatAttribute.Shield;
                }
                else
                {
                    var m = this as MonsterCard;
                    if (m != null)
                    {
                        card.AttackAttribute = m.CombatAttribute.AttackAttribute;
                        card.Attack = m.CombatAttribute.Attack;
                        card.ShieldAttribute = m.CombatAttribute.ShieldAttribute;
                        card.Shield = m.CombatAttribute.Shield;
                        card.Range = m.Range;
                        card.Flexibility = m.Flexibility;
                        card.Magnitude = m.Magnitude;
                    }
                    else
                    {
                        var s = this as SpellCard;
                        if (s != null)
                        {
                            card.Magnitude = s.Magnitude;
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }
            return card;
        }
    }
}
