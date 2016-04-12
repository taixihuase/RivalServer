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
        [ProtoMember(1200)]
        public int CardPackId { get; set; }

        [ProtoMember(1201)]
        public PackType Type { get; set; }

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

        [ProtoMember(1203)]
        public string Name { get; set; }

        [ProtoMember(1204)]
        public int Price { get; set; }

        [ProtoMember(1205)]
        public List<CardInfo> Card = new List<CardInfo>();

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
            Type = PackType.Classic;
            Name = string.Empty;
            Price = 0;
        }
    }
}
