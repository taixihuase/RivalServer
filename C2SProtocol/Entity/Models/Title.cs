//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：Title.cs
//
// 文件功能描述：
//
// Title 实体
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
    /// 名称：Title
    /// 作者：taixihuase
    /// 作用：Title 实体
    /// 编写日期：2016/4/4
    /// </summary>
    public class Title
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Player> DefaultPlayers { get; set; } = new List<Player>();

        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    }
}
