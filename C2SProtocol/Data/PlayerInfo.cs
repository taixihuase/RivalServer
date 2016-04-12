//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：PlayerInfo.cs
//
// 文件功能描述：
//
// 记录玩家信息
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
using ProtoBuf;

namespace C2SProtocol.Data
{
    /// <summary>
    /// 类型：类 
    /// 名称：PlayerInfo
    /// 作者：taixihuase
    /// 作用：记录玩家基本信息
    /// 编写日期：2016/4/12
    /// </summary>
    [ProtoContract]
    public class PlayerInfo : UserInfo
    {
        /// <summary>
        /// 类型：方法 
        /// 名称：PlayerInfo
        /// 作者：taixihuase
        /// 作用：默认构造玩家数据
        /// 编写日期：2016/4/12
        /// </summary>
        public PlayerInfo()
        {
            Level = 0;
            Experience = 0;
            UpgradeExp = 0;
            Currency = 0;
            Win = 0;
            Total = 0;
            DefaultAvatar = 0;
            DefaultTitle = 0;
            DefaultDeck = 0;
        }

        [ProtoMember(5)]
        public int Level { get; set; }

        [ProtoMember(6)]
        public int Experience { get; set; }

        [ProtoMember(7)]
        public int UpgradeExp { get; set; }

        [ProtoMember(8)]
        public int Currency { get; set; }

        [ProtoMember(9)]
        public int Win { get; set; }

        [ProtoMember(10)]
        public int Total { get; set; }

        [ProtoMember(11)]
        public int DefaultAvatar { get; set; }

        [ProtoMember(12)]
        public int DefaultTitle { get; set; }

        [ProtoMember(13)]
        public int DefaultDeck { get; set; }

        [ProtoMember(14)]
        public Dictionary<int, UserInfo> Friend = new Dictionary<int, UserInfo>();
             
        [ProtoMember(15)]
        public Dictionary<int, AvatarInfo> Avatar = new Dictionary<int, AvatarInfo>(); 

        [ProtoMember(16)]
        public Dictionary<int, TitleInfo> Title = new Dictionary<int, TitleInfo>(); 

        [ProtoMember(17)]
        public Dictionary<int, CardInfo> Card = new Dictionary<int, CardInfo>(); 

        [ProtoMember(18)]
        public Dictionary<int, List<CardInfo>> Deck = new Dictionary<int, List<CardInfo>>();

        [ProtoMember(19)]
        public Dictionary<int, CardPackInfo> CardPack = new Dictionary<int, CardPackInfo>();

        [ProtoMember(20)]
        public Dictionary<int, int> CardPackCount = new Dictionary<int, int>(); 
    }
}
