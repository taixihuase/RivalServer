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

namespace C2SProtocol.Entity.Models
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

        public string Type { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：CardType
        /// 作者：taixihuase
        /// 作用：卡牌类型枚举
        /// 编写日期：2016/4/5
        /// </summary>
        public enum CardType
        {
            [Description("Basic")] Basic,
            [Description("Lord")] Lord,
            [Description("Monster")] Monster,
            [Description("Spell")] Spell
        }

        public string MainAttribute { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：AttributeType
        /// 作者：taixihuase
        /// 作用：五行属性类型枚举
        /// 编写日期：2016/4/5
        /// </summary>
        public enum AttributeType
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

        public string Rarity { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：RarityType
        /// 作者：taixihuase
        /// 作用：稀有度类型枚举
        /// 编写日期：2016/4/5
        /// </summary>
        public enum RarityType
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
    }
}
