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
using System.ComponentModel;

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

        public CardType Type { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：CardType
        /// 作者：taixihuase
        /// 作用：卡牌类型枚举
        /// 编写日期：2016/4/5
        /// </summary>
        public enum CardType : byte
        {
            [Description("Basic")] Basic,
            [Description("Lord")] Lord,
            [Description("Monster")] Monster,
            [Description("Spell")] Spell
        }

        public AttributeType MainAttribute { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：AttributeType
        /// 作者：taixihuase
        /// 作用：五行属性类型枚举
        /// 编写日期：2016/4/5
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

        public RarityType Rarity { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：RarityType
        /// 作者：taixihuase
        /// 作用：稀有度类型枚举
        /// 编写日期：2016/4/5
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

        public virtual ICollection<CardEffect> CardEffects { get; set; } = new List<CardEffect>();

        /// <summary>
        /// 类型：枚举
        /// 名称：MagnitudeType
        /// 作者：taixihuase
        /// 作用：星等类型枚举
        /// 编写日期：2016/4/6
        /// </summary>
        public enum MagnitudeType : byte
        {
            [Description("1")] One,
            [Description("2")] Two,
            [Description("3")] Three,
            [Description("4")] Four,
            [Description("5")] Five,
            [Description("6")] Six
        }

        public virtual ICollection<CardPool> CardPools { get; set; } = new List<CardPool>(); 
    }
}
