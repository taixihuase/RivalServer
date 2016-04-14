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
using C2SProtocol.Data;

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

        public CardPackInfo.PackType Type { get; set; }     

        public string Name { get; set; }

        public int Price { get; set; }

        public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

        /// <summary>
        /// 类型：方法
        /// 名称：ToCardPackInfo
        /// 作者：taixihuase
        /// 作用：转换为 CardPackInfo 对象
        /// 编写日期：2016/4/14
        /// </summary>
        /// <returns></returns>
        public CardPackInfo ToCardPackInfo()
        {
            var pack = new CardPackInfo
            {
                CardPackId = Id,
                Name = Name,
                Type = Type,
                Price = Price
            };
            foreach (var card in Cards)
            {
                pack.AddCard(card.Id);
            }
            return pack;
        }
    }
}
