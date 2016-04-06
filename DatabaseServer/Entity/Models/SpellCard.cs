﻿//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：SpellCard.cs
//
// 文件功能描述：
//
// SpellCard 实体
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

namespace DatabaseServer.Entity.Models
{
    /// <summary>
    /// 类型：类
    /// 名称：SpellCard
    /// 作者：taixihuase
    /// 作用：SpellCard 实体
    /// 编写日期：2016/4/4
    /// </summary>
    public class SpellCard : Card
    {
        public MagnitudeType Magnitude { get; set; }
    }
}