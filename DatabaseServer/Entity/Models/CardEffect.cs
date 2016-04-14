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

using C2SProtocol.Data;

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

        public CardEffectInfo.OwnerType Owner { get; set; }

        public CardEffectInfo.ConditionType Condition { get; set; }

        public string Description { get; set; }

        public int? Value { get; set; }

        /// <summary>
        /// 类型：方法
        /// 名称：ToCardEffectInfo
        /// 作者：taixihuase
        /// 作用：转换为 CardEffectInfo 对象
        /// 编写日期：2016/4/14
        /// </summary>
        /// <returns></returns>
        public CardEffectInfo ToCardEffectInfo()
        {
            return new CardEffectInfo
            {
                CardEffectId = Id,
                Owner = Owner,
                Condition = Condition,
                Description = Description,
                Value = Value
            };
        }
    }
}
