//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：Deck.cs
//
// 文件功能描述：
//
// Deck 实体
//
// 创建标识：taixihuase 20160408
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

namespace DatabaseServer.Entity.Models
{
    /// <summary>
    /// 类型：类
    /// 名称：Deck
    /// 作者：taixihuase
    /// 作用：Deck 实体
    /// 编写日期：2016/4/8
    /// </summary>
    public class Deck
    {
        public int Id { get; set; }

        public int Index { get; set; }

        public string Name { get; set; }

        public virtual Player Player { get; set; }

        public bool IsDefault { get; set; }

        public int? LordCardId { get; set; }

        public virtual LordCard LordCard { get; set; }

        public virtual ICollection<SummonCard> SummonCards { get; set; } = new List<SummonCard>(24);
   
        public int CardCount { get; set; }
    }
}
