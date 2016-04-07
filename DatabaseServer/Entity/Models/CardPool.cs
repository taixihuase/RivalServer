//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：CardPool.cs
//
// 文件功能描述：
//
// CardPool实体
//
// 创建标识：taixihuase 20160407
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
    /// 名称：CardLibrary
    /// 作者：taixihuase
    /// 作用：CardPool 实体
    /// 编写日期：2016/3/22
    /// </summary>
    public class CardPool
    {
        public int Id { get; set; }

        public virtual Player Player { get; set; }

        public virtual ICollection<Card> Cards { get; set; } = new List<Card>(); 
    }
}
