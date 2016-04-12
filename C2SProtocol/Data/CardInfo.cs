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
        [ProtoMember(1000)]
        public int CardId { get; set; }

        [ProtoMember(1001)]
        public string Name { get; set; }

        [ProtoMember(1002)]
        public CardType Type { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：CardType
        /// 作者：taixihuase
        /// 作用：卡牌类型枚举
        /// 编写日期：2016/4/12
        /// </summary>
        public enum CardType : byte
        {
            [Description("Basic")]
            Basic,
            [Description("Lord")]
            Lord,
            [Description("Monster")]
            Monster,
            [Description("Spell")]
            Spell
        }

        [ProtoMember(1003)]
        public AttributeType MainAttribute { get; set; }

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

        [ProtoMember(1004)]
        public RarityType Rarity { get; set; }

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

        [ProtoMember(1005)]
        public AttributeType? AttackAttribute { get; set; }

        [ProtoMember(1006)]
        public int? Attack { get; set; }

        [ProtoMember(1007)]
        public AttributeType? ShieldAttribute { get; set; }

        [ProtoMember(1008)]
        public int? Shield { get; set; }

        [ProtoMember(1009)]
        public MagnitudeType? Magnitude { get; set; }

        [ProtoMember(1010)]
        public int? Range { get; set; }

        [ProtoMember(1011)]
        public int? Flexibility { get; set; }

        [ProtoMember(1012)]
        public List<CardEffectInfo> CardEffect = new List<CardEffectInfo>();

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
            Type = CardType.Basic;
            Magnitude = null;
            MainAttribute = AttributeType.Earth;
            Rarity = RarityType.Ordinary;
            AttackAttribute = null;
            Attack = null;
            ShieldAttribute = null;
            Shield = null;
            Range = null;
            Flexibility = null;
        }
    }
}
