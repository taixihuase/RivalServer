//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：SummonCard.cs
//
// 文件功能描述：
//
// SummonCard 实体
//
// 创建标识：taixihuase 20160410
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

namespace DatabaseServer.Entity.Models
{
    /// <summary>
    /// 类型：类
    /// 名称：SummonCard
    /// 作者：taixihuase
    /// 作用：SummonCard 实体
    /// 编写日期：2016/4/10
    /// </summary>
    public class SummonCard : Card
    {
        public MagnitudeType Magnitude { get; set; }
    }
}
