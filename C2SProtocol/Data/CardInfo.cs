//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：CardInfo.cs
//
// 文件功能描述：
//
// 记录卡牌信息
//
// 创建标识：taixihuase 20160412
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
using System.ComponentModel;
using ProtoBuf;
// ReSharper disable NonReadonlyMemberInGetHashCode

namespace C2SProtocol.Data
{
    /// <summary>
    /// 类型：类 
    /// 名称：CardInfo
    /// 作者：taixihuase
    /// 作用：记录卡牌信息
    /// 编写日期：2016/4/12
    /// </summary>
    [ProtoContract]
    public class CardInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        [ProtoMember(1)]
        public int CardId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [ProtoMember(2)]
        public string Name { get; set; }

        /// <summary>
        /// 卡牌类型
        /// </summary>
        [ProtoMember(3)]
        public CardType? Type { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：CardType
        /// 作者：taixihuase
        /// 作用：卡牌类型枚举
        /// 编写日期：2016/4/12
        /// </summary>
        public enum CardType : byte
        {
            [Description("Lord")]
            Lord,
            [Description("Monster")]
            Monster,
            [Description("Spell")]
            Spell
        }

        /// <summary>
        /// 主属性
        /// </summary>
        [ProtoMember(4)]
        public AttributeType? MainAttribute { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：AttributeType
        /// 作者：taixihuase
        /// 作用：五行属性类型枚举
        /// 编写日期：2016/4/12
        /// </summary>
        public enum AttributeType : byte
        {
            [Description("木")]
            Wood,
            [Description("火")]
            Fire,
            [Description("土")]
            Earth,
            [Description("金")]
            Metal,
            [Description("水")]
            Water
        }

        /// <summary>
        /// 稀有度
        /// </summary>
        [ProtoMember(5)]
        public RarityType? Rarity { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：RarityType
        /// 作者：taixihuase
        /// 作用：稀有度类型枚举
        /// 编写日期：2016/4/12
        /// </summary>
        public enum RarityType : byte
        {
            [Description("普通")]
            Ordinary,
            [Description("稀有")]
            Rare,
            [Description("史诗")]
            Epic,
            [Description("传说")]
            Legendary
        }

        /// <summary>
        /// 类型：枚举
        /// 名称：MagnitudeType
        /// 作者：taixihuase
        /// 作用：星等类型枚举
        /// 编写日期：2016/4/12
        /// </summary>
        public enum MagnitudeType : byte
        {
            [Description("1")]
            One,
            [Description("2")]
            Two,
            [Description("3")]
            Three,
            [Description("4")]
            Four,
            [Description("5")]
            Five,
            [Description("6")]
            Six
        }

        /// <summary>
        /// 攻击属性
        /// </summary>
        [ProtoMember(6)]
        public AttributeType? AttackAttribute { get; set; }

        /// <summary>
        /// 攻击力
        /// </summary>
        [ProtoMember(7)]
        public int? Attack { get; set; }

        /// <summary>
        /// 护盾属性
        /// </summary>
        [ProtoMember(8)]
        public AttributeType? ShieldAttribute { get; set; }

        /// <summary>
        /// 护盾值
        /// </summary>
        [ProtoMember(9)]
        public int? Shield { get; set; }

        /// <summary>
        /// 星等值
        /// </summary>
        [ProtoMember(10)]
        public MagnitudeType? Magnitude { get; set; }

        /// <summary>
        /// 射程等级
        /// </summary>
        [ProtoMember(11)]
        public int? Range { get; set; }

        /// <summary>
        /// 行动等级
        /// </summary>
        [ProtoMember(12)]
        public int? Flexibility { get; set; }

        /// <summary>
        /// 卡牌效果清单
        /// </summary>
        [ProtoMember(13)]
        public List<int> CardEffect = new List<int>();

        /// <summary>
        /// 类型：方法 
        /// 名称：CardInfo
        /// 作者：taixihuase
        /// 作用：默认构造卡牌数据
        /// 编写日期：2016/4/12
        /// </summary>
        public CardInfo()
        {
            CardId = 0;
            Name = string.Empty;
            Type = null;
            Magnitude = null;
            MainAttribute = null;
            Rarity = null;
            AttackAttribute = null;
            Attack = null;
            ShieldAttribute = null;
            Shield = null;
            Range = null;
            Flexibility = null;
        }

        /// <summary>
        /// 类型：方法 
        /// 名称：CardInfo
        /// 作者：taixihuase
        /// 作用：通过编号和名称构造卡牌数据
        /// 编写日期：2016/4/13
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// </summary>
        public CardInfo(int id, string name)
        {
            CardId = id;
            Name = name;
            Type = null;
            Magnitude = null;
            MainAttribute = null;
            Rarity = null;
            AttackAttribute = null;
            Attack = null;
            ShieldAttribute = null;
            Shield = null;
            Range = null;
            Flexibility = null;
        }

        /// <summary>
        /// 类型：方法 
        /// 名称：SetCombatAttribute
        /// 作者：taixihuase
        /// 作用：设置战斗属性
        /// 编写日期：2016/4/13
        /// </summary>
        /// <param name="attackAttr"></param>
        /// <param name="attack"></param>
        /// <param name="shieldAttr"></param>
        /// <param name="shield"></param>
        public void SetCombatAttribute(AttributeType? attackAttr = null, int? attack = null, AttributeType? shieldAttr = null, int? shield = null)
        {
            AttackAttribute = attackAttr;
            Attack = attack;
            ShieldAttribute = shieldAttr;
            Shield = shield;
        }

        /// <summary>
        /// 类型：方法 
        /// 名称：SetCardType
        /// 作者：taixihuase
        /// 作用：设置卡牌类型，若为法术卡，则清除战斗属性
        /// 编写日期：2016/4/13
        /// </summary>
        /// <param name="type"></param>
        public void SetCardType(CardType type)
        {
            Type = type;
            if(type == CardType.Spell)
                SetCombatAttribute();
        }

        /// <summary>
        /// 类型：方法 
        /// 名称：SetSummonMagnitude
        /// 作者：taixihuase
        /// 作用：设置召唤星等
        /// 编写日期：2016/4/13
        /// </summary>
        /// <param name="magnitude"></param>
        public void SetSummonMagnitude(MagnitudeType? magnitude = null)
        {
            Magnitude = magnitude;
        }

        /// <summary>
        /// 类型：方法 
        /// 名称：SetMainAttribute
        /// 作者：taixihuase
        /// 作用：设置主属性
        /// 编写日期：2016/4/13
        /// </summary>
        /// <param name="mainAttr"></param>
        public void SetMainAttribute(AttributeType? mainAttr = null)
        {
            MainAttribute = mainAttr;
        }

        /// <summary>
        /// 类型：方法 
        /// 名称：SetRarity
        /// 作者：taixihuase
        /// 作用：设置稀有度
        /// 编写日期：2016/4/13
        /// </summary>
        /// <param name="rarity"></param>
        public void SetRarity(RarityType? rarity = null)
        {
            Rarity = rarity;
        }

        /// <summary>
        /// 类型：方法 
        /// 名称：SetMonsterExtraAttritube
        /// 作者：taixihuase
        /// 作用：设置怪兽卡额外属性
        /// 编写日期：2016/4/13
        /// </summary>
        /// <param name="range"></param>
        /// <param name="flexibility"></param>
        public void SetMonsterExtraAttritube(int? range = null, int? flexibility = null)
        {
            Range = range;
            Flexibility = flexibility;
        }

        /// <summary>
        /// 类型：方法 
        /// 名称：AddCardEffect
        /// 作者：taixihuase
        /// 作用：添加一条卡牌效果
        /// 编写日期：2016/4/13
        /// </summary>
        /// <param name="cardEffect"></param>
        public void AddCardEffect(int cardEffect)
        {
            if(!CardEffect.Contains(cardEffect))
                CardEffect.Add(cardEffect);
        }

        /// <summary>
        /// 类型：方法 
        /// 名称：RemoveCardEffect
        /// 作者：taixihuase
        /// 作用：移除一条指定的卡牌效果
        /// 编写日期：2016/4/13
        /// </summary>
        /// <param name="cardEffect"></param>
        /// <returns></returns>
        public bool RemoveCardEffect(int cardEffect)
        {
            return CardEffect.Remove(cardEffect);
        }

        /// <summary>
        /// 类型：方法 
        /// 名称：ClearCardEffect
        /// 作者：taixihuase
        /// 作用：清空卡牌效果
        /// 编写日期：2016/4/13
        /// </summary>
        public void ClearCardEffect()
        {
            CardEffect.Clear();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this == obj)
                return true;

            var cardInfo = obj as CardInfo;
            return cardInfo != null && CardId == cardInfo.CardId;
        }

        public override int GetHashCode()
        {
            return CardId ^ Name.GetHashCode();
        }
    }
}
