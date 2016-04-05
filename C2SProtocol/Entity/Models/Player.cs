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

namespace C2SProtocol.Entity.Models
{
    /// <summary>
    /// 类型：类
    /// 名称：Player
    /// 作者：taixihuase
    /// 作用：Player 实体
    /// 编写日期：2016/4/4
    /// </summary>
    public class Player
    {
        public int Id { get; set; }

        public User User { get; set; }

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
    }
}
