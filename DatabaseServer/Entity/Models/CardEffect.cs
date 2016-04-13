//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：CardEffect.cs
//
// 文件功能描述：
//
// CardEffect 实体
//
// 创建标识：taixihuase 20160405
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

namespace DatabaseServer.Entity.Models
{
    /// <summary>
    /// 类型：类
    /// 名称：CardEffect
    /// 作者：taixihuase
    /// 作用：CardEffect 实体
    /// 编写日期：2016/4/5
    /// </summary>
    public class CardEffect
    {
        public int Id { get; set; }

        public OwnerType Owner { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：OwnerType
        /// 作者：taixihuase
        /// 作用：效果所有者类型枚举
        /// 编写日期：2016/4/5
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

        public ConditionType Condition { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：ConditionType
        /// 作者：taixihuase
        /// 作用：触发条件类型枚举
        /// 编写日期：2016/4/5
        /// </summary>
        public enum ConditionType : byte
        {
            [Description("All")]
            All,
        }

        public string Description { get; set; }

        public int? Value { get; set; }
    }
}
