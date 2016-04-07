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

        public virtual CardPool CardPool { get; set; }

        public int LordCardId { get; set; }

        public bool IsDefault { get; set; }

        #region CardId

        public int CardId2 { get; set; }
        public int CardId3 { get; set; }
        public int CardId4 { get; set; }
        public int CardId5 { get; set; }
        public int CardId6 { get; set; }
        public int CardId7 { get; set; }
        public int CardId8 { get; set; }
        public int CardId9 { get; set; }
        public int CardId10 { get; set; }
        public int CardId11 { get; set; }
        public int CardId12 { get; set; }
        public int CardId13 { get; set; }
        public int CardId14 { get; set; }
        public int CardId15 { get; set; }
        public int CardId16 { get; set; }
        public int CardId17 { get; set; }
        public int CardId18 { get; set; }
        public int CardId19 { get; set; }
        public int CardId20 { get; set; }
        public int CardId21 { get; set; }
        public int CardId22 { get; set; }
        public int CardId23 { get; set; }
        public int CardId24 { get; set; }
        public int CardId25 { get; set; }

        #endregion
    }
}
