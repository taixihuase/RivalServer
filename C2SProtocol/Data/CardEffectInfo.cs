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
// ReSharper disable NonReadonlyMemberInGetHashCode

namespace C2SProtocol.Data
{
    /// <summary>
    /// 类型：类
    /// 名称：CardEffectInfo
    /// 作者：taixihuase
    /// 作用：记录卡牌效果信息
    /// 编写日期：2016/4/12
    /// </summary>
    [ProtoContract]
    public class CardEffectInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        [ProtoMember(1)]
        public int CardEffectId { get; set; }

        /// <summary>
        /// 效果所有者
        /// </summary>
        [ProtoMember(2)]
        public OwnerType? Owner { get; set; }

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

        /// <summary>
        /// 触发条件
        /// </summary>
        [ProtoMember(3)]
        public ConditionType? Condition { get; set; }

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

        /// <summary>
        /// 效果描述
        /// </summary>
        [ProtoMember(4)]
        public string Description { get; set; }

        /// <summary>
        /// 效果附带数值
        /// </summary>
        [ProtoMember(5)]
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
            Owner = null;
            Condition = null;
            Description = string.Empty;
            Value = null;
        }

        /// <summary>
        /// 类型：方法 
        /// 名称：SetCardEffectOwner
        /// 作者：taixihuase
        /// 作用：设置卡牌效果所有者
        /// 编写日期：2016/4/13
        /// </summary>
        /// <param name="owner"></param>
        public void SetCardEffectOwner(OwnerType owner = OwnerType.All)
        {
            Owner = owner;
        }

        /// <summary>
        /// 类型：方法 
        /// 名称：SetCardEffectCondition
        /// 作者：taixihuase
        /// 作用：设置卡牌效果触发条件
        /// 编写日期：2016/4/13
        /// </summary>
        /// <param name="condition"></param>
        public void SetCardEffectCondition(ConditionType? condition = ConditionType.All)
        {
            Condition = condition;
        }

        /// <summary>
        /// 类型：方法 
        /// 名称：SetCardEffect
        /// 作者：taixihuase
        /// 作用：设置卡牌效果描述和数值
        /// 编写日期：2016/4/13
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="val"></param>
        public void SetCardEffect(string desc, int? val = null)
        {
            Description = desc;
            Value = val;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this == obj)
                return true;

            var cardEffectInfo = obj as CardEffectInfo;
            return cardEffectInfo != null && (CardEffectId == cardEffectInfo.CardEffectId && Value == cardEffectInfo.Value);
        }

        public override int GetHashCode()
        {
            return CardEffectId ^ Description.GetHashCode();
        }
    }
}
