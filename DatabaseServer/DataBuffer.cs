//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：DataBuffer.cs
//
// 文件功能描述：
//
// 数据库数据缓存池
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
//-----------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Data.Entity;
using C2SProtocol.Data;
using DatabaseServer.Entity.Context;
using DatabaseServer.Entity.Models;

namespace DatabaseServer
{
    /// <summary>
    /// 类型：类 
    /// 名称：DataBuffer
    /// 作者：taixihuase
    /// 作用：数据缓存池
    /// 编写日期：2016/4/12
    /// </summary>
    public class DataBuffer
    {
        public static readonly DataBuffer Instance = new DataBuffer();

        public Dictionary<int, UserInfo> User = new Dictionary<int, UserInfo>(); 

        public Dictionary<int, PlayerInfo> Player = new Dictionary<int, PlayerInfo>(); 

        public Dictionary<int, AvatarInfo> Avatar = new Dictionary<int, AvatarInfo>();
        
        public Dictionary<int, TitleInfo> Title = new Dictionary<int, TitleInfo>(); 

        public Dictionary<int, CardInfo> Card = new Dictionary<int, CardInfo>();

        public Dictionary<int, CardEffectInfo> CardEffect = new Dictionary<int, CardEffectInfo>();
         
        public Dictionary<int, CardPackInfo> CardPack = new Dictionary<int, CardPackInfo>();

        /// <summary>
        /// 类型：方法
        /// 名称：DataBuffer
        /// 作者：taixihuase
        /// 作用：构造数据缓存池
        /// 编写日期：2016/4/12
        /// </summary>
        private DataBuffer()
        {

        }

        /// <summary>
        /// 类型：方法
        /// 名称：Initial
        /// 作者：taixihuase
        /// 作用：从数据库读取数据以初始化缓存池
        /// 编写日期：2016/4/14
        /// </summary>
        public void Initial()
        {
            using (RivalContext db = new RivalContext())
            {
                db.Find<User>().Load();
                db.Find<Player>().Load();
                db.Find<Avatar>().Load();
                db.Find<Title>().Load();
                db.Find<Card>().Load();
                db.Find<CardEffect>().Load();
                db.Find<CardPack>().Load();
                
                foreach (var user in db.Users.Local)
                {                    
                    User.Add(user.Id, user.ToUserInfo());
                }
                foreach (var player in db.Players.Local)
                {
                    Player.Add(player.Id, player.ToPlayerInfo());
                }
                foreach (var avatar in db.Avatars.Local)
                {
                    Avatar.Add(avatar.Id, avatar.ToAvatarInfo());
                }
                foreach (var title in db.Titles.Local)
                {
                    Title.Add(title.Id, title.ToTitleInfo());
                }
                foreach (var card in db.Cards.Local)
                {
                    Card.Add(card.Id, card.ToCardInfo());
                }
                foreach (var effect in db.CardEffects.Local)
                {
                    CardEffect.Add(effect.Id, effect.ToCardEffectInfo());
                }
                foreach (var pack in db.CardPacks.Local)
                {
                    CardPack.Add(pack.Id, pack.ToCardPackInfo());
                }
            }
        }
    }
}
