//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：CardEffect.cs
//
// 文件功能描述：
//
// 记录卡牌效果信息
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

using System.ComponentModel;
using ProtoBuf;

namespace C2SProtocol.Data
{
    /// <summary>
    /// 类型：类
    /// 名称：CardEffectInfo
    /// 作者：taixihuase
    /// 作用：记录卡牌效果信息
    /// 编写日期：2016/4/12
    /// </summary>
    public class CardEffectInfo
    {
        [ProtoMember(1100)]
        public int CardEffectId { get; set; }

        [ProtoMember(1101)]
        public OwnerType Owner { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：OwnerType
        /// 作者：taixihuase
        /// 作用：效果所有者类型枚举
        /// 编写日期：2016/4/12
        /// </summary>
        public enum OwnerType : byte
        {
            [Description("All")]
            All,
            [Description("OnlyLord")]
            OnlyLord,
            [Description("OnlyMonster")]
            OnlyMonster,
            [Description("OnlySpell")]
            OnlySpell,
            [Description("LordAndMonster")]
            LordAndMonster,
            [Description("LordAndSpell")]
            LordAndSpell,
            [Description("MonsterAndSpell")]
            MonsterAndSpell
        }

        [ProtoMember(1102)]
        public ConditionType Condition { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：ConditionType
        /// 作者：taixihuase
        /// 作用：触发条件类型枚举
        /// 编写日期：2016/4/12
        /// </summary>
        public enum ConditionType : byte
        {
            [Description("All")]
            All,
        }

        [ProtoMember(1103)]
        public string Description { get; set; }

        [ProtoMember(1104)]
        public int? Value { get; set; }

        /// <summary>
        /// 类型：方法 
        /// 名称：CardEffectInfo
        /// 作者：taixihuase
        /// 作用：默认构造卡牌效果数据
        /// 编写日期：2016/4/12
        /// </summary>
        public CardEffectInfo()
        {
            CardEffectId = 0;
            Owner = OwnerType.All;
            Condition = ConditionType.All;
            Description = string.Empty;
            Value = null;
        }
    }
}
