//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：CardPack.cs
//
// 文件功能描述：
//
// CardPack 实体
//
// 创建标识：taixihuase 20160411
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

namespace DatabaseServer.Entity.Models
{
    /// <summary>
    /// 类型：类
    /// 名称：CardPack
    /// 作者：taixihuase
    /// 作用：CardPack 实体
    /// 编写日期：2016/4/11
    /// </summary>
    public class CardPack
    {
        public int Id { get; set; }

        public PackType Type { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：PackType
        /// 作者：taixihuase
        /// 作用：扩展包类型枚举
        /// 编写日期：2016/4/11
        /// </summary>
        public enum PackType : byte
        {
            [Description("Classic")]
            Classic,
        }        

        public string Name { get; set; }

        public int Price { get; set; }

        public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
    }
}
