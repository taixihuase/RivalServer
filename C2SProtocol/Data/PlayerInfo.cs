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

        /// <summary>
        /// 当前等级
        /// </summary>
        [ProtoMember(1)]
        public int Level { get; set; }

        /// <summary>
        /// 当前等级经验值
        /// </summary>
        [ProtoMember(2)]
        public int Experience { get; set; }

        /// <summary>
        /// 升级所需经验值
        /// </summary>
        [ProtoMember(3)]
        public int UpgradeExp { get; set; }

        /// <summary>
        /// 当前等级战胜所得经验值
        /// </summary>
        [ProtoMember(4)]
        public int WinExp { get; set; }

        /// <summary>
        /// 当前等级战败所得经验值
        /// </summary>
        [ProtoMember(5)]
        public int LoseExp { get; set; }

        /// <summary>
        /// 游戏币数值
        /// </summary>
        [ProtoMember(6)]
        public int Currency { get; set; }

        /// <summary>
        /// 胜利总场数
        /// </summary>
        [ProtoMember(7)]
        public int Win { get; set; }

        /// <summary>
        /// 游戏总场数
        /// </summary>
        [ProtoMember(8)]
        public int Total { get; set; }

        /// <summary>
        /// 默认头像
        /// </summary>
        [ProtoMember(9)]
        public int DefaultAvatar { get; set; }

        /// <summary>
        /// 默认头衔
        /// </summary>
        [ProtoMember(10)]
        public int DefaultTitle { get; set; }

        /// <summary>
        /// 默认牌组序号
        /// </summary>
        [ProtoMember(11)]
        public int DefaultDeck { get; set; }
             
        /// <summary>
        /// 头像编号清单
        /// </summary>
        [ProtoMember(12)]
        public List<int> Avatar = new List<int>();

        /// <summary>
        /// 头衔编号清单
        /// </summary>
        [ProtoMember(13)]
        public List<int> Title = new List<int>();

        /// <summary>
        /// 已有卡牌编号清单
        /// </summary>
        [ProtoMember(14)]
        public List<int> Card = new List<int>();

        /// <summary>
        /// 牌组清单
        /// </summary>
        [ProtoMember(15)]
        public Dictionary<int, List<int>> Deck = new Dictionary<int, List<int>>();

        /// <summary>
        /// 扩展包编号及数量清单
        /// </summary>
        [ProtoMember(16)]
        public Dictionary<int, int> CardPack = new Dictionary<int, int>();

        /// <summary>
        /// 好友清单
        /// </summary>
        [ProtoMember(17)]
        public Dictionary<int, UserInfo> Friend = new Dictionary<int, UserInfo>();
    }
}
