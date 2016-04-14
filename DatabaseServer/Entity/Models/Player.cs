//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：Player.cs
//
// 文件功能描述：
//
// Player 实体
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

using System.Collections.Generic;
using C2SProtocol.Data;

namespace DatabaseServer.Entity.Models
{
    /// <summary>
    /// 类型：类
    /// 名称：Player
    /// 作者：taixihuase
    /// 作用：Player 实体
    /// 编写日期：2016/4/4
    /// </summary>
    public class Player : User
    {
        public int LevelId { get; set; }

        public virtual Level Level { get; set; }

        public int Experience { get; set; }

        public int DefaultAvatarId { get; set; }

        public virtual Avatar DefaultAvatar { get; set; }

        public virtual ICollection<Avatar> Avatars { get; set; } = new List<Avatar>();

        public int DefaultTitleId { get; set; }

        public virtual Title DefaultTitle { get; set; }

        public virtual ICollection<Title> Titles { get; set; } = new List<Title>();

        public int VirtualCurrency { get; set; } 

        public int Win { get; set; }

        public int Total { get; set; }

        public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

        public virtual ICollection<Deck> Decks { get; set; } = new List<Deck>();

        public virtual ICollection<PlayerCardPack> PlayerCardPacks { get; set; } = new List<PlayerCardPack>();

        /// <summary>
        /// 类型：方法
        /// 名称：ToPlayerInfo
        /// 作者：taixihuase
        /// 作用：转换为 PlayerInfo 对象
        /// 编写日期：2016/4/14
        /// </summary>
        /// <returns></returns>
        public PlayerInfo ToPlayerInfo()
        {
            PlayerInfo player = new PlayerInfo
            {
                UniqueId = Id,
                Account = Account,
                Nickname = Nickname,
                Status = UserInfo.UserStatus.Default,
                Level = LevelId,
                Experience = Experience,
                UpgradeExp = Level.UpgradeExp,
                WinExp = Level.WinExp,
                LoseExp = Level.LoseExp,
                DefaultAvatar = DefaultAvatarId,
                DefaultTitle = DefaultTitleId,
                Currency = VirtualCurrency,
                Win = Win,
                Total = Total
            };
            foreach (var avatar in Avatars)
            {
                player.Avatar.Add(avatar.Id);
            }
            foreach (var title in Titles)
            {
                player.Title.Add(title.Id);
            }
            foreach (var card in Cards)
            {
                player.Card.Add(card.Id);
            }
            foreach (var pack in PlayerCardPacks)
            {
                player.CardPack.Add(pack.CardPackId, pack.Count);
            }
            foreach (var deck in Decks)
            {
                player.Deck.Add(deck.Index, new List<int>());
                if (deck.LordCardId != null) player.Deck[deck.Index].Add(deck.LordCardId.Value);
                foreach (var card in deck.SummonCards)
                {
                    player.Deck[deck.Index].Add(card.Id);
                }
            }
            return player;
        }
    }
}
