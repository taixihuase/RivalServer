//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：CardPackInfo.cs
//
// 文件功能描述：
//
// 记录卡包信息
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

using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace C2SProtocol.Data
{
    /// <summary>
    /// 类型：类 
    /// 名称：CardPackInfo
    /// 作者：taixihuase
    /// 作用：记录卡包信息
    /// 编写日期：2016/4/12
    /// </summary>
    [ProtoContract]
    public class CardPackInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        [ProtoMember(1)]
        public int CardPackId { get; set; }

        /// <summary>
        /// 扩展包类型
        /// </summary>
        [ProtoMember(2)]
        public PackType? Type { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：PackType
        /// 作者：taixihuase
        /// 作用：扩展包类型枚举
        /// 编写日期：2016/4/12
        /// </summary>
        public enum PackType : byte
        {
            [Description("Classic")]
            Classic,
        }

        /// <summary>
        /// 名称
        /// </summary>
        [ProtoMember(3)]
        public string Name { get; set; }

        /// <summary>
        /// 售价
        /// </summary>
        [ProtoMember(4)]
        public int Price { get; set; }

        /// <summary>
        /// 所包含的可能卡牌编号
        /// </summary>
        [ProtoMember(5)]
        public List<int> Card = new List<int>();

        /// <summary>
        /// 类型：方法 
        /// 名称：CardPackInfo
        /// 作者：taixihuase
        /// 作用：默认构造卡包数据
        /// 编写日期：2016/4/12
        /// </summary>
        public CardPackInfo()
        {
            CardPackId = 0;
            Type = null;
            Name = string.Empty;
            Price = 0;
        }

        /// <summary>
        /// 类型：方法 
        /// 名称：AddCard
        /// 作者：taixihuase
        /// 作用：添加卡包内所含卡牌数据
        /// 编写日期：2016/4/13
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(int card)
        {
            if(!Card.Contains(card))
                Card.Add(card);
        }

        /// <summary>
        /// 类型：方法 
        /// 名称：RemoveCard
        /// 作者：taixihuase
        /// 作用：移除卡包内所含的指定卡牌数据
        /// 编写日期：2016/4/13
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool RemoveCard(int card)
        {
            return Card.Remove(card);
        }

        /// <summary>
        /// 类型：方法 
        /// 名称：RemoveCard
        /// 作者：taixihuase
        /// 作用：移除卡包内所含的指定卡牌数据
        /// 编写日期：2016/4/13
        /// </summary>
        public void ClearCard()
        {
            Card.Clear();
        }
    }
}
